using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager{

    public static DialogueManager Instance {
        get {
            if (instance == null) {
                instance = new DialogueManager();
            }
            return instance;
        }
    }

    private static DialogueManager instance;

    private DialogueManagerComponent _component;
    private Queue<DialogueLine> _dialogueLines;


    private DialogueManager() {
        _dialogueLines = new Queue<DialogueLine>();
    }

    public void RegisterComponent(DialogueManagerComponent component) {
        _component = component;
    }

    public void DisplayDialogue(Dialogue dialogue) {
        _dialogueLines.Clear();

        Time.timeScale = 0.0f;

        for (int i = 0; i < dialogue.lines.Length; i++) {
            _dialogueLines.Enqueue(dialogue.lines[i]);
        }

        _component.HideDialogueBox(false);

        NextDialogueLine();
    }

    public void NextDialogueLine() {
        if (_dialogueLines.Count == 0) {
            _component.HideDialogueBox(true);
            Time.timeScale = 1.0f;
            return;
        }

        DialogueLine line = _dialogueLines.Dequeue();
        _component.DisplayDialogue(line.Name, line.Text);
    }

}
