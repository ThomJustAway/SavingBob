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

        public bool HasDriver => driverElement != null;

        protected bool isStartingElement;

        protected virtual void Start()
        {
            var startingGearComponent = GetComponent<StartingGearClass>();
            if (startingGearComponent != null) { isStartingElement = true; }
            else
            {
                isStartingElement = false;
            }
            //this check is to make sure that the element is not a starting element. If it is, then there will be 
            // a different behaviour that it will go
        }

        protected virtual void Update()
        {
            surroundingElements = FindingRotatingElement();
            if (driverElement != null && !isStartingElement)
            {
                /*
                if there is a driver element stored in the memory
                and it is not a starting element, then it will make sure
                if there is the rotatble element is still in the surrounding elements.
                
                It is important to get the surrounding element as it make sure that gears
                know what to get it's speed.
                 */
                CheckingDriverElement();
            }
            if (driverJammingElement != null)
            {
                /*
                check if there is a jamming gear still exist.
                
                this is important as the rotatable element needs to know if it has
                to stop because of a jamming element (like one directional rotational gear)
                 
                 */
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
                //normal behaviour to rotate surrounding element
                RotateOtherElements();
            }
        }

        private void RotateOtherElements()
        {
            // do the standard gear rotation
            if (speed > 0) 
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
            //gradually reduce the speed. this wont be a matter if there is a driver element to supply speed
            speed -= friction * Time.deltaTime;
        }

        protected virtual void CheckingDriverElement()
        {
            //iterate over the array to find the driver element
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] == driverElement)
                {
                    if (driverElement.Speed > 0)
                    { //check if the driverelement still has speed. if it has then the driver element is still a driver element
                        return;
                    }
                    break;
                }
            }
            //this mean that there is no driver element anymore and
            //that the rotatable element can set this to null to perform
            //another action
            driverElement = null;
        }

        protected virtual void CheckJammingElement()
        {

            //same thing where it iterates over the elements to find 
            // a jamming gear
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] == driverJammingElement)
                {
                    if (driverJammingElement.IsJamming)
                    {
                        // this does a sort of check to see if the driverjamming element itself is jammed
                        return;
                    }
                    break;
                }
            }
            driverJammingElement = null;
        }


        //this code stores the operation to find rotatable elements. different scripts will have different implementation of it to find
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

        protected virtual void RotateElementVisually() { } //show how the rotatable element is rotated

        protected virtual void RotateSurroundingElements() 
        {
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                RotatableElement selectedRotatableElement = surroundingElements[i];
                if (selectedRotatableElement == driverElement) continue; //ignore the drive element
                if (selectedRotatableElement.Teeths == 0)
                {   //this meant that it is a joint. Therefore, there will be different behaviour as well
                    // so rotatable element will give current speed and it current rotatable
                    // direction to the joint (because that is how it works)
                    selectedRotatableElement.AddSpeedAndRotation(speed, rotationDirection, this); 

                }
                else
                { //for gears it is more special as the speed differs between two different gear sizes
                    // there is a special formula to get the speed
                    float newSpeed = CalculateSpeed(this, selectedRotatableElement); 
                    //give the rotatable element new speed and give a reverse direction to show that the gear is being rotated
                    selectedRotatableElement.AddSpeedAndRotation(newSpeed, ChangeDirection(rotationDirection), this);
                }
            }
        } // rotate all element

        protected virtual void JamSurroundingElements()
        {
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                //iterate and find all element and jam them
                if (surroundingElements[i] != driverJammingElement)
                {
                    //add the jaming element to the other rotatable element so that they know they have to jam
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
            //formula: gearRatio = driven.teeths / driver teeths
            // driven speed = driver.speed / gearRatio 
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