using UnityEngine;


public class Reproduction
{
    private int _countChilds = 2;
    private int _coountOfNotMutableChilds = 0;

    /// <summary>
    /// Метод размножения клетки.
    /// </summary>
    /// <param name="gameObject"></param>
    public void Reproduct(Cell cell)
    {
        for(int i = 0; i < _countChilds; i++)
        {
            if(i < _coountOfNotMutableChilds)
            {
                MonoBehaviour.Instantiate(cell);
                continue;
            }

            MonoBehaviour.Instantiate(Mutate(cell));
        }      
    }

    /// <summary>
    /// Метод для мутирования параметров.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public Cell Mutate(Cell cell)
    {
        cell.Speed += cell.Speed * Random.Range(-1, 1) * cell.MutationCoef;
        cell.Vision += cell.Vision * Random.Range(-1, 1) * cell.MutationCoef;
        Debug.Log("speed = " + cell.Speed+ " vision = "+ cell.Vision);
        if (cell.Vision < 0)
            cell.Vision = 0;
        cell.SetVision();

        cell.BetweenFoodEnemyCoefAngle = cell.Vision + Random.Range(-1, 1) * cell.MutationCoef;
        if (cell.BetweenFoodEnemyCoefAngle > 1)
            cell.BetweenFoodEnemyCoefAngle = 1;
        if (cell.BetweenFoodEnemyCoefAngle < 0)
            cell.BetweenFoodEnemyCoefAngle = 0;

        cell.MutationCoef += cell.MutationCoef * Random.Range(-1, 1) * cell.MutationCoef;
        if (cell.MutationCoef > 0.1f)
            cell.MutationCoef = 0.1f;
        if (cell.MutationCoef < 0)
            cell.MutationCoef = 0;
        return cell;
    }
}

