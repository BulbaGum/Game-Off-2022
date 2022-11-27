using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 100f;
    public int hp = 4;
    public Rigidbody2D enemyRb;
    public Transform target;

    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        enemyDeath();
    }

    void FollowPlayer()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            enemyRb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * enemySpeed * Time.deltaTime;
        }
        else
        {
            enemyRb.velocity = new Vector2(0, 0);
        }
    }

    void enemyDeath()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Left arm"))
        {
            hp -= 1;
            Debug.Log("Punch success");
        }

        else if (collision.gameObject.CompareTag("Right arm"))
        {
            hp -= 3;
            Debug.Log("Punch success");
        }
    }
}
