using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec3 {

    public float x, y, z;
    [HideInInspector]
    public static Vec3 back = new Vec3(0, 0, -1);
    public static Vec3 forward = new Vec3(0, 0, 1);
    public static Vec3 down = new Vec3(0, -1, 0);
    public static Vec3 up = new Vec3(0, 1, 0);
    public static Vec3 left = new Vec3(-1, 0, 0);
    public static Vec3 right = new Vec3(1, 0, 0);
    public static Vec3 one = new Vec3(1, 1, 1);
    public static Vec3 zero = new Vec3(0, 0, 0);

    /// <summary>
    /// Constructors
    /// </summary>
    public Vec3() {
        x = 0;
        y = 0;
        z = 0;
    }

    public Vec3(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vec3(Vec3 v) {
        x = v.x;
        y = v.y;
        z = v.z;
    }

    /// <summary>
    /// Operator Overloadings
    /// We use implicit to enable conversion betwwen our vector3 class & unity's 
    /// </summary>
    public static implicit operator Vector3(Vec3 v) {
        return new Vector3(v.x, v.y, v.z);
    }

    public static implicit operator Vec3(Vector3 v) {
        return new Vec3(v.x, v.y, v.z);
    }

    public static Vec3 operator +(Vec3 v1, Vec3 v2) {
        Vec3 vec = new Vec3(); ;
        vec.x = v1.x + v2.x;
        vec.y = v1.y + v2.y;
        vec.z = v1.z + v2.z;
        return vec;
    }

    public static Vec3 operator -(Vec3 v) {
        return new Vec3(-v.x, -v.y, -v.z);
    }

    public static Vec3 operator -(Vec3 v1, Vec3 v2) {
        Vec3 vec = new Vec3(); ;
        vec.x = v1.x - v2.x;
        vec.y = v1.y - v2.y;
        vec.z = v1.z - v2.z;
        return vec;
    }

    public static Vec3 operator *(Vec3 v, float scalar) {
        Vec3 vec = new Vec3(); ;
        vec.x = v.x * scalar;
        vec.y = v.y * scalar;
        vec.z = v.z * scalar;
        return vec;
    }

    public static Vec3 operator *(float scalar, Vec3 v) {
        Vec3 vec = new Vec3(); ;
        vec.x = v.x * scalar;
        vec.y = v.y * scalar;
        vec.z = v.z * scalar;
        return vec;
    }

    /// <summary>
    /// Necessary & Utility Methods
    /// </summary>
    public float Module() {
        return Mathf.Sqrt(x*x + y*y + z*z);
    }

    public float DotProduct(Vec3 v) {
        return x * v.x + y * v.y + z * v.z;
    }

    public Vec3 Normalize() {
        Vec3 vec = new Vec3();
        vec.x = x / Module();
        vec.y = y / Module();
        vec.z = z / Module();
        return vec;
    }

    public Vec3 CrossProduct(Vec3 v) {
        Vec3 vec = new Vec3();
        vec.x = y * v.z - z * v.y;
        vec.y = z * v.x - x * v.z;
        vec.z = x * v.y - y * v.x;
        return vec;
    }

    public float Distance(Vec3 v) {
        float vec = Mathf.Pow((v.x - x), 2) + Mathf.Pow((v.y - y), 2) + Mathf.Pow((v.z - z), 2);
        return Mathf.Sqrt(vec);
    }

}