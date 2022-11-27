using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public int maxHP = 100;
    public int currentHP;

    public Rigidbody2D _body;
    public GameObject leftArm;
    public GameObject rightArm;

    //public HealthBar healthBar;

    private float deltaX;
    private float deltaY;
    private float topBound = -0.6f;
    private float botBound = -3.5f;
    private bool isPunching = false;


    private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        currentHP = maxHP;
        //healthBar.SetMaxHealth(maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        PlayerPunch();
        MovmentAnimation();
        playerDeath();
    }

    void MovePlayer()
    {
        if (!isPunching)
        {
            deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }

        else
        {
            deltaX = 0;
            deltaY = 0;
        }

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

    void MovmentAnimation()
    {
        _anim.SetFloat("speed", Mathf.Abs(deltaX + deltaY));
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector2(Mathf.Sign(deltaX) * 3, 3);
        }

        //_anim.SetFloat("impulse", Mathf.Abs(_body.velocity.y));
    }

    void PlayerPunch()
    {
        if (Input.GetKeyDown("z") && !isPunching)
        {
            //_anim.SetBool("isLightPunchButtonPressed", true);

            //transform.localScale = new Vector2(3, 3);

            //_anim.SetBool("isLightPunchButtonPressed", false);

            //Debug.Log("Light punch");

            StartCoroutine(LightPunch());
        }

        else if (Input.GetKeyDown("x") && !isPunching)
        {
            StartCoroutine(HeavyPunch());
        }
    }

    IEnumerator LightPunch()
    {
        isPunching = true;
        _anim.SetBool("isLightPunchButtonPressed", true);
        yield return new WaitForSeconds(0.06f);
        leftArm.SetActive(true);

        //if (!Mathf.Approximately(deltaX, 0))
        //{
        //    transform.localScale = new Vector2(Mathf.Sign(deltaX) * 3, 3);
        //}

        yield return new WaitForSeconds(0.04f);

        _anim.SetBool("isLightPunchButtonPressed", false);
        leftArm.SetActive(false);
        isPunching = false;

        Debug.Log("Light punch");
    }

    IEnumerator HeavyPunch()
    {
        isPunching = true;
        _anim.SetBool("isHeavyPunchButtonPressed", true);
        yield return new WaitForSeconds(0.5f);
        rightArm.SetActive(true);

        //if (!Mathf.Approximately(deltaX, 0))
        //{
        //    transform.localScale = new Vector2(Mathf.Sign(deltaX) * 3, 3);
        //}

        yield return new WaitForSeconds(0.3f);

        _anim.SetBool("isHeavyPunchButtonPressed", false);
        rightArm.SetActive(false);
        isPunching = false;

        Debug.Log("Heavy punch");
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Enemy arm"))
    //    {
    //        currentHP -= 1;
    //        //healthBar.SetHealth(currentHP);
    //    }
    //}

    void playerDeath()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
