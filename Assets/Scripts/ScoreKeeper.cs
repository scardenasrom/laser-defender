using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    private int currentScore = 0;

    public void Score(int points) {
        currentScore += points;
        GetComponent<Text>().text = "Score: " + currentScore;
    }

    public void ResetScore() {
        currentScore = 0;
        GetComponent<Text>().text = "Score: " + currentScore;
    }

}
