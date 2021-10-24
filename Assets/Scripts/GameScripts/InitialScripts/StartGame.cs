using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Texture2D CellSprite;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var circle = new GameObject();
            circle.AddComponent<SpriteRenderer>();
            var sr = circle.GetComponent<SpriteRenderer>();
            sr.sprite = Sprite.Create(CellSprite, new Rect(0f, 0f, CellSprite.width, CellSprite.height), new Vector2(0.5f, 0.5f), 100f);
            sr.color = Color.red;

            circle.AddComponent<CircleCollider2D>();
            circle.AddComponent<Rigidbody2D>();
            circle.GetComponent<Transform>().position = new Vector2(i*2, i*3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
