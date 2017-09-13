using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExt{

    public static Vector2 AlterX(this Vector2 v, float newX) {
        return new Vector2(newX, v.y);
    }

    public static Vector2 AlterY(this Vector2 v, float newY) {
        return new Vector2(v.x, newY);
    }


    public static Vector3 AlterX(this Vector3 v, float newX) {
        return new Vector3(newX, v.y, v.z);
    }

    public static Vector3 AlterY(this Vector3 v, float newY) {
        return new Vector3(v.x, newY, v.z);
    }

    public static Vector3 AlterZ(this Vector3 v, float newZ) {
        return new Vector3(v.x, v.y, newZ);
    }


    public static Vector2 OnlyX(this Vector2 v) {
        return new Vector2(v.x, 0.0f);
    }

    public static Vector2 OnlyY(this Vector2 v) {
        return new Vector2(0.0f, v.y);
    }


    public static Vector3 OnlyX(this Vector3 v) {
        return new Vector3(v.x, 0.0f, 0.0f);
    }

    public static Vector3 OnlyY(this Vector3 v) {
        return new Vector3(0.0f, v.y, 0.0f);
    }

    public static Vector3 OnlyZ(this Vector3 v) {
        return new Vector3(0.0f, 0.0f, v.z);
    }

    /// <summary>
    /// Converts Vector3 to Vector2 with matching x and y.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector2 Vector2(this Vector3 v) {
        return new Vector2(v.x, v.y);
    }

    /// <summary>
    /// Converts Vector2 to Vector3 (x, y, 0.0f).
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 Vector3(this Vector2 v) {
        return new Vector3(v.x, v.y, 0.0f);
    }

    /// <summary>
    /// Converts Vector2 to Vector3 with given Z parameter.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Vector3 AddZ(this Vector2 v, float z) {
        return new Vector3(v.x, v.y, z);
    }

}
