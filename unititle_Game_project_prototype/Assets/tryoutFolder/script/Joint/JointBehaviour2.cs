using System.Collections;
using UnityEngine;
using Assets.tryoutFolder.script;
using System.Linq;
using static UnityEditor.Experimental.GraphView.GraphView;

public class JointBehaviour2 : RotatableElement , IMoveable
{
    [SerializeField] private CircleCollider2D lowerJoint;
    [SerializeField] private CircleCollider2D upperJoint;
    [SerializeField] private int cost;
    [SerializeField] private string name;
    private float radius;
    public int Cost => cost;
    private int layer;

    private RotatableElement connectedGearLower;
    private RotatableElement connectedGearUpper;

    public GameObject Getprefab => gameObject;

    public string Name => name;

    protected override void FindingRotatingElement()
    {
        RotatableElement[] lowerJointElements = GetElementsFromJoint(lowerJoint);
        RotatableElement[] upperJointElements = GetElementsFromJoint(upperJoint);
        surroundingElements = lowerJointElements.Concat(upperJointElements).ToArray();
    }


    private RotatableElement[] GetElementsFromJoint(Collider2D joint)
    {
        float minDept = joint.transform.position.z - 0.3f;
        float maxDept = joint.transform.position.z + 0.3f;
        return new RotatableElement[] {
            FindRotatableElementFromJoint(minDept,maxDept, LayerData.GearAreaLayer),
            FindRotatableElementFromJoint(minDept, maxDept, LayerData.JointLayer) };
    }

    private RotatableElement FindRotatableElementFromJoint(float minDept, float maxDept, int layer)
    {

        if (Physics2D.OverlapCircle(transform.position, radius, layer, minDept, maxDept)
            .TryGetComponent<RotatableElement>(out RotatableElement element))
        {
            return element;
        }
        else return null;




    }


    private void CheckBothJoint()
    {
        CheckJoint(lowerJoint, true);
        CheckJoint(upperJoint, false);   //look at both joint
    }

    private void CheckJoint(Collider2D joint, bool isLowerJoint)
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
                connectedGearLower = gearSurroundingJoint.GetComponentInParent<RotatableElement>();
            }
            else
            {
                connectedGearUpper = gearSurroundingJoint.GetComponentInParent<RotatableElement>();
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
                connectedGearUpper = null;
            }
        }
        //change this if else statement
    }

    public void Move(Vector3 position)
    {
        if (layer == 0)
        {
            layer = LayerManager.instance.CurrentLayer;
        }
        transform.position = position;
    }

    private void CheckCorrectLayer()
    {
        int maxLayer = GameManager.instance.currentGameData.NumberOfLayers;
        int currentLayer = LayerManager.instance.CurrentLayer;
        if (currentLayer == maxLayer)
        {
            transform.Translate(0, 0, +3); // basically moving one layer down
        }
        else //this line of code might be problematic since it keeps the layer when deleted...
        {
            int difference = currentLayer - layer;
            transform.Translate(0, 0, 3 * difference);
        }
    } //does the checking and making sure the joint is under the same layer

    public void CheckValidPosition() //fix this later
    {
        CheckCorrectLayer();
        CheckBothJoint();
        if (connectedGearLower != null)
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




}
