using UnityEngine;

//The hinge rotation limit limits the rotation to 1 degree of freedom around Axis. This rotation limit is additive which means the limits can exceed 360 degrees.

public class RotationLimitHinge : RotationLimit {

	//Should the rotation be limited around the axis?
	public bool useLimits = true;

	//The min limit around the axis.
	public float min = -45;

	//The max limit around the axis.
	public float max = 90;
		
		
		
	//Limits the rotation in the local space of this instance's Transform.
	protected override Quat LimitRotation(Quat rotation) {
		lastRotation = LimitHinge(rotation);
		return lastRotation;
	}

	[HideInInspector] public float zeroAxisDisplayOffset; // Angular offset of the scene view display of the Hinge rotation limit
		
	private Quat lastRotation = Quat.identity;
	private float lastAngle;
		
		
	//Apply the hinge rotation limit
		 
	private Quat LimitHinge(Quat rotation) {
		// If limit is zero return rotation fixed to axis
		if (min == 0 && max == 0 && useLimits) return Quat.AngleAxis(0, axis);
			
		// Get 1 degree of freedom rotation along axis
		Quat free1DOF = Limit1DOF(rotation, axis);
		if (!useLimits) return free1DOF;

		// Get offset from last rotation in angle-axis representation
		Quat addR = free1DOF * Quat.Inverse(lastRotation);

		float addA = Quat.Angle(Quat.identity, addR);

		Vec3 secondaryAxis = new Vec3(axis.z, axis.x, axis.y);
		Vec3 cross = secondaryAxis.CrossProduct(axis);
		if (cross.DotProduct(addR * secondaryAxis) > 0f) addA = - addA;
			
		// Clamp to limits
		lastAngle = Mathf.Clamp(lastAngle + addA, min, max);
			
		return Quat.AngleAxis(lastAngle, axis);
	}
}

