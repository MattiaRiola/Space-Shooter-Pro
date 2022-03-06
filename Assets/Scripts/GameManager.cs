using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    [SerializeField]
    private bool _isGameover = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && _isGameover)
        {
            //you have to add the scene in the settings
            // Unity/file/Build settings
            //once the Game scene is added you can see the index of the scene
            SceneManager.LoadScene(0); //0 is current Game scene
        }
    }
    public void gameOver()
    {
        _isGameover = true;
    }


}
