using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Vector3 speed;
    public Vector2 sensibility; 
    public bool xOk;
    public bool yOk;

    // Start is called before the first frame update
    void Start()
    {
        sensibility = new Vector2(5,5);
        speed = new Vector3(0,0,0);
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {



        movementController();

        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Space key pressed");
        }
        
    }

    void movementController(){

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        speed.x = horizontalInput * sensibility.x;
        speed.y = verticalInput * sensibility.y;

        transform.Translate( speed * Time.deltaTime);

        xSpeedAndPositionUpdate(Input.GetAxis("Horizontal"));
        
        ySpeedAndPositionUpdate(Input.GetAxis("Vertical"));
        
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
        }
        else if(transform.position.y <= - MainCamera.CAMERA_LIMIT_VIEW.y){
            //set the speed to 0 if moving downward
            speed.y = verticalInput >= 0 ? verticalInput : 0;    
        }
        //stall position
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(
                transform.position.y,
                -MainCamera.CAMERA_LIMIT_VIEW.y,
                MainCamera.CAMERA_LIMIT_VIEW.y),
            0);
    }
}
