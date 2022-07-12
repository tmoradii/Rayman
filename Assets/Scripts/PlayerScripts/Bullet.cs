using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxRange = 6f, speed = 6f;
    Vector2 temp;
    Animator anim;
    bool canMove;
    float startPos;

    public float Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        } 
    } 

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canMove = true;
        startPos = transform.position.x;
        //StartCoroutine(disapear(1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove )
        {
            if (Mathf.Abs(transform.position.x - startPos) < maxRange)
            {
                temp = transform.position;
                temp.x += speed * Time.deltaTime;
                transform.position = temp;
            }
            else
            {
                explode();
            }
        }

    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != MyTags.bullet && collision.tag!= MyTags.playerTag )
        {
            explode();
        }
    }
    void explode()
    {
        anim.Play("bulletExplode");
        canMove = false;
        StartCoroutine(disapear(0.2f));
    }

    IEnumerator disapear(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
