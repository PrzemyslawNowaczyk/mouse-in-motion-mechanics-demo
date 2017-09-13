using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSettings : MonoBehaviour {

    public Spring bumper;

    public bool OverwriteVelocity;
    public float Velocity;

    private void OnValidate() {
        SetSettings();
    }

    private void Reset() {
        SetSettings();
    }

    private void OnEnable() {
        SetSettings();
    }

    void SetSettings() {
        bumper.OverwriteVelocity = OverwriteVelocity;
        bumper.Velocity = Velocity;
    }

}
