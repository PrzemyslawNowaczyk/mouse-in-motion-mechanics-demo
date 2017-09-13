using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DialogueLine {
    public string Name;
    [TextArea]
    public string Text;
}

[CreateAssetMenu(menuName = "Dialogue", fileName = "new_dialogue")]
public class Dialogue : ScriptableObject {

    public DialogueLine[] lines;

}
