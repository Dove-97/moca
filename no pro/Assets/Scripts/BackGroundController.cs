using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    // BackGround가 모여있는 계층구조의 최상위 객체(부모)
    private Transform parent;

    // Sprite를 포함하고 있는 구성요소
    private SpriteRenderer spriteRenderer;
    // 이미지
    private Sprite sprite;

    // 생성지점
    private float endPoint;
    // 삭제지점
    private float exitPoint;
    
    //이미지 이동속도
    public float Speed;

    // 플레이어 정보
    private GameObject player;
    private PlayerController playerController;

    SpriteRenderer playerRenderer;
    // 움직임 정보
    private Vector3 movemane;
    // 이미지가 중앙 위치에 정상적으로 노출될 수 있도록 하기 위한 완충 역할
    private Vector3 offset = new Vector3(0.0f, 7.5f, 0.0f);


    private void Awake()
    {
        // 플레이어의 기본정보를 받아온다.
        player = GameObject.Find("Player").gameObject;

        // 부모객체를 받아온다.
        parent = GameObject.Find("BackGround").transform;

        // 이미지를 담고있는 구성요소를 받아온다
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 플레이어 이미지를 담고있는 구셩요소를 받아온다
        playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // 구성요소에 포함된 이미지를 받아온다.
        sprite = spriteRenderer.sprite;
        // 시작지점을 셋팅
        endPoint = sprite.bounds.size.x * 0.5f + transform.position.x;
        // 종료지점을 셋팅
        exitPoint = sprite.bounds.size.x * 0.5f - transform.position.x;
    }

    void Update()
    {
        // 이동정보 셋팅
        movemane = new Vector3(
                Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed + offset.x,
                player.transform.position.y + offset.y,
                0.0f + offset.z);

        // 플레이어가 바라보고 있는 방향에 따라 분기됨
        if (ControllerManager.GetInstance().dirLeft)
        {// 좌측이동
            endPoint -= movemane.x;
        }
        if(ControllerManager.GetInstance().dirRight)
        {// 우측이동
            if (transform.position.x < 0)
            {
                endPoint -= movemane.x;

            }

            else
                transform.position -= movemane;
        }

  
        

        // 동일한 이미지 복사
        if (player.transform.position.x + (sprite.bounds.size.x * 0.5f) + 1 > endPoint)
        {
            // 이미지를 복제한다.
            GameObject Obj = Instantiate(this.gameObject);

            //복제된 이미지의 부모를 설정한다.
            Obj.transform.parent = parent.transform;
            //복제된 이미지의 이름을 설정한다.
            Obj.transform.name = transform.name;

            //복제된 이미지의 위치를 설정한다.
            Obj.transform.position = new Vector3(endPoint + 25.0f, 0.0f, 0.0f);

            //시작지점을 변경한다.
            endPoint += endPoint + 25.0f;
            
        }

        // 종료지점에 도달하면 삭제한다.
        if(transform.position.x + (sprite.bounds.size.x * 0.5f) - 2 < exitPoint)
            Destroy(this.gameObject);
        
    }
}
