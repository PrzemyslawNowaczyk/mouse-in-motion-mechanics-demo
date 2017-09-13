using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<CharacterScript>()) {
            Debug.Log("+1 coin");
            Destroy(gameObject);
        }
    }
}
