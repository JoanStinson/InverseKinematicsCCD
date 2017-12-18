using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quat {

    public float w, x, y, z;
    [HideInInspector]
    public static Quat identity = new Quat(0, 0, 0, 1);
    const float radToDeg = (float)(180.0 / Mathf.PI);
    const float degToRad = (float)(Mathf.PI / 180.0);

    /// <summary>
    /// Constructors
    /// </summary>
    public Quat() {
        w = 0;
        x = 0;
        y = 0;
        z = 0;
    }

    public Quat(float x, float y, float z, float w) {
        this.w = w;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Quat(Quat q) {
        w = q.w;
        x = q.x;
        y = q.y;
        z = q.z;
    }

    /// <summary>
    /// Operator Overloadings
    /// We use implicit to enable conversion betwwen our vector3 class & unity's 
    /// </summary>
    public static implicit operator Quaternion(Quat q) {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }

    public static implicit operator Quat(Quaternion q) {
        return new Quat(q.x, q.y, q.z, q.w);
    }

    public static Quat operator +(Quat q1, Quat q2) {
        Quat quat = Identity;
        quat.x = q1.x + q2.x;
        quat.y = q1.y + q2.y;
        quat.z = q1.z + q2.z;
        quat.w = q1.w + q2.w;
        return quat;
    }

    public static Quat operator -(Quat q) {
        Quat quat = Identity;
        quat.x = -q.x;
        quat.y = -q.y;
        quat.z = -q.z;
        quat.w = -q.w;
        return quat;
    }

    public static Quat operator -(Quat q1, Quat q2) {
        Quat quat = Identity;
        quat.x = q1.x - q2.x;
        quat.y = q1.y - q2.y;
        quat.z = q1.z - q2.z;
        quat.w = q1.w - q2.w;
        return quat;
    }

    public static Quat operator *(Quat q1, Quat q2) {
        Quat quat = new Quat();
        quat.w = q2.w * q1.w - q2.x * q1.x - q2.y * q1.y - q2.z * q1.z;
        quat.x = q2.w * q1.x + q2.x * q1.w - q2.y * q1.z + q2.z * q1.y;
        quat.y = q2.w * q1.y + q2.x * q1.z + q2.y * q1.w - q2.z * q1.x;
        quat.z = q2.w * q1.z - q2.x * q1.y + q2.y * q1.x + q2.z * q1.w;
        return quat;
    }

    /// <summary>
    /// Necessary & Utility Methods
    /// </summary>
    public float Module() {
        return Mathf.Sqrt(w*w + x*x + y*y + z*z);
    }

    public Quat Normalize() {
        Quat quat = new Quat();
        float sqrt = Mathf.Sqrt(x*x + y*y + z*z + w*w);
        quat.w = w * (1 / sqrt);
        quat.x = x * (1 / sqrt);
        quat.y = y * (1 / sqrt);
        quat.z = z * (1 / sqrt);
        return quat;
    }

    public static Quat Identity {
        get { return identity; }
    }

    public Quat Inverse() {
        Quat quat = new Quat();
        float temp = x*x + y*y + z*z + w*w;
        quat.w = w * (1 / temp);
        quat.x = -x * (1 / temp);
        quat.y = -y * (1 / temp);
        quat.z = -z * (1 / temp);
        return quat;
    }

    public Quat Conjugate() {
        Quat quat = new Quat();
        quat.w = w;
        quat.x = -x;
        quat.y = -y;
        quat.z = -z;
        return quat;
    }

    public static Quat AngleAxis(float angle, Vec3 axis) {
        Quat quat = new Quat();
        angle *= Mathf.Deg2Rad;

        quat.w = Mathf.Cos(angle / 2);
        quat.x = Mathf.Sin(angle / 2) * axis.x;
        quat.y = Mathf.Sin(angle / 2) * axis.y;
        quat.z = Mathf.Sin(angle / 2) * axis.z;
        return quat;
    }
}
