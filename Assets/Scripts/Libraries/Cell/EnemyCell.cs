using System.Collections.Generic;
using UnityEngine;


public class EnemyCell : Cell
{
    private HashSet<GameObject> _cellsInVision = new HashSet<GameObject>();

    ///  <inheritdoc>
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

    ///  <inheritdoc>
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

    ///  <inheritdoc>
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

    ///  <inheritdoc>
    public override void MakeDecision()
    {
        var position = thisTransform.position; 
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

