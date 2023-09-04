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

        Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRigidBody.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    void ResetThrow() {
        readyToThrow = true;
    }




}
