using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameoverText;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _livesImg;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);
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
    public void updateGameover(){
        _gameoverText.gameObject.SetActive(true);
        StartCoroutine(flickeringGameover());
    }

    IEnumerator flickeringGameover()
    {
        while(true){
            yield return new WaitForSeconds(0.5f);
            _gameoverText.gameObject.SetActive(
                ! _gameoverText.gameObject.activeSelf
                );

        }
    }
}
