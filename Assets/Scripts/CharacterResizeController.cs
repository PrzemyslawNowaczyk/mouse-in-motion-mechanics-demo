using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterResizeController : MonoBehaviour {

    Rigidbody _rigidbody;
    bool flatMouse = false;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Q) && !flatMouse){
            transform.localScale = transform.localScale.AlterY(0.4f).AlterX(3.5f);
            transform.Translate(Vector3.down * 0.5f);
            flatMouse = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && flatMouse) {
            transform.localScale = transform.localScale.AlterY(2.0f).AlterX(2.0f);
            transform.Translate(Vector3.up * 0.75f);
            _rigidbody.AddForce(Vector3.up * 250.0f, ForceMode.Impulse);
            flatMouse = false;
        }
    }
}
