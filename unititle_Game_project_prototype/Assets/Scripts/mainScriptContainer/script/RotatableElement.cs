using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Assets.tryoutFolder.script
{
    public abstract class RotatableElement : MonoBehaviour
    {
        //readme!
        /*
        Rotatable Element is the base abstract class for all implementation
        of class that requires to rotate. It follows a strict update loop 
        which you can look down.

        How it works:
        1. In order for a element to rotate, it requires an element to rotate.
        2. a starting gear. ususally using a Starting gear script will be rotating throughout the game
        3. It passes speed through the AddSpeedAndRotation() method. where the element that is being rotated will
        store memory to the driver element as you can see below. It will also set the speed and rotation of the driven element
        4. In every update call, whenever there is speed, it will show that speed through RotateElementVisually(). 
        where it will show the speed (like gears) or not (like joints)
        5. lastly, it will slowly reduce the speed using friction.

        Some things to consider:
        1. the rotatable element will ususally check if there is a rotatable element within its surrounding.
        (this is called using the FindingRotatingElement() function)
        2. There will also be jamming gear ( like the one directional gear ) which will immediately stop all 
        movement in the gear. the JamSurroundingElements() also function the same way as the AddSpeedAndRotation() 
        but you will have to see the one direction gear to find out more.
        3. You can briefly look through the update call to understand how a rotatable act within each update call.


        Some problem I face (currently):
        1. there will be times where the certain function does not do its own responsiblity
        2. Some bugs will still appear which I plan to fix
        */

        //most of the code are self documenting

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
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] != driverJammingElement)
                {
                    surroundingElements[i].AddJamingElement(this);
                }
            }

            /*problem: sometimes, this does not work as intented, Even though they have 
                jamming element, it does not seem to be jaming the surround gears for some reason.
            I tried to check why but the logic I saw made sense. So it is quite strange...
            In some gameplay, it does not work, in others it works.... er i try to figure
            it again soon
            */
        } // this is where Gears are forcefully stopped due to jamming gears.

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