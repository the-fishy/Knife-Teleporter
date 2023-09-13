using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform cam;
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform attackPointAlt;
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

    [SerializeField] Animator throwableAnimator;
    [SerializeField] GameObject bladeSway;

    [SerializeField] bool readyToThrow;

    [SerializeField] bool throwCoRoutineRunning;

    void Start(){
        readyToThrow = true;
    }

    void Update(){
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0) {
            throwableAnimator.SetTrigger("Throw");
            Invoke(nameof(Throw), 0.5f);
        }
        else if (Input.GetKeyDown(recallKey) && !readyToThrow) {
            RecallWeapon();
        }
    }

    public bool WeaponThrown() {
        if (totalThrows == 0) { return true; }
        return false;
    }

    void Throw() {
        Debug.Log("hit object infront at: " + CalculateHitDistance());

        
        // Ray cast from camera... if out hit < 10 change attackpoint to be -10 in z?

        // Play throw animation at certain time throw object, destroy one in hand

        readyToThrow = false;

        GameObject projectile;
        bool addForce = true;

        // TODO if < 1 just drop weapon?
        float distanceToHit = CalculateHitDistance();
        if (distanceToHit < 1.4f) {
            projectile = Instantiate(objectToThrow, attackPointAlt.position, attackPointAlt.rotation);
            addForce = false;
        } else if (distanceToHit < 2.25f) {
            projectile = Instantiate(objectToThrow, attackPointAlt.position, attackPointAlt.rotation);
        } else {
            projectile = Instantiate(objectToThrow, attackPoint.position, attackPoint.rotation);
        }

        if (addForce) {
            Vector3 forceDirection = CalculateForceDirection();

            // TODO clean projectiles code, maybe just ref projectile and forceDirection?

            Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();

            Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

            projectileRigidBody.AddForce(forceToAdd, ForceMode.Impulse);
        }
        

        totalThrows--;

        ChangeGameObjectVisibility(bladeSway);

        //Invoke(nameof(ResetThrow), throwCooldown);
        StartCoroutine(ThrowCooldown());
    }

    public bool GetThrowCoRoutineState() {
        return throwCoRoutineRunning;
    }

    // Breaking when teleport since I destroy object

    // TODO fix coroutine not working after teleporting

    IEnumerator ThrowCooldown() {
        throwCoRoutineRunning = true;
        yield return new WaitForSeconds(throwCooldown);
        readyToThrow = true;
        if (totalThrows > 0) {
            ChangeGameObjectVisibility(bladeSway);
        }
        throwCoRoutineRunning = false;
    }

    public void increaseThrowCounter() {
        totalThrows++;
    }

    void RecallWeapon() {
        increaseThrowCounter();
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    void ChangeGameObjectVisibility(GameObject gameobject) {
        gameobject.SetActive(!gameobject.activeSelf);
    }

    float CalculateHitDistance() {

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 100f)){
            return hit.distance;
        }
        return 1000f;
    }

    Vector3 CalculateForceDirection() {
        
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
        if (totalThrows > 0) {
            ChangeGameObjectVisibility(bladeSway);
        }
    }

}
