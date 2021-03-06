using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject[] _powerups;


    [SerializeField]
    private PowerUp.PowerupID[] _powerupsId = {
        PowerUp.PowerupID.TRIPLE_SHOT,
        PowerUp.PowerupID.SPEED,
        PowerUp.PowerupID.SHIELD
        };


    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _powerupContainer;

    [SerializeField]
    private bool _spawnEnabled = true;



    public void startSpawning(){
        StartCoroutine(spawnEnemyRoutine());
        StartCoroutine(spawnPowerupRoutine());  
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

            int randPowerupId = Random.Range(0, _powerups.Length);
            GameObject newPowerup = Instantiate(
                            _powerups[randPowerupId],
                            new Vector3(MainCamera.randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0),
                            Quaternion.identity
                            );

            if (newPowerup == null)
                Debug.LogError("new powerup is null");
            else
                newPowerup.transform.parent = _powerupContainer.transform;

            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    public void onPlayerDeath()
    {
        _spawnEnabled = false;
    }


}
