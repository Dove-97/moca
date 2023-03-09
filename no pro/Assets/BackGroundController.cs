using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public float Speed;

    private GameObject player;
    private Vector3 movemant;
    private Vector3 offset = new Vector3(0.0f, 7.5f, 0.0f);

    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    void Update()
    {
        movemant = new Vector3(
            Input.GetAxisRaw("Horizontal") + offset.x,
            player.transform.position.y + offset.y,
            0.0f + offset.z);

        transform.position -= movemant * Time.deltaTime * Speed;
    }
}