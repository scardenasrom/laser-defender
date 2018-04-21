using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	void Start () {
        Text text = GetComponent<Text>();
        text.text = "Score: " + ScoreKeeper.currentScore;
        ScoreKeeper.ResetScore();
	}

}
