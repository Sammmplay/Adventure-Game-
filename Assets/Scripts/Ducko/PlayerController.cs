using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InputAction _moveAction;

    public float _velocity;

    Rigidbody2D _rb;

    [SerializeField] Vector2 _dir;

    // Variables related to the health system
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }
    [Header("Disparo")]
    public GameObject _prefabBullet;
    [Header("Invincibility")]
    public float timeInvincible = 2.0f;
    bool isInvincible = false;
    float damageCooldown;
    [Header("Animacion")]
    Animator _anim;
    [SerializeField] Vector2 _moveDirection = new Vector2(1, 0);
    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    private void OnEnable() {
        _moveAction.Enable();
    }
    private void Update() {
        if (isInvincible) {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0) {
                isInvincible = false;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.C)) {
            Lauch();
        }
    }
    private void FixedUpdate() {
        Movement();
    }
    public void ChangeHealth(int amount) {
        if (amount < 0) {
            if (isInvincible) {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
            _anim.SetTrigger("Hit");
        }


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHandlerHealth.Instance.SetHealthValue(currentHealth/(float)maxHealth);
    }
    void Movement() {
        _dir = _moveAction.ReadValue<Vector2>();
        if (!Mathf.Approximately(_dir.x, 0.0f) || !Mathf.Approximately(_dir.y, 0.0f)) {
            _moveDirection.Set(_dir.x, _dir.y);
            _moveDirection.Normalize();
        }
        _anim.SetFloat("LookX", _moveDirection.x);
        _anim.SetFloat("LookY", _moveDirection.y);
        _anim.SetFloat("Speed", _dir.magnitude);
        Vector2 position = (Vector2)_rb.position +
            _dir * _velocity * Time.fixedDeltaTime;
        _rb.MovePosition(position);
    }
    void Lauch() {
        GameObject _bullet = Instantiate(_prefabBullet, _rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Proyectiles _scrip = _bullet.GetComponent<Proyectiles>();
        _scrip.Lauch(_dir, 300);
        _anim.SetTrigger("Shoot");
    }
    private void OnDisable() {
        _moveAction.Disable();
    }
}
