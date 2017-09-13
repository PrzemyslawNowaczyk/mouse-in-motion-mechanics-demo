using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraSizeToVelocity : MonoBehaviour {

    private Camera _camera;
    public CharacterScript target;

    private float smoothVelocity;

    [Header("Ortographic")]
    public float OrtographicSmoothFactor = 0.9f;
    public float OrtographicLowerLimit;
    public float OrtographicUpperLimit;

    [Header("Perspective")]
    public float PerspectiveSmoothFactor = 0.9f;
    public Vector3 PerspectiveClosestCamera;
    public Vector3 PerspectiveFarCamera;

    [Header("Other")]
    public float MaxVelocity;

    private void Awake() {
        _camera = GetComponent<Camera>();
    }

    void FixedUpdate () {
        

        if (_camera.orthographic) {
            smoothVelocity = Mathf.Lerp(smoothVelocity, target.GlobalVelocity.magnitude, OrtographicSmoothFactor * Time.fixedDeltaTime);
            _camera.orthographicSize = Mathf.Lerp(OrtographicLowerLimit, OrtographicUpperLimit, smoothVelocity / MaxVelocity);
        }
        else {
            smoothVelocity = Mathf.Lerp(smoothVelocity, target.GlobalVelocity.magnitude, PerspectiveSmoothFactor * Time.fixedDeltaTime);
            _camera.transform.localPosition = Vector3.Lerp(PerspectiveClosestCamera, PerspectiveFarCamera, smoothVelocity / MaxVelocity);
        }
        
	}
}
