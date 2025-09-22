using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    private float HorSpeed;
    [SerializeField] private float Imp;
    public Animator animator; // для анимаций
    private AnimatorController _animatorController;

    private bool isGround;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animatorController = GetComponentInChildren<AnimatorController>();
    }

    private void FixedUpdate()
    {
        transform.Translate(HorSpeed, 0, 0);
        animator.SetFloat("Speed", Mathf.Abs (HorSpeed));
    }


    public void right()
    {
        HorSpeed = Speed;
        transform.localScale = new Vector3(6, 6, 1); // Смотрим вправо

    }
    public void left()
    {
        HorSpeed = -Speed;
        transform.localScale = new Vector3(-6, 6, 1); // Смотрим влево

    }
    public void up()
    {
        if(isGround)
        rb.AddForce(new Vector2(0, Imp), ForceMode2D.Impulse);
    }
    public void Stop()
    {
        HorSpeed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            isGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
            isGround = false;
    }

    void Flip()
    {
        if (Input.GetButtonDown("left"))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    
}
