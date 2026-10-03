using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class Geo_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private string Var2 = "Hello ";

    int Var3 = 3;

    void Start()
    {
        Debug.Log(Var2 + "World");
        Var2 = "Goodbye";
        Debug.Log(Var2 + "TEXT");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Var3);
        Var3++;

        // This addssss a 0.005 units along the X axis every sngle frame to move the object right for inf time.
        transform.position += new Vector3(0.005f, 0, 0);

        // Detects when the W key is pressed once and sets position
        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector3(0, 1, 0);
        }
    }
}