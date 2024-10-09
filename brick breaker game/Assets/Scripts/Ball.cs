using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public GameManager gameManager;
    public Player player;
    public  Rigidbody2D rb;

    public bool Respawning;


    public void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * player.ballLaunchForce;
    }
     


    void OnCollisionEnter2D(Collision2D collision)
    {

        Collider2D hitCollider = collision.collider;
        Vector2 direction = new Vector2(0,0);
       
        if (hitCollider == player.leftHitBox)
        {
            print("only hit box left was hit");
             direction = new Vector2(-2, 2).normalized;
            rb.velocity = direction * player.ballLaunchForce;
        }
        else if(hitCollider == player.rightHitBox)
        {
             direction = new Vector2(2, 2).normalized;
            rb.velocity = direction * player.ballLaunchForce;
            print("only hit box right was hit");
        }
        else if (hitCollider.gameObject.CompareTag("TopWall"))
        {
            direction =  new Vector2(direction.x, -2).normalized;
        }
       
       else  if (collision.gameObject.CompareTag("Brick"))
        {
            gameManager.NotifyBrickDestroyed(collision.gameObject.GetComponent<Brick>().score);
           
            //  collision.gameObject.SetActive(false);
            Destroy(collision.gameObject);
            print("ball collided with me :)");
        }


    }

    public void StopBallVelocity()
    {
        GameObject.Find("ball").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GameObject.Find("ball").GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

   

    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {

        print("inside");
        if (collision.gameObject.CompareTag("deathZone"))
        {
            gameManager.NoifyBallLost();
            if (gameManager.remainingPlayerBalls > 0)
            {
                print("ifstatement");
                StopBallVelocity();
                player.ResetBall();

                //gm.wait(2f);
                yield return new WaitForSeconds(2f);
                player.LaunchBall();
            }
            else
            {
                StopBallVelocity();
            }
              
        }
    }

   
    private void Awake()
    {
         
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();

        rb = GetComponent<Rigidbody2D>();
        player.startPosition = transform.position;
    }

    

     

     



}
