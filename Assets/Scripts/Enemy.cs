using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second

        //if bottom of screen
        // respawn at top with a new random x position
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -MainCamera.CAMERA_LIMIT_VIEW.y)
            transform.position = new Vector3(randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("player is null");
                Destroy(this.gameObject);
                return;
            }
            player.damage();
            Destroy(this.gameObject);
        }
        if (other.tag == "Laser")
        {
            Destroy(this.gameObject);
            Destroy(this.gameObject);
        }

    }


    static public float randomX(float margin)
    {
        return Random.Range(-MainCamera.CAMERA_LIMIT_VIEW.x + margin, MainCamera.CAMERA_LIMIT_VIEW.x - margin);
    }

}
