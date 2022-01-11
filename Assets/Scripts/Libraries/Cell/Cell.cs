using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cell : MonoBehaviour
{
    private HashSet<int> _triggerObjects = new HashSet<int>();
    private HashSet<int> _colidedObjects = new HashSet<int>();
    private Reproduction _reproduction = new Reproduction();

    public float Speed { get; set; }
    public float Vision { get; set; }
    public float Energy { get; set; }
    public int LifeExpectancy { get; set; }
    
    /// <summary>
    /// Коэффициент для регулирования наравления между направлением на еду и от Enemy, 0 - на еду, 1 - от Enemy.
    /// </summary>
    public float BetweenFoodEnemyCoefAngle { get; set; } 

    private float _speedEnergyConsumptionCoef = 1.5f;
    private float _radiusEnergyConsumptionCoef = 1.2f;

    protected Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        LifeExpectancy = 0;
        Energy = 70;
        BetweenFoodEnemyCoefAngle = 0.5f;
        Speed = .1f;
        Vision = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Energy >= 80)
            _reproduction.reproduct(gameObject);
            return; // вызывается метод размножения и в него передаётся gameObject Reproduction(gameObgect)

        //отнимание энергии за Vision. Формулу ещё можно подкорректировать.
        Energy -= Vision * _radiusEnergyConsumptionCoef * Time.deltaTime;

        makeDecision();

        if (Energy <= 0)
            Destroy(gameObject);
    }

    protected void moove(float angle)
    {
        thisTransform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime * Mathf.Cos(2 * Mathf.PI * angle);
        thisTransform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime * Mathf.Sin(2 * Mathf.PI * angle);
        Energy -= Speed * Speed / 2 * Time.deltaTime;
    }

    virtual public void makeDecision() { }

    virtual public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.isTrigger == false)
           _triggerObjects.Add(collision.gameObject.GetInstanceID());
    }
    virtual public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            _triggerObjects.Remove(collision.gameObject.GetInstanceID());
    }
    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "InterractiveObject") return;
        _colidedObjects.Add(collision.gameObject.GetInstanceID());
        gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "InterractiveObject") return;
        _colidedObjects.Remove(collision.gameObject.GetInstanceID());
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    /// <summary>
    /// Метод определения ближайшей точки к текущей точке.
    /// </summary>
    /// <param name="curPosition">Текущая точка.</param>
    /// <param name="gameObjects">Список точек.</param>
    /// <returns></returns>
    public Vector3 getNearestPosition(Vector3 curPosition, HashSet<GameObject> gameObjects)
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
}
