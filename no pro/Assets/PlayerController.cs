using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //  �����̴� �ӵ�
    private float Speed;
    private Vector3 Movement;

    private Animator animator;

    private bool onAttack;
    private bool onHit;
    private bool onJump;
    private bool onRolling;
    private bool onClimbing;


    //  ����Ƽ �⺻ ���� �Լ�
    //  �ʱⰪ�� ������ �� ���
    void Start()
    {
        //  �ӵ��� �ʱ�ȭ.
        Speed = 5.0f;

        //  player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        onAttack = false;

        onHit = false;

        onJump = false;

        onRolling = false;

        onClimbing = false;

    }

    //  ����Ƽ �⺻ ���� �Լ�
    //  �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        //  [�Ǽ� ���� IEEE754]

        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
        float Ver = Input.GetAxis("Vertical"); // -1 ~ 1 ���� �Ǽ��� ��ȯ.

        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);

        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();

        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();

        if (Input.GetKey(KeyCode.Space))
            OnJump();

        if (Input.GetKey(KeyCode.E))
            OnRolling();

        if (Input.GetKey(KeyCode.W))
            OnClimbing();

        if (Input.GetKey(KeyCode.S))
            OnClimbing();

        animator.SetFloat("Speed", Hor);
        transform.position += Movement;
    }


    private void OnAttack()
    {
        if (onAttack)
            return;

        onAttack = true;
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        onAttack = false;
    }

    private void OnHit()
    {
        if (onHit)
            return;

        onHit = true;
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        onHit = false;
    }

    private void OnJump()
    {
        if (onJump)
            return;

        onJump = true;
        animator.SetTrigger("Jump");
    }

    private void SetJump()
    {
        onJump = false;
    }

    private void OnRolling()
    {
        if (onRolling)
            return;

        onRolling = true;
        animator.SetTrigger("Rolling");
    }

    private void SetRolling()
    {
        onRolling = false;
    }

    private void OnClimbing()
    {
        if (onRolling)
            return;

        onRolling = true;
        animator.SetTrigger("Rolling");
    }

    private void SetClimbing()
    {
        onRolling = false;
    }
}