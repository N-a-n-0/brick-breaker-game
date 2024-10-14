using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int playerBalls = 3;

    public int brickGridWidth;
    public int brickGridHeight;

    public float brickAmountX;
    public float brickAmountY;

    public List<GameObject> brickPrefabs;
    public int bricksLeft;

    public Ball ballReference;

    public List<Brick> spawnedBricks;
    public int remainingPlayerBalls;

    public GameObject brickPrefab;

    public  int totalPlayerScore;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI liveText;

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winnerText;

    public int[] randomScores = { 10, 25, 50, 75, 100 };

    public AudioSource audioSource;

    private void Awake()
    {
        ballReference = GameObject.Find("ball").GetComponent<Ball>();
    }


    public  void Start()
    {
        remainingPlayerBalls = playerBalls;
        SpawnBricksAndBrickValue();
        bricksLeft = brickPrefabs.Count;




    }

    public void Update()
    {
        scoreText.text = "Score: " + totalPlayerScore;
        liveText.text = remainingPlayerBalls + " :Lives";
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
            
           // print(i);

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
                       // print("green");
                        break;
                    default:
                       // colorIndex = Random.Range(0, 5);
                        break;

                }

                
                

                currentBrick.transform.SetParent(GameObject.Find("BrickParent").transform);
                currentBrick.transform.position = new Vector3(-3 + tempBrickAmountX, 0 + tempBrickAmountY, 0);


                currentBrick.GetComponent<Brick>().score = randomScores[Random.Range(0, 5)];

                brickPrefabs.Add(currentBrick);
                spawnedBricks.Add(currentBrick.GetComponent<Brick>());
                //print(j);
             //   print(color);
                tempBrickAmountX += brickAmountX;

            }
            tempBrickAmountX = 0;
            tempBrickAmountY += brickAmountY;
            colorIndex++;


        }
    }

    public void NotifyBrickDestroyed(int score)
    {
        totalPlayerScore += score;
    }


    public void NoifyBallLost()
    {
        remainingPlayerBalls -= 1;
    }
}
