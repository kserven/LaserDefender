using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{

    public static int score = 0;
    public Text myText;

    void Start()
    {
        Reset();
        Score(0);
    }
    public void Score(int points)
    {
        score += points;
        Text myText = gameObject.GetComponent<Text>();
        myText.text = score.ToString();
    }

    public static void Reset()
    {
        score = 0;
    }
}