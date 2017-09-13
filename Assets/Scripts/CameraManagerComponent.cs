using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagerComponent : MonoBehaviour {

    public CameraShake Shaker;

    private void Awake() {
        ManagersToolbox.CameraManager.RegisterCameraManagerComponent(this);
    }
}
