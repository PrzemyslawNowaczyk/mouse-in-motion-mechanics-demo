using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAnimation : MonoBehaviour {

    public Vector3 Rotation;
    public float RotationSpeed = 0.1f;
    public Vector3 Wander;
    public float WanderSpeed = 0.1f;

    private Vector3 startPosition;

    private void Awake() {
        startPosition = transform.localPosition;
    }

    private void FixedUpdate() {
        transform.Rotate(Rotation.normalized * RotationSpeed);
        transform.localPosition = startPosition + Wander * Mathf.Sin(Time.fixedTime * WanderSpeed);
    }
}
