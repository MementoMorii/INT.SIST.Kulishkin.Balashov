using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        //определяем напрвление, бежать от врага, и направление на еду, и берём направление между ними.
        //добавляем в эту модель мутабельный коэффициент смещения к еде или от врага, от которого зависит к какому
        //напрвлению будет ближе результирующее направление.

    }

    /// <summary>
    /// Метод определения ближайшей точки к текущей точке.
    /// </summary>
    /// <param name="curPosition">Текущая точка.</param>
    /// <param name="gameObjects">Список точек.</param>
    /// <returns></returns>
    private Vector3 getNearestPosition(Vector3 curPosition, HashSet<GameObject> gameObjects)
    {
        float minDistance = getDistance(curPosition, gameObjects.ElementAt(0).transform.position);
        var nearestPosition = gameObjects.ElementAt(0).transform.position;
        foreach (var gameObj in gameObjects)
        {
            var gameObjPosition = gameObj.transform.position;
            var curFoodDistance = getDistance(curPosition, gameObjPosition);
            if (curFoodDistance <= minDistance)
            {
                minDistance = curFoodDistance;
                nearestPosition = gameObjPosition;
            }
        }
        return nearestPosition;
    }
    /// <summary>
    /// Метод определения расстояния между двумя точками.
    /// </summary>
    /// <param name="curPosition"> Текущая точка.</param>
    /// <param name="targetPosition">Целевая точка.</param
    /// <returns></returns>
    private float getDistance(Vector3 curPosition, Vector3 targetPosition)
    {
        var vector = curPosition - targetPosition;
        return vector.magnitude;
    }

    /// <summary>
    /// Метод определения угла.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private float getAngle(Vector3 start, Vector3 end)
    {
        return 0.1f;
    }
}

