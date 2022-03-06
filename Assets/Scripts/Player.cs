using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private Vector3 _speed;
    public Vector2 _sensibility;

    [SerializeField]
    private bool _tripleShotEnabled = false;
    [SerializeField]
    private bool _shieldEnabled = false;


    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaserPrefab;

    private UIManager _uiManager;
    private float _mainFireOffsetY = 1.05f;

    [SerializeField]
    private float _fireRate = 0.5f;

    private float _lastShotTime = 0.0f;

    private SpawnManager _spawnManager;


    // Start is called before the first frame update
    void Start()
    {
         _uiManager = GameObject.FindWithTag("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
            Debug.LogError("ui manager is null");
        _uiManager.updateLiveImage(_lives);
        _spawnManager = GameObject.FindGameObjectWithTag("spawnmanager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
            Debug.LogError("spawn manager is null");
        _sensibility = new Vector2(5, 5);
        _speed = new Vector3(0, 0, 0);
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        movementController();
        if (Input.GetKeyDown(KeyCode.Space) && canShoot())
            fireLaser();
    }


    void fireLaser()
    {
        _lastShotTime = Time.time;

        if (_tripleShotEnabled)
        {
            Instantiate(
                _tripleLaserPrefab,
                transform.position,
                Quaternion.identity
            );
        }
        else
        {
            Instantiate(
                _laserPrefab,
                transform.position + (Vector3.up * _mainFireOffsetY),
                Quaternion.identity
            );
        }
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


        _speed.x = horizontalInput * _sensibility.x;
        _speed.y = verticalInput * _sensibility.y;

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

    public void damage()
    {
        if (_shieldEnabled)
        {
            _shieldEnabled = false;
            _shield.SetActive(false);
        }
        else
        {
            _lives--;
            _uiManager.updateLiveImage(_lives);
        }
        if (_lives <= 0)
        {
            _spawnManager.onPlayerDeath();
            _uiManager.updateGameover();
            Destroy(this.gameObject);
        }
    }

    public void addScore(int points){
        _score+=points;
        _uiManager.updateScoreText(_score);

    }
    public int getPlayerScore(){
        return _score;
    }
    public void collectPowerUp(PowerUp.PowerupID powerUpCode, float duration)
    {
        switch (powerUpCode)
        {
            case PowerUp.PowerupID.TRIPLE_SHOT:
                _tripleShotEnabled = true;
                StartCoroutine(tripleShotPowerDownRoutine(duration));
                break;
            case PowerUp.PowerupID.SHIELD:
                _shieldEnabled = true;
                _shield.SetActive(true);
                StartCoroutine(shieldPowerDownRoutine(duration));
                break;
            case PowerUp.PowerupID.SPEED:
                _sensibility += new Vector2(5, 5);
                StartCoroutine(speedPowerDownRoutine(duration));
                break;
            default:
                Debug.LogError("Player can't collect powerup with code " + powerUpCode);
                break;
        }
    }

    IEnumerator tripleShotPowerDownRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        _tripleShotEnabled = false;
    }
    IEnumerator shieldPowerDownRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        _shieldEnabled = false;
        _shield.SetActive(false);
    }
    IEnumerator speedPowerDownRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        _sensibility -= new Vector2(5, 5);
    }
}
