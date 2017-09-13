using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFloorDistanceChecker : MonoBehaviour {

    public LayerMask RaycastMask;
    public float RaycastRange;
    Ray ray;

    [HideInInspector]
    public float Distance;

	void Update () {
        RaycastHit hit;
        ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out hit, RaycastRange, RaycastMask.value)) {
            Distance = hit.distance / RaycastRange;
        }
        else {
            Distance = 1.0f;
        }
	}
}
