using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 42;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateScoreText(int newScore){
        _scoreText.text = "Score: " + newScore.ToString();
    }
}
