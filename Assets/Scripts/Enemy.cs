using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;

    private const int Points = 10;


    private Animator _anim;

    private bool _isDestroyed;
    Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null");

        _anim = this.gameObject.GetComponent<Animator>();
        if (_anim == null)
            Debug.LogError("Enemy animator is null");
        transform.position = new Vector3(MainCamera.randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second

        //if bottom of screen
        // respawn at top with a new random x position
        transform.Translate(Vector3.down * (_speed * Time.deltaTime));
        if (transform.position.y < -MainCamera.CAMERA_LIMIT_VIEW.y && !_isDestroyed)
            transform.position = new Vector3(MainCamera.randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && ! _isDestroyed)
        {
            _player.damage();
            _isDestroyed = true;
            _speed=1.0f;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject,2.0f);
        }
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            if (_player != null && !_isDestroyed)
            {
                _player.addScore(Points);
            }
            _speed=1.0f;
            _isDestroyed=true;
            _anim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject,2.0f);
        }

    }




}
