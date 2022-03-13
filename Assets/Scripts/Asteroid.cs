using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{



    [SerializeField]
    private float _rotationSpeed = 10;

    [SerializeField]
    private float _hp = 10;

    [SerializeField]
    private int _points = 5;

    [SerializeField]
    private int _pointsOnDestroy = 50;
    private Player _player;

    private Animator _anim;
    private bool _isDestroyed = false;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject _explosionContainer;
    private SpawnManager _spawnManager;

    void Start()
    {

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
            Debug.LogError("Spawn manager not found");

        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null");

        _anim = this.gameObject.GetComponent<Animator>();
        if (_anim == null)
            Debug.LogError("asteroid anim is null");
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            damage(other.gameObject.transform.position);
            Destroy(other.gameObject);
            if (_player != null && _hp >= 0)
            {
                _player.addScore(_points);
            }

        }
        if (other.tag == "Player" && !_isDestroyed)
        {
            damage(other.gameObject.transform.position);
            _player.damage();
        }
    }

    private void damage(Vector3 pos)
    {
        _hp--;
        GameObject newExplosion = Instantiate(
            _explosionPrefab,
            pos,
            Quaternion.identity

        );
        newExplosion.transform.parent = _explosionContainer.transform;
        if (_hp <= 0 && !_isDestroyed)
        {
            _player.addScore(_pointsOnDestroy);
            _spawnManager.startSpawning();
            _anim.SetTrigger("OnAsteroidDestroyed");
            Destroy(this.gameObject, 2.4f);
            _isDestroyed = true;


        }
    }
}
