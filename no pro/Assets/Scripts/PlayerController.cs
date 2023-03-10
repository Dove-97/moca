using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    // �����̴� �ӵ�
    private float Speed;

    // �������� �����ϴ� ����
    private Vector3 Movement;

    // �÷��̾��� Animator ������Ҹ� �޾ƿ��� ����
    private Animator animator;

    // �÷��̾��� SpirteRenderer ������Ҹ� �޾ƿ��� ����
    private SpriteRenderer playerRenderer;

    // ����üũ
    private bool onAttack; //���ݻ���
    private bool onHit; //�ǰݻ���
    private bool onJump;
    private bool onRolling;

    // ������ �Ѿ� ���� 
    public GameObject BulletPrefab;

    // ������ FX����
    public GameObject fxPrefab;

    public GameObject[] stageBack = new GameObject[7];

    // ������ �Ѿ��� �������
    private List<GameObject> Bullets = new List<GameObject>();

    // �÷��̾ ���������� �ٶ� ����
    private float Direction;

    // �÷��̾ �ٶ󺸴� ����
    public bool dirLeft;
    public bool dirRight;

    private void Awake()
    {
        //  player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();
        // Player�� SpriteRenderer�� �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();
    }

    //  ����Ƽ �⺻ ���� �Լ�
    //  �ʱⰪ�� ������ �� ���
    void Start()
    {
        //  �ӵ��� �ʱ�ȭ.
        Speed = 5.0f;
        
        onAttack = false;
        onHit = false;
        onJump = false;
        onRolling = false;
        Direction = 1.0f;



        for (int i = 0; i < 7; ++i)
            stageBack[i] = GameObject.Find(i.ToString());

    }

    //  ����Ƽ �⺻ ���� �Լ�
    //  �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        //  [�Ǽ� ���� IEEE754]

        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.


        // Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�. 
        if (Hor != 0)
            Direction = Hor;
        else
        {
            dirLeft = false;
            dirRight = false;
        }

        // �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� ���� ����
        if (Direction < 0)
        {

            playerRenderer.flipX = dirLeft = true;

            // ���� �÷��̾ �����δ�.
            transform.position += Movement;
        }
        else if(Direction > 0)
        {
            playerRenderer.flipX = false;
            dirRight = true;
        }
         

        // �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            0.0f,
            0.0f);


        // ���� ��Ʈ��Ű�� �Է��Ѵٸ� 
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack(); // ����

        // ���� ����ƮŰ�� �Է��Ѵٸ�
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit(); // �ǰ�

        if (Input.GetKey(KeyCode.Space))
            OnJump();

        if (Input.GetKey(KeyCode.E))
            OnRolling();


        // ���� ��Ʈ���� �Է��Ѵٸ�
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // �Ѿ� ������ �����Ѵ�.
            GameObject Obj = Instantiate(BulletPrefab);

            // ������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
            Obj.transform.position = transform.position;

            // �Ѿ��� BulletController ��ũ��Ʈ�� �޾ƿ´�
            BulletController Controller = Obj.AddComponent<BulletController>();

            // �Ѿ� ��ũ��Ʈ ������ ���� ������ ���� �÷��̾��� ���� ������ �����Ѵ�.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);



            // �Ѿ��� SpriteRenderer�� �޾ƿ´�.
            SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();

            // �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
            renderer.flipY = playerRenderer.flipX;

            // ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
            Bullets.Add(Obj);
        }

        // �÷��̾��� �����ӿ� ���� �̵������ �����Ѵ�.
        animator.SetFloat("Speed", Mathf.Abs(Hor));
        // ���� �÷��̾ �����δ�.
        
    }


    private void OnAttack()
    {
        // �̹� ���ݸ���� �������̶��
        if (onAttack)
            // �Լ��� �����Ų��.
            return;
        // �Լ��� ������� �ʾѴٸ�
        // ���ݻ��¸� Ȱ��ȭ �ϰ�
        onAttack = true;
        // ���ݸ���� �����Ų��.
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
        // �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onAttack = false;
    }

    private void OnHit()
    {
        // �̹� �ǰݸ���� �������̶��
        if (onHit)
            // �Լ� ����
            return;
        // �Լ��� ������� �ʾҴٸ�
        // �ǰݻ��¸� Ȱ��ȭ �ϰ�
        onHit = true;
        // �ǰݸ���� �����Ų��.
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {

        // �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
        // �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
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

}