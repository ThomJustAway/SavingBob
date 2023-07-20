using System.Collections;
using UnityEngine;
using Assets.tryoutFolder.script;
using System.Linq;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Collections.Generic;

public class JointBehaviour : RotatableElement, IMoveable
{
    [SerializeField] private CircleCollider2D lowerJoint;
    [SerializeField] private CircleCollider2D upperJoint;
    [SerializeField] private string nameOfElement;
    [SerializeField] private int cost;
    private float radius;
    public int Cost => cost;
    private int layer;

    private RotatableElement connectedGearLower;
    private RotatableElement connectedGearUpper;

    public GameObject Getprefab => gameObject;

    public string Name => name;

    protected override RotatableElement[] FindingRotatingElement()
    {
        RotatableElement[] lowerJointElements = GetElementsFromJoint(lowerJoint);
        RotatableElement[] upperJointElements = GetElementsFromJoint(upperJoint);
        return lowerJointElements.Concat(upperJointElements).ToArray();
    }

    protected override void RotateSurroundingElements()
    {
        if (speed > 0)
        {
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
        float maxDept = joint.transform.position.z + 0.3f;
        RotatableElement connectedGear = FindRotatableGearFromJoint(minDept, maxDept);
        RotatableElement connectedJoint = FindRotatableJointFromJoint(minDept, maxDept, joint);

        List<RotatableElement> elements = new List<RotatableElement>();
        if (connectedGear != null )
        {
            elements.Add(connectedGear); 
        }
        if(connectedJoint != null )
        {
            elements.Add(connectedJoint);
        }

        return elements.ToArray();

    }


    private RotatableElement FindRotatableGearFromJoint(float minDept, float maxDept)
    {
        var collider = Physics2D.OverlapCircle(transform.position, radius, LayerData.GearAreaLayer, minDept, maxDept);
        if (collider != null)
        {
            return collider.GetComponentInParent<RotatableElement>();
        }
        return null;
    }

    private RotatableElement FindRotatableJointFromJoint(float minDept, float maxDept, Collider2D joint)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerData.JointLayer, minDept, maxDept);
        foreach (Collider2D collider in colliders)
        {
            if (collider != joint)
            {
                return collider.GetComponentInParent<RotatableElement>();
            }
        }
        return null;
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
        MusicManager.Instance.PlayMusicClip(SoundData.PlacingSound);
    }

}
