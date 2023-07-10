using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Assets.tryoutFolder.script
{
    public abstract class RotatableElement : MonoBehaviour
    {

        protected RotatableElement driverElement;
        protected RotatableElement[] surroundingElements;
        protected RotatableElement driverJammingElement;
        protected float speed;
        protected Vector3 rotationDirection;
        [SerializeField] protected int teeths;
        [SerializeField] protected bool hasFriction = true;
        [Range(0, 30f)]
        [SerializeField] protected float friction = 10f;

        public float Speed { get { return speed; } }
        public Vector3 RotationDirection { get { return rotationDirection; } }
        public int Teeths { get { return teeths; } }

        public bool IsJamming => driverJammingElement != null;

        protected bool isStartingElement;

        protected virtual void Start()
        {
            var startingGearComponent = GetComponent<StartingGearClass>();
            if (startingGearComponent != null) { isStartingElement = true; }
            else
            {
                isStartingElement = false;
            }
        }

        protected virtual void Update()
        {
            surroundingElements = FindingRotatingElement();
            if (driverElement != null && !isStartingElement)
            {
                CheckingDriverElement();
            }
            if (driverJammingElement != null)
            {
                //check if there is a jamming gear still exist
                CheckJammingElement();
            }

            if (driverJammingElement != null)
            {
                //if the jamming element still exist
                speed = 0;
                JamSurroundingElements();
            }
            else
            {
                RotateOtherElements();
            }
        }

        private void RotateOtherElements()
        {
            // do the standard gear rotation
            if (speed > 0) //can also be >= 0 to make the gear jam.
            {
                RotateElementVisually();
                RotateSurroundingElements();
                if (hasFriction)
                {
                    ApplyFriction();
                }
            }
            else
            {
                speed = 0;
            }
        }

        protected void ApplyFriction()
        {
            speed -= friction * Time.deltaTime;
        }

        protected virtual void CheckingDriverElement()
        {
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] == driverElement)
                {
                    if (driverElement.Speed > 0)
                    {
                        return;
                    }
                    break;
                }
            }
            driverElement = null;
        }

        protected virtual void CheckJammingElement()
        {
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] == driverJammingElement)
                {
                    if (driverJammingElement.IsJamming)
                    {
                        return;
                    }
                    break;
                }
            }
            driverJammingElement = null;
        }

        protected abstract RotatableElement[] FindingRotatingElement();

        public virtual void AddSpeedAndRotation(float speed, Vector3 rotation, RotatableElement driver = null)
        {
            if (isStartingElement)
            {// ignore this as this is a edge case
                this.speed = speed;
                this.rotationDirection = rotation;
            }
            else if(this.driverElement == null )
            {
                //if the rotate element is not assign a rotatable element
                this.speed = speed;
                this.rotationDirection = rotation;
                this.driverElement = driver;
            }
            else if(this.driverElement == driver)
            { //only rotate accept rotation from the driver only
                this.speed = speed;
                this.rotationDirection = rotation;
            }
        }

        public void AddJamingElement(RotatableElement jammingElement)
        {
            driverJammingElement = jammingElement;
        }

        protected virtual void RotateElementVisually() { }

        protected virtual void RotateSurroundingElements()
        {
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                RotatableElement selectedRotatableElement = surroundingElements[i];
                if (selectedRotatableElement == driverElement) continue;
                if (selectedRotatableElement.Teeths == 0)
                {   //this meant that it is a joint
                    selectedRotatableElement.AddSpeedAndRotation(speed, rotationDirection, this);
                }
                else
                { //for gears
                    float newSpeed = CalculateSpeed(this, selectedRotatableElement);
                    selectedRotatableElement.AddSpeedAndRotation(newSpeed, ChangeDirection(rotationDirection), this);
                }
            }
        } // rotate all element

        protected virtual void JamSurroundingElements()
        {
            for(int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] != driverJammingElement)
                {
                    surroundingElements[i].AddJamingElement(this);
                }
            }
        }

        private float CalculateSpeed(RotatableElement driver, RotatableElement driven)
        {
            float gearRatio = (float)driven.Teeths / (float)driver.Teeths;
            float calculatedSpeed = driver.Speed / gearRatio;
            return calculatedSpeed;
        }

        private Vector3 ChangeDirection(Vector3 direction)
        {
            if (direction == Vector3.forward) return Vector3.back;
            else return Vector3.forward;
        }


    }
}