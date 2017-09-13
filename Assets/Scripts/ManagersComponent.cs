using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagersComponent : MonoBehaviour {

    private void Awake() {
        //initializing
        ManagersToolbox.CameraManager.Equals(null);
        ManagersToolbox.DialogueManager.Equals(null);
    }

}
