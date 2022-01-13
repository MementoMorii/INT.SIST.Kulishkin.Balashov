using UnityEngine;


public class Reproduction
{
    private int _countChilds = 2;
    private int _coountOfNotMutableChilds = 0;

    /// <summary>
    /// Метод размножения клетки.
    /// </summary>
    /// <param name="gameObject"></param>
    public void Reproduct(GameObject gameObject)
    {
        for(int i = 0; i < _countChilds; i++)
        {
            if(i < _coountOfNotMutableChilds)
                MonoBehaviour.Instantiate(gameObject);

            MonoBehaviour.Instantiate(gameObject);
        }      
    }

    /// <summary>
    /// Метод для мутирования параметров.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public void Mutate(Cell cell)
    {
        cell.Speed += cell.Speed * Random.Range(-3, 3) * cell.MutationCoef;
        cell.Vision += cell.Vision * Random.Range(-3, 3) * cell.MutationCoef;
        if (cell.Vision < 0)
            cell.Vision = 0;

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
    }
}

