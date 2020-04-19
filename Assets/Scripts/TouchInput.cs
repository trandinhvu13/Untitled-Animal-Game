using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {
    public LayerMask touchInputMask;
    private RaycastHit2D hit;

    private List<GameObject> touchList = new List<GameObject> ();
    private GameObject[] touchsOld;
    void Update () {
        #region EditorTest
        // #if UNITY_EDITOR
        //         if (Input.GetMouseButton (0) || Input.GetMouseButtonDown (0) || Input.GetMouseButtonUp (0)) {
        //             touchsOld = new GameObject[touchList.Count];
        //             touchList.CopyTo (touchsOld);
        //             touchList.Clear ();

        //                 Ray ray = GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);

        //                 if (Physics.Raycast (ray, out hit, touchInputMask)) {
        //                     GameObject recipient = hit.transform.gameObject;
        //                     touchList.Add (recipient);

        //                     if (Input.GetMouseButtonDown (0)) {
        //                         recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
        //                     }
        //                     if (Input.GetMouseButtonUp(0)) {
        //                         recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
        //                     }
        //                     if (Input.GetMouseButton (0)) {
        //                         recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
        //                     }
        //                 }
        //             }
        //             foreach (GameObject g in touchsOld) {
        //                 if (!touchList.Contains (g)) {
        //                     g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
        //                 }
        //             }
        //         }
        // #endif
        #endregion
        Debug.Log (Input.touchCount);

        if (Input.touchCount > 0) {
            touchsOld = new GameObject[touchList.Count];
            touchList.CopyTo (touchsOld);
            touchList.Clear ();

            foreach (Touch touch in Input.touches) {
                var rayOrigin = GetComponent<Camera> ().ScreenToWorldPoint (touch.position);
                hit = Physics2D.Raycast (new Vector2(rayOrigin.x, rayOrigin.y),Vector2.zero , touchInputMask);
                if (hit) {
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add (recipient);

                    if (touch.phase == TouchPhase.Began) {
                        recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Ended) {
                        recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
                        recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                    if (touch.phase == TouchPhase.Canceled) {
                        recipient.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }

            }
            foreach (GameObject g in touchsOld) {
                if (!touchList.Contains (g)) {
                    g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

    }
}