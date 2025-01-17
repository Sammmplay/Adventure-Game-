using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InputAction _moveAction;
    public Slider _slidelife;
    public float _velocity;

    Rigidbody2D _rb;

    [SerializeField] Vector2 _dir;

    // Variables related to the health system
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }

    [Header("Invincibility")]
    public float timeInvincible = 2.0f;
    bool isInvincible = false;
    float damageCooldown;
    
    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
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
        }


        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHandlerHealth.Instance.SetHealthValue(currentHealth/(float)maxHealth);
    }
    void Movement() {
        _dir = _moveAction.ReadValue<Vector2>();
        Vector2 position = (Vector2)_rb.position +
            _dir * _velocity * Time.fixedDeltaTime;
        _rb.MovePosition(position);
    }
    private void OnDisable() {
        _moveAction.Disable();
    }
}
