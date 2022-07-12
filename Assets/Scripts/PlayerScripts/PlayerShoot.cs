using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    
    public Transform bulletStartPos;
    public GameObject bullet;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject newBullet = Instantiate(bullet, bulletStartPos.position, Quaternion.identity);
            if (transform.localScale.x > 0)
            {
                //player faces right so bullet goes to right
                newBullet.GetComponent<Bullet>().Speed = Mathf.Abs(newBullet.GetComponent<Bullet>().Speed);
            }
            else
            {
                newBullet.GetComponent<Bullet>().Speed = -Mathf.Abs(newBullet.GetComponent<Bullet>().Speed);
            }           
            
        }
    }



}
