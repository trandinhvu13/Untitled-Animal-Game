using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        SecurePlayerPrefs.Init();
    }

}