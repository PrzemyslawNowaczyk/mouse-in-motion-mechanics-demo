using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManagerComponent : MonoBehaviour {

    public Text NameText;
    public Text DialogueText;

    void Awake() {
        ManagersToolbox.DialogueManager.RegisterComponent(this);
        HideDialogueBox(true);
    }

    public void HideDialogueBox(bool hide) {
        gameObject.SetActive(!hide);
    }

    public void DisplayDialogue(string name, string text) {
        NameText.text = name;
        DialogueText.text = text;
    }

    private void Update() {
        if (gameObject.activeSelf) {
            if (Input.GetButtonDown("Use")) {
                ManagersToolbox.DialogueManager.NextDialogueLine();
            }
        }
    }

}
