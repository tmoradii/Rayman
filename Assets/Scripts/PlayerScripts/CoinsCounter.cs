using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounter : MonoBehaviour
{
    static int coinCount = 0;
    Text coinText;
  
    // Start is called before the first frame update
    void Start()
    {
        coinText = GameObject.Find("coinCount").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTags.coin)
        {
            collision.gameObject.SetActive(false);
            coinCount++;
            coinText.text = coinCount.ToString();
            GetComponent<PlayerAudioManager>().coinCollect.Play();          

        }
    }
}
