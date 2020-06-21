using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hellmade.Sound;

public class SceneAnimationStuffs : MonoBehaviour {
    [Header ("Object")]
    #region Objects
    public GameObject restocker;
    public GameObject trash;
    public GameObject inventory;
    public GameObject pane;
    public GameObject machine;
    public GameObject table;
    public GameObject chair;
    public GameObject score;
    public GameObject leanTouch;
    public GameObject fadeBlack;
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI gameOverHighScore;
    public GameObject countDownObject;
    public GameObject customerManager;
    public Image fadeBlackImage;
    public GameObject pauseMenu;
    public GameObject scoreObj;
    public GameObject lifeObj;
    public GameObject gameOverMenu;
    public GameObject highScoreGameOverObj;
    public GameObject scoreGameOverObj;

    public SpriteRenderer soundIcon;
    public Sprite mute;
    public Sprite unmute;

    #endregion

    #region LeanTween
    [Header ("LeanTween Properties")]
    public float floatAmount;
    public float startUpDelay;
    public float machineMoveTime;
    public float restockerMoveTime;
    public float trashMoveTime;
    public float inventoryMoveTime;
    public float paneMoveTime;
    public float tableMoveTime;
    public float chairMoveTime;
    public float scoreMoveTime;
    private int trashHoverID;
    private int restockerHoverID;
    public float fadeAmount;
    public float fadeTime;
    public float countDownTweenTime;
    public float pauseMenuMoveTime;
    public float buttonTweenTime;
    public float gameOverTweenTime;
    public float scoreGameOverTweenTime;

    [Header ("LeanTween Ease Type")]
    public LeanTweenType moveEaseType;
    public LeanTweenType floatingEaseType;
    public LeanTweenType scoreEaseType;
    public LeanTweenType countDownEaseType;
    public LeanTweenType pauseMenuEaseType;
    public LeanTweenType buttonEase;
    public LeanTweenType gameOverEaseType;
    public LeanTweenType scoreGameOverEaseType;
    #endregion

    #region Variable
    bool isStartedUp = true;
    #endregion

    #region Monos
    private void Awake () {

    }

    private void OnEnable () {
        leanTouch = GameObject.FindWithTag ("LeanTouch");
        machine.transform.localPosition = new Vector2 (0, -2f);
        table.transform.localPosition = new Vector2 (0, 3.2f);
        chair.transform.localPosition = new Vector2 (-3f, 0);
        inventory.transform.localPosition = new Vector2 (0, 4.5f);
        restocker.transform.localPosition = new Vector2 (-3f, 0);
        trash.transform.localPosition = new Vector2 (3f, 0);
        pane.transform.localPosition = new Vector2 (0, -2f);
        gameOverMenu.transform.localScale = Vector3.zero;
        GameEvent.instance.OnBeginPlay += SetUpBeginPlaying;
        GameEvent.instance.OnPauseIn += HandlePauseIn;
        GameEvent.instance.OnGameOver += HandleGameOver;

        fadeBlack.SetActive (false);
        customerManager.SetActive (false);
        SetActiveTouch (false);
        if (isStartedUp) {
            isStartedUp = false;
            LeanTween.moveLocalY (machine, 0, machineMoveTime).setFrom (-2f).setDelay (startUpDelay).setEase (moveEaseType);
            LeanTween.moveLocalY (table, 0, tableMoveTime).setFrom (3.2f).setDelay (startUpDelay).setEase (moveEaseType);
            LeanTween.moveLocalX (chair, -0.009f, chairMoveTime).setFrom (-3f).setDelay (startUpDelay + tableMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalY (inventory, 0, inventoryMoveTime).setFrom (4.5f).setDelay (startUpDelay + tableMoveTime + chairMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalX (restocker, 0, restockerMoveTime).setFrom (-3f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalX (trash, 0, trashMoveTime).setFrom (3f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveLocalY (pane, 0, inventoryMoveTime).setFrom (-2f).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime).setEase (moveEaseType);
            LeanTween.moveY (score, 0, scoreMoveTime).setEase (scoreEaseType).setDelay (startUpDelay + tableMoveTime + chairMoveTime + inventoryMoveTime + inventoryMoveTime).setOnComplete (() => { StartCoroutine (CountDown ()); });

        }

    }
    private void OnDestroy () {
        GameEvent.instance.OnBeginPlay -= SetUpBeginPlaying;
        GameEvent.instance.OnPauseIn -= HandlePauseIn;
        GameEvent.instance.OnGameOver -= HandleGameOver;

        isStartedUp = true;
    }
    private void OnDisable () {
        GameEvent.instance.OnBeginPlay -= SetUpBeginPlaying;
        GameEvent.instance.OnPauseIn -= HandlePauseIn;
        GameEvent.instance.OnGameOver -= HandleGameOver;
        isStartedUp = true;
        LeanTween.cancelAll (true);
    }
    #endregion

    #region Methods
    void SetActiveTouch (bool _bool) {
        leanTouch.SetActive (_bool);
    }
    void SetUpBeginPlaying () {
        restockerHoverID = LeanTween.moveLocalY (restocker, 0.04f, floatAmount).setEase (floatingEaseType).setFrom (0).setLoopPingPong (-1).id;
        trashHoverID = LeanTween.moveLocalY (trash, 0.04f, floatAmount).setEase (floatingEaseType).setFrom (0).setLoopPingPong (-1).id;
        customerManager.SetActive (true);

    }

    IEnumerator CountDown () {
        fadeBlack.SetActive (true);
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, 0, 0);
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, fadeAmount, fadeTime).setEase (LeanTweenType.easeInOutQuad);
        yield return new WaitForSeconds (fadeTime);
        countDown.text = "3";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        GameEvent.instance.CountingStart();
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType).setOnComplete(()=> {  });
        yield return new WaitForSeconds (0.85f);
        countDown.text = "2";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        GameEvent.instance.CountingStart();
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType).setOnComplete(() => { });
        yield return new WaitForSeconds (0.85f);
        countDown.text = "1";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        GameEvent.instance.CountingStart();
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType).setOnComplete(() => {  });
        yield return new WaitForSeconds (0.85f);
        countDown.text = "GO!";
        countDownObject.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
        GameEvent.instance.DoneCountingStart();
        LeanTween.scale (countDownObject, new Vector3 (1, 1, 1), countDownTweenTime).setEase (countDownEaseType).setOnComplete(() => { });
        yield return new WaitForSeconds (0.85f);
        countDownObject.transform.localScale = Vector3.zero;
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, fadeAmount, 0, fadeTime).setEase (LeanTweenType.easeInOutQuad).setOnComplete (() => { fadeBlack.SetActive (false); SetActiveTouch (true); GameStateMachine.instance.ChangeState<InGameState> (); });

    }
    void UpdateFadeBlackAlpha (float val, float ratio) {
        fadeBlackImage.color = new Color (0, 0, 0, val);
    }
    #region Pause Menu
    public void HandlePauseIn () {
        GameEvent.instance.ButtonPress();
        bool isMuted = playerSettings.isMuted;
        if (!isMuted)
        {
            soundIcon.sprite = unmute;
        }
        else
        {
            soundIcon.sprite = mute;
        }
        fadeBlack.SetActive (true);

        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, 0, 0);
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, fadeAmount, fadeTime).setEase (LeanTweenType.easeInOutQuad).setOnComplete (pauseMenuUp);

        void pauseMenuUp () {

            LeanTween.moveY (pauseMenu, 0, pauseMenuMoveTime).setEase (pauseMenuEaseType).setOnComplete (() => GameStateMachine.instance.ChangeState<PauseState> ());
        }

    }

    public void HandlePauseOutToGame (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            GameEvent.instance.ButtonPress();
            LeanTween.scale (_gameObject, new Vector3 (3.5f, 3.5f, 3.5f), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setIgnoreTimeScale (true).setOnComplete (execute);
        }

        void execute () {
            LeanTween.moveY (pauseMenu, -4.3f, pauseMenuMoveTime).setEase (pauseMenuEaseType).setOnComplete (fadeOut).setIgnoreTimeScale (true);

            void fadeOut () {

                LeanTween.value (gameObject, UpdateFadeBlackAlpha, fadeAmount, 0, fadeTime).setEase (LeanTweenType.easeInOutQuad).setIgnoreTimeScale (true).setOnComplete (() => { GameStateMachine.instance.ChangeState<InGameState> (); fadeBlack.SetActive (false); });
            }
        }

    }

    public void HandlePauseOutToMenu (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            GameEvent.instance.ButtonPress();
            LeanTween.scale (_gameObject, new Vector3 (3.5f, 3.5f, 3.5f), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setIgnoreTimeScale (true).setOnComplete (execute);
        }

        void execute () {
            GameEvent.instance.ChangeScene (0);

        }
    }

    public void HandleRestart (GameObject _gameObject) {
        if (!LeanTween.isTweening (_gameObject)) {
            GameEvent.instance.ButtonPress();
            LeanTween.scale (_gameObject, new Vector3 (3.5f, 3.5f, 3.5f), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setIgnoreTimeScale (true).setOnComplete (execute);
        }

        void execute () {
            GameEvent.instance.ChangeScene (2);

        }
    }

    public void SoundButtonClick(GameObject _gameObject)
    {
        bool isMuted = playerSettings.isMuted;
        if (!LeanTween.isTweening(_gameObject))
        {
            GameEvent.instance.ButtonPress();
            LeanTween.scale(_gameObject, new Vector3(3.5f, 3.5f, 3.5f), buttonTweenTime).setLoopPingPong(1).setEase(buttonEase).setIgnoreTimeScale(true).setOnComplete(execute);
        }

        void execute()
        {
            if (!isMuted)
            {
                EazySoundManager.GlobalVolume = 0f;
                playerSettings.isMuted = true;
                soundIcon.sprite = mute;
            }
            else
            {
                EazySoundManager.GlobalVolume = 1f;
                playerSettings.isMuted = false;
                soundIcon.sprite = unmute;
            }


        }

        
    }

    public void HandleGameOver () {
        fadeBlack.SetActive (true);

        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, 0, 0).setIgnoreTimeScale (true);
        LeanTween.value (gameObject, UpdateFadeBlackAlpha, 0, fadeAmount, fadeTime).setEase (LeanTweenType.easeInOutQuad).setOnComplete (gameOverMenuAni).setIgnoreTimeScale (true);
        gameOverScore.text = "0";
        void gameOverMenuAni () {
            gameOverMenu.transform.localScale = Vector3.zero;
            LeanTween.scale (gameOverMenu, new Vector3 (1, 1, 1), gameOverTweenTime).setFrom (Vector3.zero).setEase (gameOverEaseType).setOnComplete (runScore).setIgnoreTimeScale (true);
            void runScore () {
                GameStateMachine.instance.ChangeState<LoseState> ();
                // int score = ScoreManager.instance.currentScore;
                int score = ScoreManager.instance.currentScore;
                int currentScore = 0;
                float animationScoreTime = 1f / score;
                StartCoroutine (ScoreUpdater ());
                IEnumerator ScoreUpdater () {
                    while (true) {
                        gameOverScore.color = HSBColor.ToColor (new HSBColor (Mathf.PingPong (Time.unscaledTime * 0.5f, 1), 1, 1));
                        if (currentScore < score) {
                            GameEvent.instance.ScoreCount();
                            currentScore += 50; //Increment the display score by 1
                            gameOverScore.text = currentScore.ToString (); //Write it to the UI\
                        } else {
                            GameEvent.instance.DoneScoreCounting();
                            currentScore = score;
                            gameOverScore.text = currentScore.ToString ();
                            LeanTween.scale (scoreGameOverObj, new Vector3 (1.5f, 1.5f, 1.5f), scoreGameOverTweenTime).setEase (scoreGameOverEaseType).setLoopPingPong (1).setIgnoreTimeScale (true).setOnComplete (() => {
                                if (currentScore > PlayerStats.instance.highScore) {
                                    gameOverHighScore.text = "New highscore!";
                                    SecurePlayerPrefs.SetInt ("highscore", currentScore);
                                    PlayerStats.instance.highScore = currentScore;
                                    //update to database
                                } else {
                                    gameOverHighScore.text = "Highscore: " + PlayerStats.instance.highScore;
                                }
                                LeanTween.scale (highScoreGameOverObj, new Vector3 (1.5f, 1.5f, 1.5f), scoreGameOverTweenTime).setEase (scoreGameOverEaseType).setLoopPingPong (1).setIgnoreTimeScale (true);
                            });
                            yield break;

                        }
                        yield return new WaitForSecondsRealtime (1 / 50000000); // I used .2 secs but you can update it as fast as you want
                    }
                }
            }

        }
        #endregion

        #endregion
    }
}