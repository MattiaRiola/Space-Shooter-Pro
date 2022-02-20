using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector3 speed;
    public float sensibility = 5f; 
    public bool xOk;
    public bool yOk;

    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector3(0,0,0);
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        speed.x = horizontalInput;
        speed.y = verticalInput;


        xSpeedAndPositionUpdate(Input.GetAxis("Horizontal"));
        
        ySpeedAndPositionUpdate(Input.GetAxis("Vertical"));
        


        transform.Translate(sensibility*speed*Time.deltaTime);


        
    }

    void xSpeedAndPositionUpdate(float horizontalInput){
        if(transform.position.x >= MainCamera.CAMERA_LIMIT_VIEW.x){
            //teleport object
            transform.position = 
                new Vector3(
                    - MainCamera.CAMERA_LIMIT_VIEW.x+0.5f,
                    transform.position.y,
                    transform.position.z);
            
        }
        else if(transform.position.x <= - MainCamera.CAMERA_LIMIT_VIEW.x){
            //teleport object
            transform.position = 
                new Vector3(
                    MainCamera.CAMERA_LIMIT_VIEW.x-0.5f,
                    transform.position.y,
                    transform.position.z);
            
        }
    }
    void ySpeedAndPositionUpdate(float verticalInput){
        if(transform.position.y >= MainCamera.CAMERA_LIMIT_VIEW.y){
            //set the speed to 0 if moving upward
            speed.y = verticalInput <= 0 ? verticalInput : 0;
            //stall position
            transform.position = 
                new Vector3(
                    transform.position.x,
                    MainCamera.CAMERA_LIMIT_VIEW.y,
                    transform.position.z);
            
        }
        
        else if(transform.position.y <= - MainCamera.CAMERA_LIMIT_VIEW.y){
            //set the speed to 0 if moving downward
            speed.y = verticalInput >= 0 ? verticalInput : 0;
            //stall position
            transform.position = 
                new Vector3(
                    transform.position.x,
                    -MainCamera.CAMERA_LIMIT_VIEW.y,
                    transform.position.z);
            
        }
    }
}
