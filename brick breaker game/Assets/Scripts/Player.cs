using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float ballLaunchForce =5f;
    public GameManager gm;


    public void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    void Update()
    {
       
        float moveX = Input.GetAxisRaw("Horizontal");  
        if (moveX != 0)
        {
             
            Vector3 newPosition = transform.position;
            newPosition.x += moveX * moveSpeed; 
            transform.position = newPosition;
        }
    }
}
