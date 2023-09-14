using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayerToThis : MonoBehaviour
{
    KeyCode teleportKey = KeyCode.Mouse2;

    GameObject player;

    TimeScaler timeScaler;
    ThrowWeapon throwWeapon;
    ProjectileCollider projectileCollider;


    void Start()
    {
        projectileCollider = GetComponent<ProjectileCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        throwWeapon = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<ThrowWeapon>();
        timeScaler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimeScaler>();
    }

    void Update()
    {
        if (Input.GetKey(teleportKey) && throwWeapon.WeaponThrown()) {
            timeScaler.SlowDownTime();

            player.SetActive(false); // disable and re-enable so player can teleport properly
            player.transform.position = transform.position;
            player.SetActive(true);


            //Destroy(this.gameObject);
            DestroyWeapon();
            throwWeapon.increaseThrowCounter();
            throwWeapon.ChangeGameObjectVisibility(throwWeapon.GetThrowableBlade());
            // cancel change switch boolean
        }
    }

    void DestroyWeapon() {
        if (!throwWeapon.GetThrowCoRoutineState()) {
            Destroy(this.gameObject);
            return;
        }

        if (this.GetComponent<MeshRenderer>().enabled) {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<MeshCollider>().enabled = false;
        }

        Invoke(nameof(DestroyWeapon), 1f);

    }

}
