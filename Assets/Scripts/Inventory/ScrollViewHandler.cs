using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewHandler : MonoBehaviour {
    [SerializeField]
    private ScrollRect scrollRect;
    private void Awake () {
        scrollRect = GetComponent<ScrollRect> ();
    }
    private void OnEnable () {
        GameEvent.instance.OnToggleScroll += ToggleScroll;
    }
    private void OnDisable () {
        GameEvent.instance.OnToggleScroll -= ToggleScroll;
    }

    void ToggleScroll (bool _toggle) {
        scrollRect.horizontal = _toggle;
    }
}