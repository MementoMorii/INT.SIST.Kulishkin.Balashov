using System.Collections.Generic;
using UnityEngine;


public class EnemyCell : Cell
{
    private HashSet<GameObject> _cellsInVision = new HashSet<GameObject>();

    ///  <inheritdoc>

    public void VisionTriggerExit(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Vegan":
                _cellsInVision.Remove(collision.gameObject);
                break;

            default:
                break;
        }
    }
    public void VisionTriggerEnter(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Vegan":
                _cellsInVision.Add(collision.gameObject);
                Debug.Log("Кол-во " + _cellsInVision.Count);
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
            case "Cell":
                Energy += 25;
                break;

            default:
                break;

        }
    }

    ///  <inheritdoc>
    public override void MakeDecision()
    {
        if(_cellsInVision.Count == 0)
        {
            Moove(Random.Range(0f, 1.0f));
            return;
        }
        var position = thisTransform.position;
        //Debug.Log(position.ToString());
        //Debug.Log("Клетки" + _cellsInVision.Count);
        var nearestCellPosition = GetNearestPosition(position, _cellsInVision);
        var angle = getCellAngle(position, nearestCellPosition) / (2 * Mathf.PI);
        Moove(angle);
    }

    /// <summary>
    /// Метод получения угла направления на клетку.
    /// </summary>
    /// <param name="curPosition"></param>
    /// <param name="targetCellPosition"></param>
    /// <returns>Угол направления на клетку в радианах.</returns>
    private float getCellAngle(Vector3 curPosition, Vector3 targetCellPosition)
    {
        var directionVector = targetCellPosition - curPosition;
        return Mathf.Atan2(directionVector.y, directionVector.x);
    }
}

