using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float boundary = 8f;
    public float ballLaunchForce =5f;
    public GameManager gm;

    public BoxCollider2D leftHitBox;
    public BoxCollider2D rightHitBox;

    public Vector3 startPosition = Vector3.zero;

    public void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        LaunchBall();
    }

   

    void Update()
    {
       

        float moveX = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * moveX * moveSpeed * Time.deltaTime);
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -boundary, boundary);
        transform.position = pos;
         
    }
   
    public void LaunchBall()
    {
        print("ball was launched");

        Vector2 direction = new Vector2(Random.Range(-3, 3), Random.Range(1, 3)).normalized;
        GameObject.Find("ball").GetComponent<Rigidbody2D>().velocity = direction * ballLaunchForce;
    }

    public void ResetBall()
    {
       
        if (gm.remainingPlayerBalls > 0)
        {
            
            GameObject.Find("ball").transform.position = new Vector3(0.07f, -1.91f, 0);

            GameObject.Find("ball").transform.position = startPosition;
            GameObject.Find("ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
    }
}
