using System.Collections;
using UnityEngine;
using Assets.tryoutFolder.script;
using System.Linq;
using System.Collections.Generic;

public class JointBehaviour : RotatableElement, IMoveable
{
    //readme
    /*
        You can think of joints as two gear at different layers.
        The gear will has two joint, lower and upper joint.
        Basically what is that it check for any connected gear 
        from either joint. 

        Afterwards, it will tell the opposing gear to follow the rotation
        and speed of the gear. It is also dragable due to the IMoveable interface
        The methods are pretty self documenting so feel free to start at the 
        FindingRotatingElement() to find out how it detect the surrounding joints.
    */

    [SerializeField] private CircleCollider2D lowerJoint;// upper and lower joints are to check for gears/joints 
    [SerializeField] private CircleCollider2D upperJoint;
    [SerializeField] private string nameOfElement; //Joint in this case are dragable object as they implement the imoveable interface
    [SerializeField] private int cost;
    private float radius; //have radius to do physic raycasting
    public int Cost => cost;
    private int layer; // keep track of where the joint z position is at

    private RotatableElement connectedGearLower; //this is used to auto automatically stick to the gear at the bottom and top
    private RotatableElement connectedGearUpper;

    public GameObject Getprefab => gameObject; //look at imoveable. All imoveable requires this

    public string Name => name;

    protected override RotatableElement[] FindingRotatingElement()
    {

        //basically find elements from both the lower and upper joint
        //afterwards, combine together to get all the rotatable element near the joint
        RotatableElement[] lowerJointElements = GetElementsFromJoint(lowerJoint);
        RotatableElement[] upperJointElements = GetElementsFromJoint(upperJoint);
        return lowerJointElements.Concat(upperJointElements).ToArray();
    }

    protected override void RotateSurroundingElements()
    {
        //the different gears will give speed to the joint, so all it has to do is just give rotation to the surrounding gears and joints
        if (speed > 0)
        { //if the speed of the joint is not zero, rotate all the other element surrounding the joint(which are connected to the joint)
            for (int i = 0; i < surroundingElements.Length; i++)
            {
                if (surroundingElements[i] != driverElement) 
                    surroundingElements[i].AddSpeedAndRotation(speed, rotationDirection, this);
            }
        }
    }

    private RotatableElement[] GetElementsFromJoint(Collider2D joint)
    {
        float minDept = joint.transform.position.z - 0.3f; 
        float maxDept = joint.transform.position.z + 0.3f; // this range is to make sure the joint check its current layer
        RotatableElement connectedGear = FindRotatableGearFromJoint(minDept, maxDept); //get both joint and gear near the joint
        RotatableElement connectedJoint = FindRotatableJointFromJoint(minDept, maxDept, joint);

        List<RotatableElement> elements = new List<RotatableElement>(); //will have a list to add gear and joint that the joint found
        if (connectedGear != null ) 
        {
            elements.Add(connectedGear); 
        }
        if(connectedJoint != null )
        {
            elements.Add(connectedJoint);
        } //add the gear and joint if it exist

        return elements.ToArray();

    }

    private RotatableElement FindRotatableGearFromJoint(float minDept, float maxDept)
    {
        //function to find out if there is a rotatable gear near the joint
        var collider = Physics2D.OverlapCircle(transform.position, radius, LayerData.EntireAreaLayer, minDept, maxDept);
        if (collider != null)
        {//if there is a gear, get the rotatable element
            return collider.GetComponentInParent<RotatableElement>();
        }
        return null;
    }

    private RotatableElement FindRotatableJointFromJoint(float minDept, float maxDept, Collider2D joint)
    {
        //function to find the surrounding joint (the joint has many collider, so have to use overlap circle all)
        var colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerData.JointLayer, minDept, maxDept);
        //this physic.overlapcircleall basically finds all the collider in a joint layer. it will only detect joints 
        foreach (Collider2D collider in colliders)
        {
            //the function will go through the different collider to find a joint that is not theirs
            if (collider != joint)
            {
                //if there is a collider it mean it found a joint, return that joint rotatable element component)
                return collider.GetComponentInParent<RotatableElement>();
            }
        }
        return null;
    }

    private void CheckBothJoint()
    {
        //will check if there is a gear at the bottom and top joint and store it as reference
        CheckJoint(lowerJoint, true);
        CheckJoint(upperJoint, false);   //look at both joint
    }

    private void CheckJoint(Collider2D joint, bool isLowerJoint)
    {
        float minDept = joint.transform.position.z - 0.3f;
        float maxDept = joint.transform.position.z + 0.3f; //restrict the layer it can check

        Collider2D getRotatableElementSurroundingJoint = Physics2D.OverlapCircle
            (joint.transform.position,
            radius,
            LayerData.InnerAreaLayer,
            minDept
            , maxDept
            ); //get the rotatable gear near the joint

        
        if (getRotatableElementSurroundingJoint != null)
        { //if have found a rotatable object
            //assign it to the different variables that is related to the position of the joint
            if (isLowerJoint)
            {
                connectedGearLower = getRotatableElementSurroundingJoint.GetComponentInParent<RotatableElement>();
            }
            else
            {
                connectedGearUpper = getRotatableElementSurroundingJoint.GetComponentInParent<RotatableElement>();
            }

        }
        else
        { //if it does not, make the value of connectedGearLower and upper to be null based on the where the value is at
            if (isLowerJoint)
            {
                connectedGearLower = null;
            }
            else
            {
                connectedGearUpper = null;
            }
        }
 
    }

    public void Move(Vector3 position)
    {
        if (layer == 0)
        {//if the joint does not have any value which means that it was just brought
            layer = LayerManager.instance.CurrentLayer;
        }
        transform.position = position;
    }

    private void CheckCorrectLayer()
    {
        //check correct layer is important to make sure that the joint stay put in the current layer it is in
        int maxLayer = LevelManager.instance.currentGameData.NumberOfLayers; //check how many layers are there
        int currentLayer = LayerManager.instance.CurrentLayer; //check the current layer
        if (currentLayer == maxLayer)
        { 
            //as there is a top joint and bottom joint. this calculation is done at the bottom joint.
            //as a result if this code is not here, the joint below the maximum layer will not be shown 
            //as the top joint is above the next layer (which does not exist). As a result, it
            //is important to push the bottom joint by one layer down so that the top joint can be shown at the max layer.
            //it is a bit hard to understand this concept so you should see the joint prefab.
            transform.Translate(0, 0, +3); // basically moving one layer down
        }
        else 
        {

            //another important code to tell the joint to stay put in their current layer
            //the global variable layer is to keep track of the joint current layer.
            /*
                When the joint is move at the top joint, it will force the bottom joint to 
                go the the top joint position. as a result, this will make the bottom joint disappear
                if the top joint is being moved
                This difference is basically making sure it moves one layer down so that the bottom joint
                will remain at the bottom
             */
            int difference = currentLayer - layer; 
            transform.Translate(0, 0, 3 * difference);
        }
    } //does the checking and making sure the joint is under the same layer

    public void CheckValidPosition() //fix this later
    {
        CheckCorrectLayer(); 
        CheckBothJoint();

        //this will make the joint auto stick to different gear if it sense a lower or upper gear
        if (connectedGearLower != null)
        {
            transform.position = connectedGearLower.transform.position;
        }
        else if (connectedGearUpper != null)
        {
            //if it is upper gear go to the position of it. but this will cause a problem where the lower joint will 
            //go to the connected gear upper. So we need to fix that
            Vector3 newPosition = connectedGearUpper.transform.position;
            newPosition.z = newPosition.z + 3; // this makes the joint remain at the same position as it pushes the 
            //joint down by one layer
            transform.position = newPosition; 
        }
        MusicManager.Instance.PlayMusicClip(SoundData.PlacingSound);
    }

    public void RemoveItem()
    {
        var itemButtons = LevelManager.instance.itemButtons;
        for (int i = 0; i < itemButtons.Length; i++)
        {//the function will find the item button relating to the joint.
            if (itemButtons[i].IsGameObjectRelated(gameObject))
            {//if found, then remove the item.
                itemButtons[i].RemoveItem(gameObject);
                break;
            }
        }
    }
}
