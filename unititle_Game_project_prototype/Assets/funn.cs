using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class funn : MonoBehaviour
{
    public string name;
    private Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        print("Hello "+name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        print("Bye " + name);
    }
}
