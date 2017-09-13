using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsEffectorHandler : MonoBehaviour {

    protected Rigidbody _rigidbody;

    protected void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual Vector3 Velocity {
        get { return _rigidbody.velocity; }
    }

    public virtual void SetVelocity(Vector3 velocity) {
        _rigidbody.velocity = velocity;
    }

    public virtual void AddForce(Vector3 force, ForceMode mode) {
        _rigidbody.AddForce(force, mode);
    }
}
