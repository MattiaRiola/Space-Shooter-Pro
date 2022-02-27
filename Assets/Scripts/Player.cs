using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int _lives = 3;

    [SerializeField]
    private Vector3 _speed;
    public Vector2 sensibility;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _offset = 0.8f;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _lastShotTime = 0.0f;

    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("spawnmanager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
            Debug.LogError("spawn manager is null");
        sensibility = new Vector2(5, 5);
        _speed = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        movementController();
        if(Input.GetKeyDown(KeyCode.Space) && canShoot())
            fireLaser();    
    }


    void fireLaser(){
            _lastShotTime = Time.time;
            Instantiate(
                _laserPrefab,
                transform.position + (Vector3.up * _offset),
                Quaternion.identity
            );
    }
    
    // return true if the player can shoot false otherwise
    public bool canShoot()
    {
        return Time.time - _lastShotTime > _fireRate;
    }
    void movementController()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        _speed.x = horizontalInput * sensibility.x;
        _speed.y = verticalInput * sensibility.y;

        transform.Translate(_speed * Time.deltaTime);

        xSpeedAndPositionUpdate(Input.GetAxis("Horizontal"));

        ySpeedAndPositionUpdate(Input.GetAxis("Vertical"));

    }

    void xSpeedAndPositionUpdate(float horizontalInput)
    {
        if (transform.position.x >= MainCamera.CAMERA_LIMIT_VIEW.x)
        {
            //teleport object
            transform.position =
                new Vector3(
                    -MainCamera.CAMERA_LIMIT_VIEW.x + 0.5f,
                    transform.position.y,
                    transform.position.z);

        }
        else if (transform.position.x <= -MainCamera.CAMERA_LIMIT_VIEW.x)
        {
            //teleport object
            transform.position =
                new Vector3(
                    MainCamera.CAMERA_LIMIT_VIEW.x - 0.5f,
                    transform.position.y,
                    transform.position.z);

        }
    }
    void ySpeedAndPositionUpdate(float verticalInput)
    {
        if (transform.position.y >= MainCamera.CAMERA_LIMIT_VIEW.y)
        {
            //set the speed to 0 if moving upward
            _speed.y = verticalInput <= 0 ? verticalInput : 0;
        }
        else if (transform.position.y <= -MainCamera.CAMERA_LIMIT_VIEW.y)
        {
            //set the speed to 0 if moving downward
            _speed.y = verticalInput >= 0 ? verticalInput : 0;
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

    public void damage(){
        _lives--;
        if(_lives <= 0 ){
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
