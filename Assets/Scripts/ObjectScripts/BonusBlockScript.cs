using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlockScript : MonoBehaviour
{
    public Transform downCollisionTransform;
    public GameObject bonusItem;
    Vector2 raycastPoint;
    Vector3 upPosition,primaryPos;
    Collider2D collisionCheck;
    int PlayerLayerMask;
    float bouncMaxY = 0.25f;
    public int bounusItemCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        raycastPoint = new Vector2(downCollisionTransform.position.x, downCollisionTransform.position.y);
        PlayerLayerMask = LayerMask.GetMask(MyLayers.player);
        upPosition = new Vector3(transform.position.x, transform.position.y + bouncMaxY, 0);
        primaryPos = transform.position;
        if (bonusItem.tag != MyTags.coin)
        {
            //only one item is alowded if bonus is not coin
            bounusItemCount = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        collisionCheck = Physics2D.OverlapCircle(raycastPoint, 0.1f, PlayerLayerMask);
        if (collisionCheck != null)
        {
            //print("player Hitted me woooooWW");
            //block goes up
            transform.position = Vector3.Lerp(transform.position, upPosition, Time.deltaTime * 10f);
            if(bounusItemCount > 0)
            {
                
                Instantiate(bonusItem, upPosition+new Vector3(0,0.5f,0), Quaternion.identity);
                bounusItemCount--;
            }
            
                
        }
        else
        {
            //block going down
            transform.position = Vector3.Lerp(transform.position, primaryPos, Time.deltaTime * 10f);
        }
    }

}
