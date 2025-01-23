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
        if (transform.position.magnitude > 100.0f) {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        enemiesController enemy = collision.GetComponent<enemiesController>();
        if (enemy != null) {
            enemy.Fix();
        }
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
  }
}
