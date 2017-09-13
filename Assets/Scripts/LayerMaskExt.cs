using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskExt {

    /// <summary>
    /// Check if flag of given value is set in layer mask
    /// </summary>
    /// <param name="layermask"></param>
    /// <param name="layer">flag to check</param>
    /// <returns></returns>
    public static bool IsSet(this LayerMask layermask, int layer) {
        return layermask == (layermask | (1 << layer));
    }

    /// <summary>
    /// Check if flag of given name is set in layer mask
    /// </summary>
    /// <param name="layermask"></param>
    /// <param name="layer">flag to check</param>
    /// <returns></returns>
    public static bool Contains(this LayerMask layermask, string layer) {
        return IsSet(layermask, LayerMask.NameToLayer(layer));
    }

    public static LayerMask WithBitChanged(this LayerMask layermask, int layer, bool value) {

        LayerMask lm = layermask;

        int mask = 1 << layer;

        if (value) {
            lm.value |= mask;
        }
        else {
            lm.value &= ~mask;
        }

        return lm;
    }
}
