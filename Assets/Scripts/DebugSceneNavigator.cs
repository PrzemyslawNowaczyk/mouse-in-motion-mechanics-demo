using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneNavigator : MonoBehaviour {

    [SerializeField]
    DemoScenesRepository demoScenes;

    void OnGUI() {

        for (int i = 0; i < demoScenes.Scenes.Length; i++) {

            if (GUI.Button(new Rect(10 , 10 + i * 35, 100, 30), demoScenes.Scenes[i].SceneName)) {
                SceneManager.LoadScene(demoScenes.Scenes[i]);
            }

        }


    }
}
