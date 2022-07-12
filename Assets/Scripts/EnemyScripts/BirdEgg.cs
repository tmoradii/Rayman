using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdEgg : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == MyTags.playerTag)
        {
            //damage player
            collision.gameObject.GetComponent<PlayerDamage>().damageplayer();
        }
        gameObject.SetActive(false);
    }
}
