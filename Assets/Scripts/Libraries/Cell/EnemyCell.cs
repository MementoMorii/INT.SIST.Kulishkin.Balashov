using System.Collections.Generic;
using UnityEngine;


public class EnemyCell : Cell
{
    private HashSet<GameObject> _cellsInVision = new HashSet<GameObject>();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Cell":
                _cellsInVision.Add(collision.gameObject);
                break;
            
            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    virtual public void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Cell":
                _cellsInVision.Remove(collision.gameObject);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Cell":
                Energy += 25;
                break;

            default:
                break;

        }
    }
}

