using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerBalls;

    public int brickGridWidth;
    public int brickGridHeight;

    public float brickAmountX;
    public float brickAmountY;

    public List<GameObject> brickPrefabs; // Prefab list for spawning bricks

    public Ball ballReference;

   // public int currentScore  

    public List<Brick> spawnedBricks;
    public int remainingPlayerBalls;

    public GameObject brickPrefab;

    private void Awake()
    {
        ballReference = GameObject.Find("ball").GetComponent<Ball>();
    }


    private void Start()
    {

        SpawnBricksAndBrickValue();
        ballReference.LaunchBall();

        //LaunchAll(Random.Range(0, 2) == 1);
    }

    public IEnumerator wait(float timeAmount)
    {


       yield return new WaitForSeconds(timeAmount);
    }

    public void Respawnball()
    {
        GameObject.Find("ball").transform.position = new Vector3(0.07f, -1.91f, 0);
    }

    public void StopBallVelocity()
    {
        GameObject.Find("ball").GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GameObject.Find("ball").GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    public void SpawnBricksAndBrickValue()
    {
         string color = "yellow";
         int colorIndex = 0;

        float tempBrickAmountX = 0;
        float tempBrickAmountY = 0;

        for (int i = 0; i < brickGridHeight; i++)
        {
       

            switch (colorIndex)
            {
                case 0:
                    color = "yellow";
                   break;
                case 1:
                    color = "orange";
                    break;
                case 2:
                    color = "red";
                    break;
                case 3:
                    color = "blue";
                    break;
                case 4:
                    color = "green";
                    break;
                default:
                    colorIndex = Random.Range(0, 5);
                    break;

            }
            
            print(i);

            for(int j = 0; j < brickGridWidth; j++)
            {
                GameObject currentBrick = Instantiate(brickPrefab);


                switch (colorIndex)
                {
                    case 0:
                        currentBrick.GetComponent<SpriteRenderer>().color = new Color32(250, 220, 0, 255);
                        break;
                    case 1:
                        currentBrick.GetComponent<SpriteRenderer>().color = new Color32(250, 120, 0, 255);
                        break;
                    case 2:
                        currentBrick.GetComponent<SpriteRenderer>().color = new Color32(250, 13, 0, 255);
                        break;
                    case 3:
                        currentBrick.GetComponent<SpriteRenderer>().color = new Color32(0, 13, 255, 255);
                        break;
                    case 4:
                        currentBrick.GetComponent<SpriteRenderer>().color = new Color32(13, 255, 0, 255);
                        print("green");
                        break;
                    default:
                       // colorIndex = Random.Range(0, 5);
                        break;

                }

                
                

                currentBrick.transform.SetParent(GameObject.Find("BrickParent").transform);
                currentBrick.transform.position = new Vector3(-3 + tempBrickAmountX, 0 + tempBrickAmountY, 0);




                brickPrefabs.Add(currentBrick);
                print(j);
                print(color);
                tempBrickAmountX += brickAmountX;

            }
            tempBrickAmountX = 0;
            tempBrickAmountY += brickAmountY;
            colorIndex++;


        }
    }
}
