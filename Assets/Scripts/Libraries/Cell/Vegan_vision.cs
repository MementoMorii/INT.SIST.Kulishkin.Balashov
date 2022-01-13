using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegan_vision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider2D collision)
    {
        transform.parent.GetComponent<VeganCell>().VisionTriggerEnter(collision);
    }

    void OnTriggerExit(Collider2D collision)
    {
        transform.parent.GetComponent<VeganCell>().VisionTriggerExit(collision);
    }
}
