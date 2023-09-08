using UnityEngine;

public class ProjectileStickToObject : MonoBehaviour
{
    Rigidbody rb;

    bool targetHit;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (targetHit) { return; }

        if (collision.gameObject.tag == "Stickable") {
            targetHit = true;

            // makes projectile stick to surface
            rb.isKinematic = true;
            
            // NEED to have hit objects scale to be 1 to use this
            transform.SetParent(collision.transform);
        }
    }

    public bool TargetHit() {
        return targetHit;
    }
}
