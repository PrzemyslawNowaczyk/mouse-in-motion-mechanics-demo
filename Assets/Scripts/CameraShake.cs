using UnityEngine;
using System.Collections;

//http://answers.unity3d.com/questions/212189/camera-shake.html

public class CameraShake : MonoBehaviour {

    private Vector3 _originalPos;
    private float _shakeDecay = 0.0f;
    private float _shakeIntensity = 0.0f;

    public

    void Start() {
        _shakeIntensity = 0.0f;
        _shakeDecay = 1.0f;
    }

    void FixedUpdate() {
        if (_shakeIntensity > 0) {
            transform.localPosition = _originalPos + Random.insideUnitSphere * _shakeIntensity;
            _shakeIntensity -= _shakeDecay * Time.fixedDeltaTime;
        }else {
            transform.localPosition = _originalPos;
        }
    }


    public void DoShake(float intensity, float decayPerSecond) {
        _originalPos = transform.localPosition;
        _shakeIntensity = intensity;
        _shakeDecay = decayPerSecond;
    }


}