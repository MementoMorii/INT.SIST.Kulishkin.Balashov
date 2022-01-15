using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_vision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<EnemyCell>().VisionTriggerEnter(collision);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.GetComponent<EnemyCell>().VisionTriggerExit(collision);
    }
}
