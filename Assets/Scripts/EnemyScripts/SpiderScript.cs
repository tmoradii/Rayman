using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    Rigidbody2D body;
    float MaxPos, MinPos;
    bool canMove = true;
    Animator anim;
    public float speed = 3f,movementRange = 5f;

    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        MaxPos = transform.position.y + movementRange/2;
        MinPos = transform.position.y - movementRange/2;
        body.velocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
          
            if (transform.position.y >= MaxPos)
            {
                body.velocity = Vector2.down * speed;

            }
            else if (transform.position.y <= MinPos)
            {
                body.velocity = Vector2.up * speed;
            }

        }
        
    }

    IEnumerator removeSpider()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == MyTags.bullet)
        {
            canMove = false;
            anim.Play("SpiderDead");
            body.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Collider2D>().isTrigger = true;
            StartCoroutine(removeSpider());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == MyTags.playerTag)
        {
            //damage player
            collision.gameObject.GetComponent<PlayerDamage>().damageplayer();
        }
    }

}
