using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;

public class JointBehaviour : MonoBehaviour ,IMoveable
{

    [SerializeField] private Collider2D lowerJoint;
    [SerializeField] private Collider2D upperJoint;
    [SerializeField] private int cost;
    [SerializeField] private string name;
    private Gear connectedGearLower;
    private Gear connectedGearUpper;
    private float radius = 0.5f;

    private Gear driverGear;
    private JointBehaviour connectingJoint;
    private int layer = 0; // this variable refers to the lower joint layer;

    public int Cost { get { return cost; } }

    public GameObject Getprefab => this.gameObject;

    public string Name => name;


    void Update()
    {

        // things that needs to be fix
        // 1. the connection between joints
        
        //how to do it?
        /*
        1. have the joint find driver gear.
        2. use the gear speed and rotation.
        3. tell all the connecting joint to rotate using the driver gear.
         */

        //add speed and rotation
        //connected joint (set connected joint)

        //some problems
        /*
         1. How do the joints know if they are connect, if so ,
            which rotating gear should they follow?
         2. how to simplify the code to make it easy and readible
         3. Is there a better solution?
          */
        
        CheckBothJoint();

        if(connectedGearLower != null && connectedGearUpper != null)
        {
            if (connectedGearLower.Speed > 0 && (connectedGearUpper.DriverGear == null || connectedGearUpper.DriverGear == connectedGearLower))
            {
                connectedGearUpper.AddSpeedAndRotation(connectedGearLower.Speed, connectedGearLower.Direction, connectedGearLower);
            }
            else if (connectedGearUpper.Speed > 0 && (connectedGearLower.DriverGear == null || connectedGearLower.DriverGear == connectedGearUpper))
            {
                connectedGearLower.AddSpeedAndRotation(connectedGearUpper.Speed, connectedGearUpper.Direction, connectedGearUpper);
            }
            else
            {
                print("Invalid Position!");
            }
        } 

        //if( connectedGearLower  != null && connectedGearUpper == null)
        //{
        //    SetSpeedAndDirection(connectedGearLower.Speed,connectedGearLower.Direction);
        //}

    }

    
    //private void SetSpeedAndDirection(float speed, Vector3 direction)
    //{
    //    this.speed = speed;
    //    this.direction = direction;
    //}


    private void CheckBothJoint()
    {
        CheckJoint(lowerJoint, true);
        CheckJoint(upperJoint, false);   //look at both joint
    }

    private void CheckJoint(Collider2D joint , bool isLowerJoint )
    {
        float minDept = joint.transform.position.z - 0.3f;
        float maxDept = joint.transform.position.z + 0.3f;

        Collider2D gearSurroundingJoint = Physics2D.OverlapCircle
            (joint.transform.position,
            radius,
            LayerData.InnerGearLayer,
            minDept
            , maxDept
            );
        if (gearSurroundingJoint != null)
        {
            if (isLowerJoint)
            {
                connectedGearLower = gearSurroundingJoint.GetComponentInParent<Gear>();
            }
            else
            {
                connectedGearUpper = gearSurroundingJoint.GetComponentInParent<Gear>();
            }

        }
        else
        {
            if (isLowerJoint)
            {
                connectedGearLower = null;
            }
            else
            {
                connectedGearUpper= null;
            }
        }
        //change this if else statement
    }


    public void CheckValidPosition() //fix this later
    {
        CheckCorrectLayer();
        CheckBothJoint();
        if(connectedGearLower != null)
        {
            transform.position = connectedGearLower.transform.position;
        }
        else if (connectedGearUpper != null)
        {
            Vector3 newPosition = connectedGearUpper.transform.position;
            newPosition.z = newPosition.z + 3; // this makes the joint remain at the same position
            transform.position = newPosition;
        }
    }

    private void CheckCorrectLayer()
    {
        int maxLayer= GameManager.instance.currentGameData.NumberOfLayers;
        int currentLayer = LayerManager.instance.CurrentLayer;
        if(currentLayer == maxLayer)
        {
            transform.Translate(0, 0, +3); // basically moving one layer down
        }
        else //this line of code might be problematic since it keeps the layer when deleted...
        {
            int difference = currentLayer - layer;
            transform.Translate(0, 0, 3 * difference);
        }
    } //does the checking and making sure the joint is under the same layer

    public void Move(Vector3 position)
    {
        if(layer == 0) 
        {
            layer = LayerManager.instance.CurrentLayer;
        }
        transform.position = position;
    }


}