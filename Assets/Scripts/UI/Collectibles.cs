using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [SerializeField] float _value;

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController controller = collision.GetComponent<PlayerController>();


        if (controller != null && controller.health<controller.maxHealth) {
            controller.ChangeHealth((int)_value);
            
            Destroy(gameObject);
        }
    }
}
