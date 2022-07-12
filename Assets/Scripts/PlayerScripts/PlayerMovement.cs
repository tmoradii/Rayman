using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D player;
    Animator anim;
    float horizontal, vertical;
    bool jumped, isGrounded;
    LayerMask groundLayer;

    public float JumpPower;
    public Transform downCollision;

    // Start is called before the first frame update
    void Start()
    {
        jumped = false;
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundLayer = LayerMask.GetMask(MyLayers.ground, MyLayers.obstacle);

    }

    // Update is called once per frame
    void Update()
    {
        playerJump();
    }
    void FixedUpdate()
    {
        playerWalk();      
    }

    void playerJump()
    {
        isGrounded = Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f, groundLayer);
        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("jump", false);
            }
            //vertical = Input.GetAxisRaw("Vertical");
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                player.velocity = new Vector2(player.velocity.x, JumpPower);
                jumped = true;
                anim.SetBool("jump", true);
                PlayerAudioManager.jumpSound.Play();
                //anim.SetInteger("speed", 0);
            }
        }

    }

    void playerWalk()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        if (horizontal > 0)
        {
            player.velocity = new Vector2(5f, player.velocity.y);
            changeDirection(1);
        }
        else if (horizontal < 0)
        {

            player.velocity = new Vector2(-5f, player.velocity.y);
            changeDirection(-1);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        anim.SetInteger("speed", Mathf.Abs((int)player.velocity.x));
    }

    void changeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

}
