using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Cell : MonoBehaviour
{
    
    private Reproduction _reproduction = new Reproduction(); 
    public float MutationCoef { get; set; }
    public float Speed { get; set; }
    public float Vision { get; set; }
    public float Energy { get; set; }
    public int LifeExpectancy { get; set; }
    
    /// <summary>
    /// ����������� ��� ������������� ���������� ����� ������������ �� ��� � �� Enemy, 0 - �� ���, 1 - �� Enemy.
    /// </summary>
    public float BetweenFoodEnemyCoefAngle { get; set; } 

    public float _speedEnergyConsumptionCoef { get; set; } = 0.5f;
    public float _radiusEnergyConsumptionCoef { get; set; } = 0.5f;

    protected Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {

        SetParams();
        _reproduction.Mutate(this);
        var visionColider = thisTransform.Find("Vision_colider").gameObject.GetComponent<CircleCollider2D>();
        visionColider.radius = Vision;
    }

    // Update is called once per frame
    void Update()
    {
        if (Energy >= 80)
        {
            // ���������� ����� ����������� � � ���� ��������� gameObject Reproduction(gameObgect)
            _reproduction.Reproduct(this);
            Destroy(gameObject);
            return;
        }


        //��������� ������� �� Vision. ������� ��� ����� �����������������.
        Energy -= Vision * Vision * _radiusEnergyConsumptionCoef * Time.deltaTime;

        MakeDecision();

        if (Energy <= 0)
            Destroy(gameObject);
    }

    public virtual void SetParams()
    {
        thisTransform = transform;
        LifeExpectancy = 0;
        Energy = 70;
        BetweenFoodEnemyCoefAngle = 0.5f;
        Speed = 1f;
        Vision = 3f;
        MutationCoef = 0.2f;
    }

    /// <summary>
    /// M���� ������������� ����������� ������ � �������� ����������� � � ���������.
    /// </summary>
    /// <param name="angle">����������� �� 0 �� 1, 0 ��� ��� �, ����������� �������� ������ �������.</param>
    public void Moove(float angle)
    {
        thisTransform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime * Mathf.Cos(2 * Mathf.PI * angle);
        thisTransform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime * Mathf.Sin(2 * Mathf.PI * angle);
        Energy -= ((Speed * Speed) / 2) * (Time.deltaTime * _speedEnergyConsumptionCoef);
    }

    /// <summary>
    /// ����� �������� ������� � ����������� ��������.
    /// </summary>
    abstract public void MakeDecision();

    ///// <summary>
    ///// ����� ������������� ��� ��������� �������� � ������ ������.
    ///// </summary>
    ///// <param name="collision"></param>
    //abstract public void OnTriggerEnter2D(Collider2D collision);

    ///// <summary>
    ///// ����� ������������� ��� ������ �������� �� ������ ������.
    ///// </summary>
    ///// <param name="collision"></param>
    //abstract public void OnTriggerExit2D(Collider2D collision);

    ///// <summary>
    ///// ����� ������������� ��� ������������ ������ � ������ ��������.
    ///// </summary>
    ///// <param name="collision"></param>
    //abstract public void OnCollisionEnter2D(Collision2D collision);

    /// <summary>
    /// ����� ����������� ��������� ����� � ������� �����.
    /// </summary>
    /// <param name="curPosition">������� �����.</param>
    /// <param name="gameObjects">������ �����.</param>
    /// <returns></returns>
    public Vector3 GetNearestPosition(Vector3 curPosition, HashSet<GameObject> gameObjects)
    {

        var vector = gameObjects.Select(
            obj => new { Distance = GetDistance(curPosition, obj.transform.position),
                         Position = obj.transform.position
                       })
            .OrderBy(obj => obj.Distance)
            .Select(obj => obj.Position)
            .First();
        return vector;
        //float minDistance = GetDistance(curPosition, gameObjects.ElementAt(0).transform.position);
        //var nearestPosition = gameObjects.ElementAt(0).transform.position;
        //foreach (var gameObj in gameObjects)
        //{
        //    var gameObjPosition = gameObj.transform.position;
        //    var curFoodDistance = GetDistance(curPosition, gameObjPosition);
        //    if (curFoodDistance <= minDistance)
        //    {
        //        minDistance = curFoodDistance;
        //        nearestPosition = gameObjPosition;
        //    }
        //}
        //return nearestPosition;
    }

    /// <summary>
    /// ����� ������� ������� ����������.
    /// </summary>
    public void SetVision()
    {
        var visionColider = thisTransform.Find("Vision_colider").gameObject.GetComponent<CircleCollider2D>();
        visionColider.radius = Vision;
    }

    /// <summary>
    /// ����� ����������� ���������� ����� ����� �������.
    /// </summary>
    /// <param name="curPosition"> ������� �����.</param>
    /// <param name="targetPosition">������� �����.</param
    /// <returns></returns>
    private float GetDistance(Vector3 curPosition, Vector3 targetPosition)
    {
        var vector = curPosition - targetPosition;
        return vector.magnitude;
    }
}
