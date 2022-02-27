using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _spawnEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator spawnRoutine()
    {
        while (_spawnEnabled == true)
        {

            GameObject newEnemy = Instantiate(
                _enemyPrefab,
                new Vector3(Enemy.randomX(2), MainCamera.CAMERA_LIMIT_VIEW.y, 0),
                Quaternion.identity
                );
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void onPlayerDeath()
    {
        _spawnEnabled = false;
    }


}
