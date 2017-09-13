using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceRotationReset : MonoBehaviour {

    Transform _transform;

    private void Awake() {
        _transform = transform;
    }

    private void FixedUpdate() {
        _transform.rotation = Quaternion.identity;
    }

}
