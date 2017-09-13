using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathf2 {
    
    public static float Sign3(float value) {
        if (value < 0.0f) {
            return -1.0f;
        }
        if (value > 0.0f) {
            return 1.0f;
        }
        return 0.0f;
    }

}