using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    static public Vector3 CAMERA_LIMIT_VIEW = new Vector3(11.3f,5,0); 
    // Start is called before the first frame update
    void Start()
    {
        transform.position.Set(0,0,-10);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
