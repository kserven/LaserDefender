using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{

    public int score = 0;
    public Text myText;

    void Start()
    {
        Reset();
    }
    public void Score(int points)
    {
        score += points;
        Text myText = gameObject.GetComponent<Text>();
        myText.text = score.ToString();
    }

    public void Reset()
    {
        score = 0;
        Text myText = gameObject.GetComponent<Text>();
        myText.text = score.ToString();
    }
}