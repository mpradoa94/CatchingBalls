using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour {

    public Text scoreTxt;
    public int ballValue;

    private int score;
    private int wrongs;

	// Use this for initialization
	void Start () {
        score = 0;
        wrongs = 0;
        UpdateScore();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "RedBall")
        {
            wrongs += ballValue;
        }
        else
        {
            score += ballValue;
        }
        UpdateScore();
    }
	
	void UpdateScore () {
        scoreTxt.text = "Score: " + (score - wrongs);
    }

    public int getScore()
    {
        return score;
    }

    public int getWrongs()
    {
        return wrongs;
    }
}


