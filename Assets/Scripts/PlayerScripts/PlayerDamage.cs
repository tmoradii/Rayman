using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public int lifes = 3; //heart images must be named like "life3", "life4"
    GameObject[] heartObjects;
    Image[] heartImages;
    bool canDamage = true;
    AudioSource audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        audioManager = GetComponent<AudioSource>();
        heartObjects = new GameObject[lifes];
        for(int i=1; i <= lifes ; i++)
        {
            heartObjects[i-1] = GameObject.Find("life"+ i);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageplayer()
    {
        if(canDamage && lifes > 0)
        {
            lifes--;
            heartObjects[lifes].SetActive(false);
            //TODO
            GetComponent<PlayerAudioManager>().damageSound.Play();

            if (lifes > 0)
            {              
                canDamage = false;
                StartCoroutine(blinking());
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
        
    }

    IEnumerator blinking()
    {
        yield return new WaitForSeconds(3f);
        canDamage = true;
    }
}
