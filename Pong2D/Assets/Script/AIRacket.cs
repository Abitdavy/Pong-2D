using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRacket : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Npc Setting")]
    public float speed;
    public float delayMove;

    private bool isMoveAi; // cek apakah raket bergerak atau tidak
    private float randomPos; //-1 atau 1
    private bool isSingleTake;
    private bool isUp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameData.instance.isSinglePlayer)
        {

            // ! = invert == false
            if (!isMoveAi && !isSingleTake)
            {
                Debug.Log("Kepanggil");

                StartCoroutine("DelayAIMove");
                isSingleTake = true;
            }

            if (isMoveAi)
            {
                MoveAI();
            }
        }
    }

    private IEnumerator DelayAIMove()
    {
        yield return new WaitForSeconds(delayMove);
        randomPos = Random.Range(-1f, 1f);

        if(transform.position.y < randomPos)
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }

        isSingleTake = false;
        isMoveAi = true;
    }

    private void MoveAI()
    {
        if(!isUp) //raket kearah bawah
        {
            rb.velocity = new Vector2(0, -1) * speed; // velo = Acc -> Vector 2 x = 0, y = -1
            if(transform.position.y <= randomPos)
            {
                rb.velocity = Vector2.zero;
                isMoveAi = false;
            }
        }

        if(isUp)
        {
            rb.velocity = new Vector2(0, 1) * speed;
            if(transform.position.y >= randomPos)
            {
                rb.velocity = Vector2.zero;
                isMoveAi = false;
            }
        }
    }
}
