using UnityEngine;

//The base abstract class for all Rotation limits

public abstract class RotationLimit : MonoBehaviour
{

    //The main axis of the rotation limit.
    public Vec3 axis = Vec3.forward;

    /// Map the zero rotation point to the current rotation
    public void SetDefaultLocalRotation()
    {
        Quat localRot = new Quat(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        defaultLocalRotation = localRot;
    }

    //Returns the limited local rotation.
    public Quat GetLimitedLocalRotation(Quat localRotation, out bool changed)
    {
        // Making sure the Rotation Limit is initiated
        if (!initiated) Awake();

        // Subtracting defaultLocalRotation
        Quat rotation = Quat.Inverse(defaultLocalRotation) * localRotation;

        Quat limitedRotation = LimitRotation(rotation);
        changed = limitedRotation != rotation;

        if (!changed) return localRotation;

        // Apply defaultLocalRotation back on
        return defaultLocalRotation * limitedRotation;
    }

    //Apply the rotation limit to transform.localRotation. Returns true if the limit has changed the rotation.
    public bool Apply()
    {
        bool changed = false;

        Quat localRot = new Quat(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
        Quat limitedLocalRot = new Quat(GetLimitedLocalRotation(localRot, out changed).x, GetLimitedLocalRotation(localRot, out changed).y, GetLimitedLocalRotation(localRot, out changed).z, GetLimitedLocalRotation(localRot, out changed).w);
        transform.localRotation.Set(limitedLocalRot.x, limitedLocalRot.y, limitedLocalRot.z, limitedLocalRot.w); 

        return changed;
    }

    //Disable this instance making sure it is initiated. Use this if you intend to manually control the updating of this Rotation Limit.
    public void Disable()
    {
        if (initiated)
        {
            enabled = false;
            return;
        }

        Awake();
        enabled = false;
    }

    //An arbitrary secondary axis that we get by simply switching the axes  
    public Vec3 secondaryAxis { get { return new Vec3(axis.y, axis.z, axis.x); } }

    //Cross product of axis and secondaryAxis 
    public Vec3 crossAxis { get { return axis.CrossProduct(secondaryAxis); } }

        
    //The default local rotation of the gameobject. By default stored in Awake.
    [HideInInspector] public Quat defaultLocalRotation;

    protected abstract Quat LimitRotation(Quat rotation);

    private bool initiated;
    private bool applicationQuit;

    //Initiating the Rotation Limit
    void Awake()
    {
        // Store the local rotation to map the zero rotation point to the current rotation
        SetDefaultLocalRotation();

        if (axis == Vec3.zero) Debug.LogError("Axis is Vector3.zero.");
        initiated = true;
    }

    /*
    Using LateUpdate here because you most probably want to apply the limits after animation. 
    If you need precise control over the execution order, disable this script and call Apply() whenever you need
    */
    void LateUpdate()
    {
        Apply();
    }


    //Limits rotation to a single degree of freedom (along axis)
    protected static Quat Limit1DOF(Quat rotation, Vec3 axis)
    {
        return Quat.RotateFromTo(rotation * axis, axis) * rotation;
    }

    //Applies uniform twist limit to the rotation
    protected static Quat LimitTwist(Quat rotation, Vec3 axis, Vec3 orthoAxis, float twistLimit)
    {
        twistLimit = Mathf.Clamp(twistLimit, 0, 180);
        if (twistLimit >= 180) return rotation;

        Vec3 normal = rotation * axis;
        Vec3 orthoTangent = orthoAxis;
        Vec3.OrthoNormalize(normal, orthoTangent);

        Vec3 rotatedOrthoTangent = rotation * orthoAxis;
        Vec3.OrthoNormalize(normal, rotatedOrthoTangent);

        Quat fixedRotation = Quat.RotateFromTo(rotatedOrthoTangent, orthoTangent) * rotation;

        if (twistLimit <= 0) return fixedRotation;

        // Rotate from zero twist to free twist by the limited angle
        return Quat.RotateTowards(fixedRotation, rotation, twistLimit);
    }
}
