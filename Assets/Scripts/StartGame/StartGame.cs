using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<Rigidbody>();
        cube.transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
