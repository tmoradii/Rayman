using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snailScript : MonoBehaviour
{
    public float moveSpeed = -0.8f;
    public Transform leftCollision, rightCollision, upCollision, downCollision;

    private Rigidbody2D snail;
    private Collider2D bodyCollider, hitedUp;
    private Animator snailAnim;   
    int playerObstacleMask, playerMask, maskNum;
     public float radius = 0.2f;
    bool Stunned,killerMode, playerLeft = false;
    RaycastHit2D leftHitted, rightHitted;  
    Coroutine lastRoutine = null;



    // Start is called before the first frame update
    void Start()
    {
        snail = GetComponent<Rigidbody2D>();
        snailAnim = GetComponent<Animator>();
        bodyCollider = GetComponent<Collider2D>();
        playerObstacleMask = LayerMask.GetMask(MyLayers.player,MyLayers.obstacle);
        playerMask = LayerMask.GetMask(MyLayers.player);
        Stunned = false;
        killerMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        leftHitted = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerObstacleMask);
        rightHitted = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerObstacleMask);
        hitedUp = Physics2D.OverlapCircle(upCollision.position, radius, playerMask);


        if (!Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f))
        {
            changeDirection();
        }

        if (!Stunned)
        {
            snail.velocity = new Vector2(moveSpeed, snail.velocity.y);                       
            checkCollision();

        }
        else if(tag == MyTags.snail)
        {
            kickDetect(leftHitted, 10f);
            kickDetect(rightHitted, -10f);
            if (hitedUp && playerLeft)//Jumped for the second time so enemy must be defeated
            {
                print("jjjjjjjjj" + hitedUp +" " + radius);
                snail.velocity = Vector2.zero;
                killerMode = false;
                //play defeat sound
                //play fallout animation
                //deactivate the object
                gameObject.SetActive(false);
            }

            if (killerMode) StopCoroutine(lastRoutine);
        }

    }

    void changeDirection()
    {
        moveSpeed = -moveSpeed;

        Vector3 tempScale = transform.localScale;
        tempScale.x = -transform.localScale.x;
        transform.localScale = tempScale;

        //fliping right and left values
        Vector3 tempPosition = leftCollision.position;
        leftCollision.position = rightCollision.position;
        rightCollision.position = tempPosition;
    }

    void checkCollision()
    {
      
        if (hitedUp != null && !Stunned) //player Jumped on this object
        {
            //this enemy goes to sleep
            
            Rigidbody2D playerRB = hitedUp.GetComponent<Rigidbody2D>();
            playerRB.velocity = new Vector2(playerRB.velocity.x, 10f); //player force jump
            //radius = 0f;
            StartCoroutine("stalling");

            snailAnim.Play("stunned");
            Stunned = true;
            snail.velocity = new Vector2(0f, snail.velocity.y);
            if (tag == MyTags.snail)
            {
                lastRoutine = StartCoroutine(snailSleep());

            }
            if (tag == MyTags.beetle)
            {
                StartCoroutine(enemyDisapear());
                snail.bodyType = RigidbodyType2D.Static;
                bodyCollider.enabled = false;
            }
            
  


        }
        else if (leftHitted.collider != null)
        {
            maskNum = leftHitted.transform.gameObject.layer;
            if (maskNum == LayerMask.NameToLayer(MyLayers.obstacle) && moveSpeed < 0) 
            {
                //change direction if this object colided with an obstacle
                changeDirection(); 
            }
            if (maskNum == LayerMask.NameToLayer(MyLayers.player))
            {
                //To DO
                //if(snail.velocity.x > 0)
                 leftHitted.collider.gameObject.GetComponent<PlayerDamage>().damageplayer();
            }

        }
        else if (rightHitted.collider != null)
        {
            maskNum = rightHitted.transform.gameObject.layer;
            if (maskNum == LayerMask.NameToLayer(MyLayers.obstacle) && moveSpeed > 0)
            {
                changeDirection();
            }
            else if (maskNum == LayerMask.NameToLayer(MyLayers.player))
            {
                //To DO
                //print("Game over");
                rightHitted.collider.gameObject.GetComponent<PlayerDamage>().damageplayer();
            }
        }
    }

    void kickDetect(RaycastHit2D hit, float speed)
    {
        if (hit.collider != null)
        {
            if (!killerMode && hit.transform.gameObject.layer == LayerMask.NameToLayer(MyLayers.player))
            {
                snail.velocity = new Vector2(speed, snail.velocity.y);
                StopCoroutine(lastRoutine);
                killerMode = true;
            }
            else if (killerMode && hit.transform.gameObject.layer == LayerMask.NameToLayer(MyLayers.player))
            {
                snail.velocity = new Vector2(speed, snail.velocity.y);
                StopCoroutine(lastRoutine);
                //TO DO
                //print("Gameover");
                hit.collider.gameObject.GetComponent<PlayerDamage>().damageplayer();

            }
            else if (killerMode && hit.transform.gameObject.layer == LayerMask.NameToLayer(MyLayers.obstacle))
            {
                snail.velocity = new Vector2(speed, snail.velocity.y);
                StopCoroutine(lastRoutine);
            }


        }
    }
    IEnumerator snailSleep()
    {       
        yield return new WaitForSeconds(4);
        Stunned = false;
        //radius = 0.1f;
        snailAnim.Play("SnailWalk");
    } 

    IEnumerator stalling()
    {
        yield return new WaitForSeconds(0.2f);
        playerLeft = true;
    }

    IEnumerator enemyDisapear()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.bullet)
        {
            snailAnim.Play("stunned");
            Stunned = true;
            snail.velocity = new Vector2(0f, snail.velocity.y);
            if (tag == MyTags.snail)
            {
            }
            if (tag == MyTags.beetle)
            {
                StartCoroutine(enemyDisapear());
                snail.bodyType = RigidbodyType2D.Static;
                bodyCollider.enabled = false;
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(upCollision.position, radius);
    }
}
