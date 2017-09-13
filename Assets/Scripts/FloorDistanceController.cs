using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is for sure not the final solution, but it's good enough for now

public class FloorDistanceController : MonoBehaviour {

    public CharacterFloorDistanceChecker Left, Right;

	void LateUpdate () {
        if (Left.Distance < 1.0f || Right.Distance < 1.0f) {
            var a = Left.Distance - Right.Distance;
            var b = Vector3.Distance(Left.transform.localPosition, Right.transform.localPosition);
            var angle = Mathf.Atan(a / b) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -90.0f, 90.0f);
            transform.Rotate(Vector3.forward * 0.3f *  angle);
        }
        else {
            var rotation = transform.rotation.eulerAngles;
            if (rotation.z > 180.0f) {
                rotation.z = 180.0f - rotation.z;
            }
            transform.Rotate(-rotation * 0.1f);
        }
	}
}
