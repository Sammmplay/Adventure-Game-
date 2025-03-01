using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZones : MonoBehaviour
{
    [SerializeField] int damage;
    void OnTriggerStay2D(Collider2D other) {
        PlayerController controller = other.GetComponent<PlayerController>();
        if (controller != null) {
            controller.ChangeHealth(damage);
        }
    }
}
