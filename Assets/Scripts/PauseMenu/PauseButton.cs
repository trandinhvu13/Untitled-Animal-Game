using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour {
    public LeanTweenType buttonEase;
    public float buttonTweenTime;

    public void pauseClick () {
        if (!LeanTween.isTweening (gameObject)) {
            LeanTween.scale (gameObject, new Vector3 (5, 5, 5), buttonTweenTime).setLoopPingPong (1).setEase (buttonEase).setOnComplete (execute);
        }

        void execute () {
            GameEvent.instance.PauseIn ();
        }
    }
}