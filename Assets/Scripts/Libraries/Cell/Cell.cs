using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Cell : MonoBehaviour
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

    private float _speedEnergyConsumptionCoef = 1f;
    private float _radiusEnergyConsumptionCoef = 1f;

    protected Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        LifeExpectancy = 0;
        Energy = 70;
        BetweenFoodEnemyCoefAngle = 0.5f;
        Speed = 20f;
        Vision = 100f;
        MutationCoef = 0.1f;

        _reproduction.Mutate(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Energy >= 80)
        {
            // ���������� ����� ����������� � � ���� ��������� gameObject Reproduction(gameObgect)
            _reproduction.Reproduct(gameObject);
            return;
        }
            

        //��������� ������� �� Vision. ������� ��� ����� �����������������.
        Energy -= Vision * Vision * _radiusEnergyConsumptionCoef * Time.deltaTime;

        MakeDecision();

        if (Energy <= 0)
            Destroy(gameObject);
    }

    /// <summary>
    /// M���� ������������� ����������� ������ � �������� ����������� � � ���������.
    /// </summary>
    /// <param name="angle">����������� �� 0 �� 1, 0 ��� ��� �, ����������� �������� ������ �������.</param>
    protected void Moove(float angle)
    {
        thisTransform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime * Mathf.Cos(2 * Mathf.PI * angle);
        thisTransform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime * Mathf.Sin(2 * Mathf.PI * angle);
        Energy -= Speed * Speed / 2 * Time.deltaTime * _speedEnergyConsumptionCoef;
    }

    /// <summary>
    /// ����� �������� ������� � ����������� ��������.
    /// </summary>
    virtual public void MakeDecision() { }

    /// <summary>
    /// ����� ������������� ��� ��������� �������� � ������ ������.
    /// </summary>
    /// <param name="collision"></param>
    virtual public void OnTriggerEnter2D(Collider2D collision) { }

    /// <summary>
    /// ����� ������������� ��� ������ �������� �� ������ ������.
    /// </summary>
    /// <param name="collision"></param>
    virtual public void OnTriggerExit2D(Collider2D collision) { }

    /// <summary>
    /// ����� ������������� ��� ������������ ������ � ������ ��������.
    /// </summary>
    /// <param name="collision"></param>
    virtual public void OnCollisionEnter2D(Collision2D collision) { }

    /// <summary>
    /// ����� ����������� ��������� ����� � ������� �����.
    /// </summary>
    /// <param name="curPosition">������� �����.</param>
    /// <param name="gameObjects">������ �����.</param>
    /// <returns></returns>
    public Vector3 GetNearestPosition(Vector3 curPosition, HashSet<GameObject> gameObjects)
    {
        float minDistance = GetDistance(curPosition, gameObjects.ElementAt(0).transform.position);
        var nearestPosition = gameObjects.ElementAt(0).transform.position;
        foreach (var gameObj in gameObjects)
        {
            var gameObjPosition = gameObj.transform.position;
            var curFoodDistance = GetDistance(curPosition, gameObjPosition);
            if (curFoodDistance <= minDistance)
            {
                minDistance = curFoodDistance;
                nearestPosition = gameObjPosition;
            }
        }
        return nearestPosition;
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
