using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionBeltController : MonoBehaviour {

    public Vector3 Force;
    CharacterScript _charScript;

    private void OnTriggerEnter(Collider collision) {
        _charScript = collision.gameObject.GetComponent<CharacterScript>();
    }

    private void OnTriggerExit(Collider collision) {
        if (collision.gameObject.GetComponent<CharacterScript>() == _charScript) {
            _charScript = null;
        }
    }

    private void FixedUpdate() {
        if (_charScript) {
            //check if it still work
            //_charScript._rigidbody.AddForce(Force, ForceMode.Force);
        }
    }
}
