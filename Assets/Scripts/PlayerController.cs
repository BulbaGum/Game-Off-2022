using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public int maxHP = 100;
    public int currentHP;
    public Rigidbody2D _body;
    //public HealthBar healthBar;

    private float deltaX;
    private float deltaY;
    private float topBound = -0.6f;
    private float botBound = -3.5f;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        //healthBar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        playerDeath();
    }

    void MovePlayer()
    {
        deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector2 movment = new Vector2(deltaX, deltaY);
        _body.velocity = movment;

        if (transform.position.y > topBound)
        {
            transform.position = new Vector2(transform.position.x, topBound);
        }
        else if (transform.position.y < botBound)
        {
            transform.position = new Vector2(transform.position.x, botBound);
        }
    }

    void playerDeath()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
