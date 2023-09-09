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
    KeyCode throwKey = KeyCode.Mouse1;
    [SerializeField] float throwForce;
    [SerializeField] float throwUpwardForce;

    [Header("Recalling")]
    KeyCode recallKey = KeyCode.Alpha1;

    [SerializeField] bool readyToThrow;

    void Start(){
        readyToThrow = true;
    }

    void Update(){
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0) {
            Throw();
        }
        else if (Input.GetKeyDown(recallKey) && !readyToThrow) {
            RecallWeapon();
        }
    }

    public bool WeaponThrown() {
        return !readyToThrow;
    }

    void Throw() {
        // Play throw animation at certain time throw object, destroy one in hand

        readyToThrow = false;

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, attackPoint.rotation);

        Vector3 forceDirection = CalculateForceDirection();

        // TODO clean projectiles code, maybe just ref projectile and forceDirection?

        Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();

        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRigidBody.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        

        Invoke(nameof(ResetThrow), throwCooldown);
    }

    public void increaseThrowCounter() {
        totalThrows++;
    }

    void RecallWeapon() {
        totalThrows++;
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private Vector3 CalculateForceDirection() {
        
        Vector3 forceDirection = cam.transform.forward;
        // TODO add a little bit of offset to the left so it goes straight
        // when not hitting and object, right now it goes to the right

        // closer distance is, less upward force
        

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
