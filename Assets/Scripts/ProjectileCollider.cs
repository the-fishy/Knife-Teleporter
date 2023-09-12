using System;
using System.Collections;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    [SerializeField] float massIncrease;
    [SerializeField] float dragIncrease;
    [SerializeField] float velocityDecreaseAmount;

    Rigidbody rb;

    bool targetHit;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        

        if (collision.gameObject.tag == "Stickable") {
            targetHit = true;

            // makes projectile stick to surface
            try {
                rb.isKinematic = true;

                // NEED to have hit objects scale to be 1 to use this
                transform.SetParent(collision.transform);
            } catch (Exception e) {
                Debug.Log("Error");
            }
            
        } else {
            rb.drag = dragIncrease;
            rb.mass *= massIncrease;
            // TODO increase velocity decrease amount by ray cast out distance
            // TODO increase upwards force by ray cast out distance

            rb.velocity *= velocityDecreaseAmount;
            rb.angularVelocity *= velocityDecreaseAmount;
            //StartCoroutine(nameof(DecreaseVelocity));

        }


    }

    IEnumerator DecreaseVelocity () {
        rb.velocity *= velocityDecreaseAmount * Time.deltaTime;
        rb.angularVelocity *= velocityDecreaseAmount * Time.deltaTime;
        yield return null;
    }

    public bool TargetHit() {
        return targetHit;
    }
}
