using System.Collections.Generic;
using UnityEngine;


public class VeganCell : Cell
{
    private HashSet<GameObject> _foodInVision = new HashSet<GameObject>();
    private HashSet<GameObject> _enemyInVision = new HashSet<GameObject>();

    ///  <inheritdoc>
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

    ///  <inheritdoc>
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

    ///  <inheritdoc>
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

    ///  <inheritdoc>
    public override void MakeDecision()
    {
        var position = thisTransform.position;

        var nearestFoodPosition = GetNearestPosition(position, _foodInVision);
        var nearestEnemyPosition = GetNearestPosition(position, _enemyInVision);
        var foodAngle = GetFoodAngle(position, nearestFoodPosition);
        var fromEnemyAngle = GetEnemyAngle(position, nearestEnemyPosition);
        var directionAngle = GetDirectionAngle(foodAngle, fromEnemyAngle) / (2 * Mathf.PI);
        Moove(directionAngle);
    }

    /// <summary>
    /// Метод определения угла направления на еду.
    /// </summary>
    /// <param name="curPosition"></param>
    /// <param name="targetFoodPosition"></param>
    /// <returns>Угол направления на еду в радианах</returns>
    private float GetFoodAngle(Vector3 curPosition, Vector3 targetFoodPosition)
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
    private float GetEnemyAngle(Vector3 curPosition, Vector3 targetEnemyPosition)
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
    private float GetDirectionAngle(float foodAngle, float fromEnemyAngle)
    {
        return foodAngle < fromEnemyAngle ? foodAngle + (fromEnemyAngle - foodAngle) * BetweenFoodEnemyCoefAngle :
            fromEnemyAngle + (foodAngle - fromEnemyAngle) * BetweenFoodEnemyCoefAngle;
    }
}

