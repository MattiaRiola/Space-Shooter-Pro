using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    

    [SerializeField]
    private float _rotationSpeed=10;

    [SerializeField]
    private float _hp = 10;

    [SerializeField]
    private int _points = 5;

    [SerializeField]
    private int _pointsOnDestroy = 50;
    private Player _player;

    private Animator _anim;
    private bool _isDestroyed = false;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (_player == null)
            Debug.LogError("Player is null");

        _anim = this.gameObject.GetComponent<Animator>();
        if( _anim == null )
            Debug.LogError("asteroid anim is null");
    }

    void Update()
    {
        transform.Rotate(Vector3.forward,_rotationSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null && _hp>=0)
            {
                _player.addScore(_points);
            }
            damage();
            
        }
        if(other.tag == "Player" && !_isDestroyed){
            _player.damage();
            damage();
        }
    }

    private void damage(){
        _hp--;
            if(_hp <= 0){
                if(_isDestroyed)
                    _player.addScore(_points);
                
                _isDestroyed = true;
                _anim.SetTrigger("OnAsteroidDestroyed");
                Destroy(this.gameObject,2.4f);

            }
    }
}
