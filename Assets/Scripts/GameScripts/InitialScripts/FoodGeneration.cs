using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGeneration : MonoBehaviour
{
    public GameObject foodPrefab;
    float foodSpawnPeriod = 0.1f;
    float foodSpawnElapsedTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foodSpawnElapsedTime += Time.deltaTime;
        if(foodSpawnElapsedTime > foodSpawnPeriod)
        {
            foodSpawnElapsedTime = 0f;
            SpawnFood();
        }
    }

    private void SpawnFood()
    {
        Vector3 position = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), 0);
        MonoBehaviour.Instantiate(foodPrefab, position, Quaternion.identity);
    }
}
