using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    public int score = 1;
    // Start is called before the first frame update


    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("collision was detected");
        if (collision.gameObject.CompareTag("ball"))
        {
            Destroy(this.gameObject);
            print("ball collided with me :)");
        }
    }
}
