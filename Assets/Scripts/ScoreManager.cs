using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ScoreManager : MonoBehaviour {

    public Text scoreText, recordtext;
    public Animator scoreAnimator;
    public HighScoreBeat mainHighScoreBeat;

    [SerializeField]
    int totalScore;

    int highScore;
    bool highScoreSet;

    void Start () {
        totalScore = 0;
        scoreText.text = "Puntos: " + totalScore;
        highScore = PlayerPrefs.GetInt("HighScore");
        recordtext.text = "Record: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
	   if(totalScore > highScore)
        {
            PlayerPrefs.SetInt("highScore", totalScore);
            recordtext.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }	
	}

    public void IncreaseScore() {
        totalScore+=1;
        AnimateScoreTextIncrease();
        UpdateScoreTextOnPop(totalScore);

        if(totalScore > highScore)
        {
            HighScoreBeaten();
            if (!highScoreSet)
            {
                mainHighScoreBeat.ShowHighScore();
                highScoreSet = true;
            }
        }
    }

    public int GetScore()
    {
        return totalScore;
    }

    void HighScoreBeaten()
    {
        PlayerPrefs.SetInt("HighScore", totalScore);
    }

    void AnimateScoreTextIncrease() {
        scoreAnimator.SetBool("scoreUp", true);
        Invoke("SetScoreAnimatorToFalse", 0.1f);
    }

    void SetScoreAnimatorToFalse()
    {
        scoreAnimator.SetBool("scoreUp", false);
    }

    void UpdateScoreTextOnPop(int _totalScore) {
        scoreText.text = "Puntos:" + _totalScore;
    }

    public void BorraRecord()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
