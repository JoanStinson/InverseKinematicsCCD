﻿using System.Collections;
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

    public static Quat Inverse(Quat q)
    {
        float temp = q.x * q.x + q.y * q.y + q.z * q.z + q.w * q.w;
        q.w = q.w * (1 / temp);
        q.x = -q.x * (1 / temp);
        q.y = -q.y * (1 / temp);
        q.z = -q.z * (1 / temp);
        return q;
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

    public static Quat Slerp(Quat q1, Quat q2, float t)
    {
        // Check for equality and skip operation.
        if (q1.Equals(q2))
        {
            return q1;
        }

        float scale0 = 1 - t;
        float scale1 = t;

        Quat myQuat = Quat.Identity;
        myQuat.x = (scale0 * q1.x) + (scale1 * q2.x);
        myQuat.y = (scale0 * q1.y) + (scale1 * q2.y);
        myQuat.z = (scale0 * q1.z) + (scale1 * q2.z);
        myQuat.w = (scale0 * q1.w) + (scale1 * q2.w);

        return myQuat.Normalize();
    }

    public static float Angle(Quat a, Quat b)
    {
        Quat inv = Inverse(a);
        Quat res = b * inv;
        return Mathf.Acos(res.w) * 2.0f * 57.2957795f; // To degrees!;
    }

    public static Quat RotateTowards(Quat from, Quat to, float maxDegreesDelta)
    {
        float num = Quat.Angle(from, to);
        Quat result;
        if (num == 0f)
        {
            result = to;
        }
        else
        {
            float t = Mathf.Min(1f, maxDegreesDelta / num);
            result = Quat.Slerp(from, to, t);
        }
        return result;
    }

    public static Quat RotateFromTo(Vec3 source, Vec3 target)
    {
        source.Normalize();
        target.Normalize();

        Vec3 axis = source.CrossProduct(target);

        Vec3 oldLength = axis.Normalize();

        if (oldLength != Vec3.zero)
        {
            float halfCosAngle = 0.5f * source.DotProduct(target);
            float cosHalfAngle = Mathf.Sqrt(0.5f + halfCosAngle);
            float sinHalfAngle = Mathf.Sqrt(0.5f - halfCosAngle);
            return new Quat(axis.x * sinHalfAngle, axis.y * sinHalfAngle, axis.z * sinHalfAngle, cosHalfAngle);
        }
        else
            return new Quat(1.0f, 0.0f, 0.0f, 0.0f);
    }
}
