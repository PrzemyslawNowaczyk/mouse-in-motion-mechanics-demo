using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Scenes Repository", fileName = "scenes_repository")]
[System.Serializable]
public class DemoScenesRepository : ScriptableObject {

    public SceneField[] Scenes;
}
