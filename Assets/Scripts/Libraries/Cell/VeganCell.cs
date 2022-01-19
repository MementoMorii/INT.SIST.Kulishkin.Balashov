using System.Collections.Generic;
using UnityEngine;


public class VeganCell : Cell
{
    private HashSet<GameObject> _foodInVision = new HashSet<GameObject>();
    private HashSet<GameObject> _enemyInVision = new HashSet<GameObject>();

    float movingTime = 0;
    float movingLimitTime = 4;
    float moveVector = 0.5f;

    public void VisionTriggerExit(Collider2D collision)
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
    public void VisionTriggerEnter(Collider2D collision)
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
    public void OnCollisionEnter2D(Collision2D collision)
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
        movingTime += Time.deltaTime;
        if (_foodInVision.Count == 0 && _enemyInVision.Count == 0)
        {
            if(movingTime > movingLimitTime)
            {
                moveVector = Random.Range(0f, 1.0f);
                movingTime = 0;
            }
            Moove(moveVector);
            return;
        }

        var position = thisTransform.position;
        Vector3 nearestFoodPosition;
        Vector3 nearestEnemyPosition;
        float foodAngle = 0f;
        float fromEnemyAngle = 0f;
        if (_foodInVision.Count != 0)
        {
            
            nearestFoodPosition = GetNearestPosition(position, _foodInVision);
            foodAngle = GetFoodAngle(position, nearestFoodPosition);
            if(_enemyInVision.Count == 0)
            {
                Moove(foodAngle / (2 * Mathf.PI));
            }
        }

        if (_enemyInVision.Count != 0)
        {
            nearestEnemyPosition = GetNearestPosition(position, _enemyInVision);
            fromEnemyAngle = GetEnemyAngle(position, nearestEnemyPosition);
            if (_foodInVision.Count == 0)
            {
                Moove(fromEnemyAngle / (2 * Mathf.PI));
            }
        }
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

