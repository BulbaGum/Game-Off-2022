using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemySpeed = 100f;
    public int hp = 4;
    public int scaleX;
    public int scaleY;
    public GameObject leftArm;
    public GameObject rightArm;
    public Rigidbody2D enemyRb;
    public Transform target;

    private float topBound = -0.6f;
    private float botBound = -3.5f;
    private bool isPunching = false;
    private Vector2 moveDirection;

    private AudioSource impact;
    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        impact = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();
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
            transform.localScale = new Vector2(Mathf.Sign(direction.x) * scaleX, scaleY);
        }
        else
        {
            enemyRb.velocity = new Vector2(0, 0);
        }

        if (transform.position.y > topBound)
        {
            transform.position = new Vector2(transform.position.x, topBound);
        }
        else if (transform.position.y < botBound)
        {
            transform.position = new Vector2(transform.position.x, botBound);
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

        if (collision.gameObject.CompareTag("Player") && !isPunching)
        {
            int randomPunch = Random.Range(0, 10);

            if(randomPunch < 7)
            {
                StartCoroutine(LightPunch());
            }

            else
            {
                StartCoroutine(HeavyPunch());
            }
        }
    }

    IEnumerator LightPunch()
    {
        isPunching = true;
        _anim.SetBool("isLightPunching", true);
        impact.Play();
        yield return new WaitForSeconds(0.06f);
        leftArm.SetActive(true);

        yield return new WaitForSeconds(0.04f);

        _anim.SetBool("isLightPunching", false);
        leftArm.SetActive(false);
        isPunching = false;

        Debug.Log("Light punch");
    }

    IEnumerator HeavyPunch()
    {
        isPunching = true;
        _anim.SetBool("isHeavyPunching", true);
        yield return new WaitForSeconds(0.5f);
        impact.Play();
        rightArm.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        _anim.SetBool("isHeavyPunching", false);
        rightArm.SetActive(false);
        isPunching = false;

        Debug.Log("Heavy punch");
    }
}
