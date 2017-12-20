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

    public static Vec3 operator *(Quat quat, Vec3 vec)
    {
        float num = quat.x * 2f;
        float num2 = quat.y * 2f;
        float num3 = quat.z * 2f;
        float num4 = quat.x * num;
        float num5 = quat.y * num2;
        float num6 = quat.z * num3;
        float num7 = quat.x * num2;
        float num8 = quat.x * num3;
        float num9 = quat.y * num3;
        float num10 = quat.w * num;
        float num11 = quat.w * num2;
        float num12 = quat.w * num3;
        Vec3 result = new Vec3();
        result.x = (1f - (num5 + num6)) * vec.x + (num7 - num12) * vec.y + (num8 + num11) * vec.z;
        result.y = (num7 + num12) * vec.x + (1f - (num4 + num6)) * vec.y + (num9 - num10) * vec.z;
        result.z = (num8 - num11) * vec.x + (num9 + num10) * vec.y + (1f - (num4 + num5)) * vec.z;
        return result;
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

    public float SquareMagnitude()
    {
        return x * x + y * y + z * z;
    }

    public float SquareMagnitude(Vec3 vec3)
    {
        return vec3.x * vec3.x + vec3.y * vec3.y + vec3.z * vec3.z;
    }

    public static Vec3 ProjectAndCreate(Vec3 u, Vec3 v)
    {
        float d = u.DotProduct(v);
        float d_div = d / v.SquareMagnitude();
        return v * d_div;
    }

    public static void OrthoNormalize(Vec3 v1, Vec3 v2)
    {
        v1.Normalize();

        Vec3 sub = new Vec3(v2 - Vec3.ProjectAndCreate(v2, v1));
        sub.Normalize();
    }

}