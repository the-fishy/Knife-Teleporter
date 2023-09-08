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
        throwWeapon = player.GetComponent<ThrowWeapon>();
        timeScaler = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimeScaler>();
    }

    void Update()
    {
        if (Input.GetKey(teleportKey) && throwWeapon.WeaponThrown()) {
            timeScaler.SlowDownTime();

            player.SetActive(false); // disable and re-enable so player can teleport properly
            player.transform.position = transform.position;
            player.SetActive(true);

            Destroy(this.gameObject);
            throwWeapon.increaseThrowCounter();
        }
    }


}
