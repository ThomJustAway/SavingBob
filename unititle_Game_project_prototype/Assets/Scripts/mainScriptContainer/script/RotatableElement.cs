using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Assets.tryoutFolder.script
{
    public abstract class RotatableElement : MonoBehaviour
    {

        protected RotatableElement driverElement;
        protected RotatableElement[] surroundingElements;
        protected float speed;
        protected Vector3 rotationDirection;
        [SerializeField] protected int teeths;
        [SerializeField] private bool hasFriction =true;
        [Range(0,30f)]
        [SerializeField] protected float friction = 10f;

        public float Speed { get { return speed; } }
        public Vector3 RotationDirection { get { return rotationDirection; } }
        public int Teeths { get { return teeths; } }

        protected bool isStartingElement;

        protected virtual void Start()
        {
            var startingGearComponent = GetComponent<StartingGearClass>();
            if (startingGearComponent != null) { isStartingElement = true; }
            else isStartingElement = false;
        }

        protected virtual void Update()
        {

            /*
                why does the gear does not move when touch with another gear that is moving.
                1. starting gear does not have driver gear.
                2. the movement of the starting gear is override with the gear that is not connected to anything
                3. they will both slow down until they reach to zero.
                4. afterwards, the starting gear script would add speed again to the starting gear
             */

            

            surroundingElements = FindingRotatingElement();
            if(driverElement != null && !isStartingElement) 
            {
                CheckingDriverElement();
            }
            
            if(speed > 0) //can also be >= 0 to make the gear jam.
            {
                RotateElementVisually();
                RotateSurroundingElements();
                if(hasFriction)
                {
                    ApplyFriction();
                }
            }
            else
            {
                speed = 0;
            }

        }

        private void ApplyFriction()
        {
            speed -= friction * Time.deltaTime;
        }

        protected virtual void CheckingDriverElement()
        {
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] == driverElement)
                {
                    if (driverElement.Speed > 0 )
                    {
                        return;
                    }
                    break;
                }
            }
            driverElement = null;
        }

        protected abstract RotatableElement[] FindingRotatingElement();

        public virtual void AddSpeedAndRotation(float speed, Vector3 rotation , RotatableElement driver = null)
        {
            this.speed = speed;
            this.rotationDirection = rotation;
            if (!isStartingElement)
            {
                this.driverElement = driver;
            }

        }

        protected virtual void RotateElementVisually() { }

        protected virtual void RotateSurroundingElements()
        {
            for(int i = 0; i < surroundingElements.Length; i++)
            {
                RotatableElement selectedRotatableElement = surroundingElements[i];
                if (selectedRotatableElement == driverElement) continue; 
                if(selectedRotatableElement.Teeths == 0)
                {   //this meant that it is a joint
                    selectedRotatableElement.AddSpeedAndRotation(speed, rotationDirection, this);
                }
                else
                { //for gears
                    float newSpeed = CalculateSpeed(this, selectedRotatableElement);
                    selectedRotatableElement.AddSpeedAndRotation(newSpeed, ChangeDirection(rotationDirection) , this);
                }
            }
        } // rotate all element

        private float CalculateSpeed(RotatableElement driver , RotatableElement driven)
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