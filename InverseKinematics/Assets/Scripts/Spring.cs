using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    public Transform target; // rest
    private Transform localTransform;
    public GameObject child;

    public Vec3 position;
    public Vec3 velocity;
    public Vec3 mytarget;

    public float zeta = 0.1f; // damping ratio
    public float omega = 5f; // angular frequency

    // Use this for initialization
    void Start() {
        localTransform = GetComponent<Transform>();
        position = new Vec3(localTransform.position.x, localTransform.position.y, localTransform.position.z);
        velocity = new Vec3(0, 0, 0);
        mytarget = new Vec3(target.position.x, target.position.y, target.position.z); 
    }

    // Update is called once per frame
    void Update() {
        spring(Time.deltaTime);
        localTransform.localScale = new Vec3(localTransform.localScale.x, localTransform.localScale.y, -position.z); // Scale
        mytarget = new Vec3(target.position.x, target.position.y, target.position.z);
    }

    private void spring(float dt) {
        if (zeta >= 1)
            return;
        if (zeta < 0)
            zeta = 0;

        // note that since we're moving by dt x0 = position - target and v0 = velocity
        // the catch is we have to update velocity every call thereby also using the velocity function we derived

        Vec3 x0 = position - mytarget;
        float omegaZeta = omega * zeta;
        float alpha = omega * Mathf.Sqrt(1.0f - zeta * zeta);
        float exp = Mathf.Exp(-dt * omegaZeta);
        float cos = Mathf.Cos(dt * alpha);
        float sin = Mathf.Sin(dt * alpha);

        Vec3 c2 = (velocity + x0 * omegaZeta) / alpha;

        position = mytarget + exp * (x0 * cos + c2 * sin);
        velocity = -exp * ((x0 * omegaZeta - c2 * alpha) * cos + (x0 * alpha + c2 * omegaZeta) * sin);
    }
}