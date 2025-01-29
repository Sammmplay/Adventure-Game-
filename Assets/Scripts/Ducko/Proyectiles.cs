using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectiles : MonoBehaviour
{
    Rigidbody2D _rb;
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Lauch(Vector2 _dir, float force) {
        _rb.AddForce(_dir * force);
    }
    private void Update() {
        if (transform.position.magnitude > 1000) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.TryGetComponent<enemiesController>(out enemiesController enemy)) {
            enemy.Fix();
            Destroy(gameObject);
        }
        /*enemiesController enemy = collision.GetComponent<enemiesController>();
        if (enemy != null) {
            enemy.Fix();
        }*/
        
    }
}
