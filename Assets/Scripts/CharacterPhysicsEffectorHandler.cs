using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysicsEffectorHandler : PhysicsEffectorHandler {

    CharacterScript _character;

    private new void Awake() {
        base.Awake();
        _character = GetComponent<CharacterScript>();
    }

    public override void AddForce(Vector3 force, ForceMode mode) {
        base.AddForce(force, mode);
    }

    public override void SetVelocity(Vector3 velocity) {
        _character.SetExternalVelocity(velocity);

        if (velocity.x > 0) {
            //_character.FaceDirection = 1;
        }
        else if (velocity.x < 0) {
            //_character.FaceDirection = -1;
        }

    }
}
