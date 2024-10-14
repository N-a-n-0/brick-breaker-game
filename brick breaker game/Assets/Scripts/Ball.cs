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



    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.audioSource.Play();
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
            gameManager.bricksLeft--;
            if(gameManager.bricksLeft <= 0)
            {
                Debug.Log("YOU WON THE GAME SPAWNING MORE BRICKS FOR YOU");
                StopBallVelocity();
                print("Ball has been stopped");
                gameManager.winnerText.gameObject.SetActive(true);

                for (int i = 0; i < gameManager.brickPrefabs.Count; i++)
                {
                    
                    if (gameManager.brickPrefabs[i] != null)
                    {
                       
                        Destroy(gameManager.brickPrefabs[i]);
                    }
                }
                gameManager.brickPrefabs.Clear();
                gameManager.spawnedBricks.Clear();
                yield return new WaitForSeconds(2f);
                player.ResetBall();
                gameManager.winnerText.gameObject.SetActive(false);
                gameManager.SpawnBricksAndBrickValue();
                gameManager.bricksLeft = gameManager.brickPrefabs.Count;
                yield return new WaitForSeconds(1f);
               
                yield return new WaitForSeconds(1f);
                player.LaunchBall();
            }
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
               
                StopBallVelocity();
                player.ResetBall();

                //gm.wait(2f);
                yield return new WaitForSeconds(2f);
                player.LaunchBall();
            }
            else
            {
                Debug.Log("YOU LOST  RESTARTING GAME.....");
                StopBallVelocity();
                print("Ball has been stopped");
                gameManager.gameOverText.gameObject.SetActive(true);

                for (int i = 0; i < gameManager.brickPrefabs.Count; i++)
                {
                   
                    if(gameManager.brickPrefabs[i] != null)
                    {
                        
                        Destroy(gameManager.brickPrefabs[i]);
                    }
                }
                gameManager.brickPrefabs.Clear();
                gameManager.spawnedBricks.Clear();
                yield return new WaitForSeconds(2f);
                gameManager.gameOverText.gameObject.SetActive(false);
                gameManager.Start();
               
                yield return new WaitForSeconds(1f);
                player.ResetBall();
                gameManager.totalPlayerScore = 0;
                yield return new WaitForSeconds(1f);
                player.LaunchBall();

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
