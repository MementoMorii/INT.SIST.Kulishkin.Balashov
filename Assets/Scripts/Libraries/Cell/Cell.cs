using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private HashSet<int> _triggerObjects = new HashSet<int>();
    private HashSet<int> _colidedObjects = new HashSet<int>();

    public double Speed { get; set; }
    public double Vision { get; set; }
    public double Energy { get; set; }
    public int LifeExpectancy { get; set; }

    private double _speedEnergyConsumptionCoef = 1.5f;
    private double _radiusEnergyConsumptionCoef = 1.2f;

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
            return; // вызывается метод размножения и в него передаётся gameObject Reproduction(gameObgect)

        //todo возможно надо переопределить этот метод в наследниках чтобы передать в него видимую еду и врагов
        //или других клеток в случае EnemyCell, в этом месте вызывается метод поведения или принятия решения
        //в котором уже будет вызываться метод moove ну или прямо там движение прописанно будет.

        Energy -= Vision * _radiusEnergyConsumptionCoef;

        if (Energy <= 0)
            Destroy(gameObject, .5f);
    }
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
