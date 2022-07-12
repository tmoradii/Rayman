using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTest : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D body;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        spiderMove(movement);
    }

    void spiderMove(Vector2 direction)
    {
        // body.velocity = direction *5f;
        //body.AddForce(direction * 11f);
        body.MovePosition((Vector2)transform.position + direction * 8f * Time.smoothDeltaTime);
        // transform.Translate(direction*8f*Time.deltaTime);
    }
}
