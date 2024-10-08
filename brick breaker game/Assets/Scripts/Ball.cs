using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public GameManager gameManager;
    public Player player;
    public  Rigidbody2D rb;

   public Vector3 startPosition = Vector3.zero;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();

        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void Reset()
    {
        if (rb == null) return;
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
    }
    public void LaunchBall()
    {
        print("ball was launched");
        
        Vector2 direction = new Vector2(2,2).normalized;
        rb.velocity = direction * player.ballLaunchForce;
    }

     



}
