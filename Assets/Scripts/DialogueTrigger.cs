using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue DialogueToFire;
    public bool DestroyOnFire;

    private void OnTriggerEnter(Collider other) {
        ManagersToolbox.DialogueManager.DisplayDialogue(DialogueToFire);
        if (DestroyOnFire) {
            Destroy(gameObject);
        }
    }

}
