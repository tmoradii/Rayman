using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    Animator anim;
    bool animation_Finished,jumpingLeft = true,animation_Started;
    public int jumpCount = 3;
    int jumpTime;
    Coroutine frogJump;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FrogJump());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (animation_Finished && animation_Started)
        {
            animation_Started = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector2.zero;
            
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(2f);
        animation_Finished = false;
        animation_Started = true;
        jumpTime++;
     

        if (jumpingLeft)
        {
            anim.Play("jump");
        }
        else
        {
            anim.Play("jumpRight");
        }

        frogJump = StartCoroutine(FrogJump());
        
    }

    void animationFinished()
    {
        animation_Finished = true;
        
        if (jumpingLeft)
        {
            anim.Play("frogIdle");
        }
        else
        {
            anim.Play("rightIdle");
        }

        if (jumpTime == jumpCount)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpingLeft = !jumpingLeft;
            jumpTime = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTags.bullet)
        {
            StopCoroutine(frogJump);
            animation_Finished = true;
            if (jumpingLeft)
                anim.Play("frogDead");
            else
                anim.Play("forgDeadRight");


           // GetComponent<BoxCollider2D>().enabled = false;
            
            StartCoroutine(disapear());
        }

    }

    IEnumerator disapear()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
