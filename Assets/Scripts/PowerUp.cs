using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    public enum PowerupID
    {
        SPEED=0,
        TRIPLE_SHOT=1,
        SHIELD=2
    }

        [SerializeField]
    private float _speed = 3.0f;


    [SerializeField]
    private float _powerupDuration;
    [SerializeField]
    private PowerupID _powerupType;

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
            player.collectPowerUp(_powerupType,_powerupDuration);
            Destroy(this.gameObject);
        }
         
    }
}
