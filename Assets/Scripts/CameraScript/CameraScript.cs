using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	Transform playerTransform ;
    Vector3 temp;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(MyTags.playerTag).transform;

    }

    private void Update()
    {

       temp = transform.position;
       temp.x = playerTransform.position.x;
       transform.position = temp;

    }

}
