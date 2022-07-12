using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    Rigidbody2D bird;
    Animator anim;
    public float speed = -4f , flyRange = 5f;
    float leftMovePos, rightMovePos;
    bool canMove, movingLeft, hasEgg;
    Vector3 direction;
    public GameObject birdEgg;


    private void Awake()
    {
        bird = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canMove = true;
        movingLeft = true;
        hasEgg = true;
        direction = Vector3.left;
    }

    // Start is called before the first frame update
    void Start()
    {
        leftMovePos = transform.position.x - flyRange;
        rightMovePos = transform.position.x + flyRange;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        birdFly();
        birdAttack();

    }

   void birdFly()
    {
        if (canMove)
        {
            transform.Translate(speed * Time.smoothDeltaTime * direction);
            if (transform.position.x <= leftMovePos)
            {
                direction = Vector3.right;
                changedirection();
            }
            if (transform.position.x > rightMovePos)
            {
                direction = Vector3.left;
                changedirection();
            }
        }               
    }
    
    private void changedirection()
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x *= -1;
        transform.localScale = tempScale;
        movingLeft = !movingLeft;
    }

    void birdAttack()
    {
        if(hasEgg && Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity , LayerMask.GetMask(MyLayers.player)))
        {
            Instantiate(birdEgg, 
                        new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z),
                        Quaternion.identity);
            anim.Play("birdFlyWithoutStone");
            hasEgg = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == MyTags.bullet)
        {
            anim.Play("BirdDead");
            bird.bodyType = RigidbodyType2D.Dynamic;
            canMove = false;
            GetComponent<Collider2D>().isTrigger = true;
            StartCoroutine(disapear());
        }
    }

    IEnumerator disapear()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
