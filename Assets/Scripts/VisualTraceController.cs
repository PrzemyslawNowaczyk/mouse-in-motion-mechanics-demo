using System;
using System.Collections.Generic;
using UnityEngine;

public class VisualTraceController : MonoBehaviour {

    [SerializeField]
    TrailRenderer _trail;
    CharacterScript _character;

    private void Awake() {
        _character = GetComponentInParent<CharacterScript>();
    }

    private void OnEnable() {
        _character.OnSpeedLevelChanged.AddListener(SetTrailColor);
    }

    private void OnDisable() {
        _character.OnSpeedLevelChanged.RemoveListener(SetTrailColor);
    }

    private void SetTrailColor(int speedLevelNumber, CharacterScript.SpeedLevel speedLevel) {
        _trail.colorGradient = speedLevel.ColorGradient;
    }
}
