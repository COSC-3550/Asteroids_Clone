using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float speed = 20f;
    public float maxLifeTime = 30f;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    private void Awake() 
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody2D.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }
}
