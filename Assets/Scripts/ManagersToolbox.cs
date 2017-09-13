using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManagersToolbox  {

    public static CameraManager CameraManager { get { return CameraManager.Instance; } }
    public static DialogueManager DialogueManager { get { return DialogueManager.Instance; } }

}
