using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFPS : MonoBehaviour {

    public int TargetFPS = 60;

    private void Awake() {
        Application.targetFrameRate = TargetFPS;
    }

    private void OnValidate() {
        Application.targetFrameRate = TargetFPS;
    }
}
