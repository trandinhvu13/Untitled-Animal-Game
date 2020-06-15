using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
    public dreamloLeaderBoard dl;
    public TextMeshProUGUI first;
    public TextMeshProUGUI second;
    public TextMeshProUGUI third;
    public TextMeshProUGUI yourHighScore;

    List<TextMeshProUGUI> scoreBoard = new List<TextMeshProUGUI>();
    List<string> playerName = new List<string> ();

    List<string> playerScore = new List<string> ();
private void Awake() {
     dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard ();
}
    private void OnEnable () {
        yourHighScore.text = "YOUR HIGHSCORE: "+ SecurePlayerPrefs.GetInt("highscore",0);
        scoreBoard.Add(first);
        scoreBoard.Add(second);
        scoreBoard.Add(third);
        if (dl.publicCode == "") Debug.LogError ("You forgot to set the publicCode variable");
        if (dl.privateCode == "") Debug.LogError ("You forgot to set the privateCode variable");
        List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow ();
        Debug.Log(scoreList.Count);
        if (scoreList == null) {
            first.text = "1. Loading...";
            second.text = "2. Loading...";
            third.text = "3. Loading...";
        } else {
            foreach (dreamloLeaderBoard.Score currentScore in scoreList) {
                playerName.Add (currentScore.playerName);
                Debug.Log(currentScore.playerName);
                playerScore.Add (currentScore.score.ToString ());
            }

            for (int i = 0; i < 3; i++) {
                scoreBoard[i].text = (i + 1).ToString() + ". " + playerScore[i] + " - " + playerName[i];
            }
        }
    }

}