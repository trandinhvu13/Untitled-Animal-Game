using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour {
    public float speed;
    public float clampPos;
    private Vector3 startPos;
    public GameObject childGameObject;

    private void Start() {
        startPos = transform.position;
        clampPos = childGameObject.transform.position.x - gameObject.transform.position.x;
    }

    private void Update() {
        float newPos = Mathf.Repeat(Time.time*speed, clampPos);
        transform.position = startPos + Vector3.left * newPos;
    }
}