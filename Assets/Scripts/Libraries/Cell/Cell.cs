using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private HashSet<int> _triggerObjects = new HashSet<int>();
    private HashSet<int> _colidedObjects = new HashSet<int>();
    private Reproduction _reproduction = new Reproduction();

    public float Speed { get; set; }
    public float Vision { get; set; }
    public float Energy { get; set; }
    public int LifeExpectancy { get; set; }

    private float _speedEnergyConsumptionCoef = 1.5f;
    private float _radiusEnergyConsumptionCoef = 1.2f;

    protected Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        LifeExpectancy = 0;
        Energy = 70;
        Speed = .1f;
        Vision = .1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Energy >= 80)
            _reproduction.reproduct(gameObject);
            return; // вызывается метод размножения и в него передаётся gameObject Reproduction(gameObgect)

        //todo возможно надо переопределить этот метод в наследниках чтобы передать в него видимую еду и врагов
        //или других клеток в случае EnemyCell, в этом месте вызывается метод поведения или принятия решения
        //в котором уже будет вызываться метод moove ну или прямо там движение прописанно будет.

        Energy -= Vision * _radiusEnergyConsumptionCoef * Time.deltaTime;

        if (Energy <= 0)
            Destroy(gameObject);
    }

    protected void moove(float angle)
    {
        thisTransform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime * Mathf.Cos(2 * Mathf.PI * angle);
        thisTransform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime * Mathf.Sin(2 * Mathf.PI * angle);
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
}
