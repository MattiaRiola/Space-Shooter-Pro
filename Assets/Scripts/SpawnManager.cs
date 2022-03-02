using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _tripleshotPowerupPrefab;
    
    [SerializeField]
    private GameObject _enemyContainer;
    
    [SerializeField]
    private bool _spawnEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator spawnEnemyRoutine()
    {
        while (_spawnEnabled == true)
        {

            GameObject newEnemy = Instantiate(
                _enemyPrefab,
                new Vector3(MainCamera.randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0),
                Quaternion.identity
                );
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator spawnPowerupRoutine()
    {
        while (_spawnEnabled == true)
        {

            GameObject newPowerup = Instantiate(
                _tripleshotPowerupPrefab,
                new Vector3(MainCamera.randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0),
                Quaternion.identity
                );
            newPowerup.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3.0f,7.0f));
        }
    }

    public void onPlayerDeath()
    {
        _spawnEnabled = false;
    }


}
