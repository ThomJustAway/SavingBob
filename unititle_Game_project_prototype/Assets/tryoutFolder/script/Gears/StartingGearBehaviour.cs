using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingGearBehaviour : MonoBehaviour, IRotate
{
    // Start is called before the first frame update
    private float speed;
    public float searchRadius;
    [SerializeField] private Collider2D thisEntireGearArea;
    [SerializeField] private Collider2D thisInnerGearArea;


    void Start()
    {
        thisEntireGearArea = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetGearsAroundRadius(searchRadius);
    }

    private void GetGearsAroundRadius(float radius)
    {
        Collider2D[] surroundingGears = Physics2D.OverlapCircleAll(transform.position, radius,(int)Layers.EntireGearArea);
        //get the entire gear area in the layer
        Vector3 maximumXPosition = new Vector3(transform.position.x + radius, transform.position.y);
        Vector3 maximumYPosition = new Vector3(transform.position.x, transform.position.y + radius);
        Debug.DrawLine(transform.position, maximumXPosition, Color.red);
        Debug.DrawLine(transform.position, maximumYPosition, Color.red);

        for (int i = 0; i < surroundingGears.Length; i++)
        {
            if (surroundingGears[i] != thisEntireGearArea)
            {
                print(surroundingGears[i]);
            }
        }
    }

    public void RotateGear(float speed, Vector3 direction)
    {
        transform.Rotate(direction * speed * Time.deltaTime);
    }
}
