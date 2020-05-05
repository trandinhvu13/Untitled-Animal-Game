using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour {
    #region Game Objects
    public GameObject playButton;
    public GameObject upgradesButton;
    public GameObject scoreButton;
    public GameObject settingsButton;
    public GameObject aboutButton;
    public GameObject title;
    public GameObject character1;
    public GameObject character2;

    #endregion

    #region LeanTween
    public LeanTweenType buttonEase;
    public float buttonTweenTime;
    public LeanTweenType titleStartUpEase;
    public float titleStartUpTweenTime;
    public float titleStartUpDelayTime;
    public LeanTweenType buttonStartUpEase;
    public float buttonStartUpTweenTime;
    public float buttonStartUpDelay;
    public LeanTweenType charStartUpEase;
    public float charStartUpTweenTime;
    public float charStartUpDelay;
    #endregion

    #region Monos
    private void OnEnable () {
        MainMenuCharacter ();
        SetUpTween ();
    }
    #endregion

    #region Methods
    public void TweenButton (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            LeanTween.scale (_gameObject, new Vector3 (9, 9, 9), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase);
        }

    }

    public void MainMenuCharacter () {
        int rand1 = Random.Range (0, 39);
        int rand2 = Random.Range (0, 39);
        character1.GetComponent<SpriteRenderer> ().sprite = CustomerDatabase.instance.customer[rand1].sprite;
        character2.GetComponent<SpriteRenderer> ().sprite = CustomerDatabase.instance.customer[rand2].sprite;
        character1.GetComponent<Animator> ().runtimeAnimatorController = CustomerDatabase.instance.customer[rand1].animationController;
        character2.GetComponent<Animator> ().runtimeAnimatorController = CustomerDatabase.instance.customer[rand2].animationController;
    }

    public void SetUpTween () {
        LeanTween.scale (title, new Vector3 (0.37586f, 0.37586f, 0.37586f), titleStartUpTweenTime).setEase (titleStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime);
        LeanTween.scale (playButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay);
        LeanTween.scale (upgradesButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.15f);
        LeanTween.scale (scoreButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.3f);
        LeanTween.scale (settingsButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.45f);
        LeanTween.scale (aboutButton, new Vector3 (7.5f, 7.5f, 7.5f), buttonStartUpTweenTime).setEase (buttonStartUpEase).setFrom (Vector3.zero).setDelay (titleStartUpDelayTime + buttonStartUpDelay + 0.6f);
        LeanTween.moveLocalY(character1,-4.8f,charStartUpTweenTime).setEase(charStartUpEase).setDelay(charStartUpDelay);
        LeanTween.moveLocalY(character2,-4.8f,charStartUpTweenTime).setEase(charStartUpEase).setDelay(charStartUpDelay);

    }
    #endregion
}