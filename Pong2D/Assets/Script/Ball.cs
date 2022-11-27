using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public bool isBounce;
    public bool bonusGoal;
    public bool isLastHit1;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int random = Random.Range(0,2);
        Debug.Log(random);
        if(random == 0)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            rb.velocity = Vector2.left * speed;
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 8 || transform.position.y < -8)
        {
            GameManager.instance.SpawnBall();
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.instance.BallBounceSfx();
            if(col.gameObject.tag == "RacketLeft" && !isBounce)
            {
                Vector2 dir = new Vector2(1, 0).normalized;
                rb.velocity = dir * speed;
                StartCoroutine("DelayBouce");
                isLastHit1 = true;
            }

            if(col.gameObject.tag == "RacketRight" && !isBounce)
            {
                Vector2 dir = new Vector2(-1,0).normalized;
                rb.velocity = dir * speed;
                StartCoroutine("DelayBouce");
                isLastHit1 = false;
            }
    }

    private IEnumerator DelayBouce()
    {
        isBounce = true;
        yield return new WaitForSeconds(1f);
        isBounce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal 1")
        {
            SoundManager.instance.GoalsSfx();
            GameManager.instance.player2Score++;
            if(bonusGoal)
            {
                GameManager.instance.player2Score++;
            }
            GameManager.instance.SpawnBall();
            Destroy(gameObject);
            if (GameManager.instance.goldenGoal)
            {
                GameManager.instance.GameOver();
            }
        }

        if(collision.gameObject.tag == "Goal 2")
        {
            SoundManager.instance.GoalsSfx();
            GameManager.instance.player1Score++;
            if(bonusGoal)
            {
                GameManager.instance.player1Score++;
            }
            GameManager.instance.SpawnBall();
            Destroy(gameObject);
            if (GameManager.instance.goldenGoal)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
