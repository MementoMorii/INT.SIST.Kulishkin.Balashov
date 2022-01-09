using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VeganCell : Cell
{
    private HashSet<GameObject> _foodInVision = new HashSet<GameObject>();
    private HashSet<GameObject> _enemyInVision = new HashSet<GameObject>();

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Food":
                _foodInVision.Add(collision.gameObject);
                break;

            case "Enemy":
                _enemyInVision.Add(collision.gameObject);
                break;
            
            default:
                break;
        }

    }

    virtual public void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Food":
                _foodInVision.Remove(collision.gameObject);
                break;

            case "Enemy":
                _enemyInVision.Remove(collision.gameObject);
                break;
            
            default:
                break;
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                //todo либо в классе Enemy прописать метод дамага, либо тут отниать у клетки энергию за столкновение с врагом.
                break;

            case "Food":
                Energy += 15;
                break;

            default:
                break;

        }
    }

}

