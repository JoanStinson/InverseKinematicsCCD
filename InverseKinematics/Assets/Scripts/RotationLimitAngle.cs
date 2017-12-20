using UnityEngine;



//Simple angular rotation limit.

public class RotationLimitAngle : RotationLimit {

	//The swing limit.
	[Range(0f, 180f)] public float limit = 45;

	//Limit of twist rotation around the main axis.
	[Range(0f, 180f)] public float twistLimit = 180;
				
	//Limits the rotation in the local space of this instance's Transform.
	protected override Quat LimitRotation(Quat rotation) {		
		// Subtracting off-limits swing
		Quat swing = LimitSwing(rotation);
			
		// Apply twist limits
		return LimitTwist(swing, axis, secondaryAxis, twistLimit);
	}
		
		
	//Apply swing limits
	private Quat LimitSwing(Quat rotation) {
		if (axis == Vec3.zero) return rotation; // Ignore with zero axes
		if (rotation == Quat.identity) return rotation; // Assuming initial rotation is in the reachable area
		if (limit >= 180) return rotation;
			
		Vec3 swingAxis = rotation * axis;
			
		// Get the limited swing axis
		Quat swingRotation = Quat.RotateFromTo(axis, swingAxis);
		Quat limitedSwingRotation = Quat.RotateTowards(Quat.identity, swingRotation, limit);
			
		// Rotation from current(illegal) swing rotation to the limited(legal) swing rotation
		Quat toLimits = Quat.RotateFromTo(swingAxis, limitedSwingRotation * axis);
			
		// Subtract the illegal rotation
		return toLimits * rotation;
	}
}

