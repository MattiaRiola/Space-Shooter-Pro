using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _livesImg;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateScoreText(int newScore){
        _scoreText.text = "Score: " + newScore.ToString();
    }

    public void updateLiveImage(int lives){
        _livesImg.sprite = _liveSprites[lives];
    }
}
