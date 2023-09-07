using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform cam;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject objectToThrow;

    [Header("Settings")]
    [SerializeField] int totalThrows;
    [SerializeField] float throwCooldown;

    [Header("Throwing")]
    [SerializeField] KeyCode throwKey = KeyCode.Mouse0;
    [SerializeField] float throwForce;
    [SerializeField] float throwUpwardForce;

    bool readyToThrow;

    void Start(){
        readyToThrow = true;
    }

    void Update(){
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0) {
            Throw();
        }
    }

    void Throw() {
        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        Vector3 forceDirection = CalculateForceDirection();

        // TODO clean projectiles code, maybe just ref projectile and forceDirection?

        Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRigidBody.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private Vector3 CalculateForceDirection() {
        
        Vector3 forceDirection = cam.transform.forward;
        // TODO add a little bit of offset to the left so it goes straight
        // when not hitting and object, right now it goes to the right

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f)) {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        return forceDirection;
    }

    void ResetThrow() {
        readyToThrow = true;
    }




}
