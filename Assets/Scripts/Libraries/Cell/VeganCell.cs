using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VeganCell : Cell
{
    private HashSet<GameObject> _foodInVision = new HashSet<GameObject>();
    private HashSet<GameObject> _enemyInVision = new HashSet<GameObject>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Destroy(gameObject);
                break;

            case "Food":
                Energy += 15;
                break;

            default:
                break;

        }
    }

    /// <summary>
    /// Метод прниятия решения о напрвлении движения.
    /// </summary>
    public override void makeDecision()
    {
        var position = thisTransform.position;

        var nearestFoodPosition = getNearestPosition(position, _foodInVision);
        var nearestEnemyPosition = getNearestPosition(position, _enemyInVision);
        var foodAngle = getFoodAngle(position, nearestFoodPosition);
        var fromEnemyAngle = getEnemyAngle(position, nearestEnemyPosition);
        var directionAngle = getDirectionAngle(foodAngle, fromEnemyAngle) / (2 * Mathf.PI);
        moove(directionAngle);
    }

    /// <summary>
    /// Метод определения угла направления на еду.
    /// </summary>
    /// <param name="curPosition"></param>
    /// <param name="targetFoodPosition"></param>
    /// <returns>Угол направления на еду в радианах</returns>
    private float getFoodAngle(Vector3 curPosition, Vector3 targetFoodPosition)
    {
        var directionVector = targetFoodPosition - curPosition;
        return Mathf.Atan2(directionVector.y, directionVector.x);
    }

    /// <summary>
    /// Метод определения угла направления от Enemy.
    /// </summary>
    /// <param name="curPosition"></param>
    /// <param name="targetEnemyPosition"></param>
    /// <returns>Угол направления от Enemy в радианах</returns>
    private float getEnemyAngle(Vector3 curPosition, Vector3 targetEnemyPosition)
    {
        var directionVector = targetEnemyPosition - curPosition;
        var angle = Mathf.Atan2(directionVector.y, directionVector.x) + Mathf.PI;
        return angle < 2 * Mathf.PI ? angle : (angle - 2 * Mathf.PI);
    }

    /// <summary>
    /// Метод определения угла направления между едой и направлением от Enemy.
    /// </summary>
    /// <param name="foodAngle"></param>
    /// <param name="fromEnemyAngle"></param>
    /// <returns>Угол направления в радианах</returns>
    private float getDirectionAngle(float foodAngle, float fromEnemyAngle)
    {
        return foodAngle < fromEnemyAngle ? foodAngle + (fromEnemyAngle - foodAngle) * BetweenFoodEnemyCoefAngle :
            fromEnemyAngle + (foodAngle - fromEnemyAngle) * BetweenFoodEnemyCoefAngle;
    }
}

