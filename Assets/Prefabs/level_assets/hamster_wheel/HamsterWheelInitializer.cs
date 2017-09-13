using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterWheelInitializer : MonoBehaviour {

    void Reset() {
        var colliders = GetComponentsInChildren<BoxCollider>();

        for (int i = colliders.Length - 1; i > 0; i--) {
            DestroyImmediate(colliders[i].gameObject);
        }

        var collider = colliders[0];
        int counter = 0;

        for (float i = -140.0f; i < 205.1f; i += 5.0f) {
           var obj = GameObject.Instantiate(collider.gameObject, transform.position, Quaternion.Euler(0.0f, 0.0f, i), gameObject.transform.GetChild(0));
            obj.name = "collider_" + counter++;
        }


    }
    
}
