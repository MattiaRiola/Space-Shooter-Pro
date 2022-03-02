﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 7f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up*_speed*Time.deltaTime);
        if(transform.position.y > MainCamera.CAMERA_LIMIT_VIEW.y){
            Destroy(this.gameObject);
            if(transform.parent.gameObject.tag == PowerUp.TRIPLE_LASER){
                Destroy(transform.parent.gameObject);
            }
        }

    }

}
