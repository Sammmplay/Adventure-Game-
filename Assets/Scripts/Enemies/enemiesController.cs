using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemiesController : MonoBehaviour
{
    public float _speed;
    public bool vertical;
    public float changeTime = 3.0f;
    [SerializeField] float timer;
    int direction = 1;
    Rigidbody2D _rb;
    [SerializeField] float damageColission = 15;
    private void Start() {
        _rb= GetComponent<Rigidbody2D>();
        timer = changeTime;
    }
    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
    }
    private void FixedUpdate() {
        MovementEnemie();
    }
    void MovementEnemie() {

        Vector2 position = _rb.position;
        if (vertical) {
            position.y = position.y + _speed * direction * Time.fixedDeltaTime;
        } else {
            position.x = position.x + _speed * direction * Time.fixedDeltaTime;
        }
        _rb.MovePosition(position);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null) {
            player.ChangeHealth((int)damageColission);
        }
    }
}
