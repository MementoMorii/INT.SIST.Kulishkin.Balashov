using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject veganPrefab;

    public Texture2D CellSprite;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
        //    Instantiate(enemyPrefab, position, Quaternion.identity);
        //}

        for (int i = 0; i < 10; i++)
        {
            Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
            Instantiate(veganPrefab, position, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
