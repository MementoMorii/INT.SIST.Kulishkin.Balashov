using System.Collections.Generic;
using UnityEngine;


public class EnemyCell : Cell
{
    private HashSet<GameObject> _cellsInVision = new HashSet<GameObject>();

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

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Cell":
                Energy += 15;
                //todo либо тут вызывать метод дамага, либо у самой клетки просто отнимать энергию.
                break;

            default:
                break;

        }
    }
}

