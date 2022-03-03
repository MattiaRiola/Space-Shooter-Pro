using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
        [SerializeField]
    private float _speed = 3.0f;

    public const string TRIPLE_LASER = "triple_laser";
    public const float TRIPLE_LASER_DURATION = 5.0f;
    public const string SHIELD = "shield";
    public const float SHIELD_DURATION = 3.0f;
    public const string SPEED = "speed";
    public const float SPEED_DURATION = 3.0f;


    // Update is called once per frame
    void Update()
    {
        // respawn at top with a new random x position
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -MainCamera.CAMERA_LIMIT_VIEW.y)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Player player = other.GetComponent<Player>();
            if(player == null)
                Debug.LogError("player is null");
            player.collectPowerUp(TRIPLE_LASER);
            Destroy(this.gameObject);
        }
         
    }
}
