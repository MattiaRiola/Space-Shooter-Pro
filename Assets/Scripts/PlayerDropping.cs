using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerDropping : MonoBehaviour
{
    public static float G = (float) - 9.81; // [m/s^2]
    
    public float velocity;
    public bool dropping;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position and assign the start position = (0,0,0)
        velocity = 0;
        dropping = true;
        transform.position = new Vector3Int(0, 10, 5);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 0){
            dropping=false;
            velocity = 0;
            transform.position = new Vector3(0,0,5);
        }

        if(dropping == true)
            drop(1);
        
    }

    void drop(float deltaT)
    {
        updateVelocity(deltaT);
        updatePosition(deltaT);

    }

    void updatePosition(float deltaT)
    {
        float newDeltaY = velocity * Time.deltaTime * deltaT + G * Time.deltaTime * deltaT * deltaT / 2;
        transform.Translate( new Vector3(
                    0,
                    newDeltaY,
                    0
                ));
    }
    void updateVelocity(float deltaT)
    {
        velocity += G*Time.deltaTime*deltaT;

    }

}
