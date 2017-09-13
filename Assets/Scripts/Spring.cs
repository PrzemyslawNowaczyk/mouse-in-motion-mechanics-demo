using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Spring : MonoBehaviour {

    public bool OverwriteVelocity;
    public float Velocity;

    private void OnTriggerEnter(Collider other) {
        var _handler = other.gameObject.GetComponentInParent<PhysicsEffectorHandler>();

        if (_handler) {

            //Debug.Log("Handler found!");
            if (OverwriteVelocity) {
                _handler.SetVelocity(transform.rotation * Vector3.up * Velocity);
            }
            else {
                var relativeVelocity = transform.InverseTransformDirection(_handler.Velocity);
                var bumpedRelativeVelocity = relativeVelocity.AlterY(Velocity);
                var bumpedVelocity = transform.TransformDirection(bumpedRelativeVelocity);
                _handler.SetVelocity(bumpedVelocity);
            }
        }
        else {
            Debug.Log("Handler not found!");
        }
    }

    private void Reset() {
        var _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("PhysicsEffector");
    }
}
