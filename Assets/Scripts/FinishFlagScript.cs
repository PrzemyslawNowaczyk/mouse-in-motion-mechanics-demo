using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlagScript : MonoBehaviour {
    public Vector3 RotationSpeed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(RotationSpeed * Time.smoothDeltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<CharacterScript>()) {
            Destroy(gameObject);
        }
    }
}
