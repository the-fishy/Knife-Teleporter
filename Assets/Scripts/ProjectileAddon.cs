using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    Rigidbody rb;

    bool targetHit;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (targetHit) { return; }

        if (collision.gameObject.tag == "Stickable") {
            targetHit = true;

            // makes projectile stick to surface
            rb.isKinematic = true;
            //rb.useGravity = false;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;

            // make projectile move with object it collided with
            //transform.SetParent(collision.transform, true);
        }

    }
}
