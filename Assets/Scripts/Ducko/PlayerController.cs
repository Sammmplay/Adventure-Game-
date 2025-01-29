using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InputAction _moveAction;
    public InputAction _shoot;
    public InputAction _interact;
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
    [Header("Sounds")]
    [SerializeField] AudioClip[] _audioClips;
    [SerializeField] AudioSource _audiosource;
     
    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _audiosource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }
    private void OnEnable() {
        _moveAction.Enable();
        _interact.Enable();
        _shoot.Enable();
    }
    private void Update() {
        if (isInvincible) {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0) {
                isInvincible = false;
            }
        }
        if (_interact.WasPressedThisFrame()) {
           
            FindFriend();
        }

        if (_shoot.WasPressedThisFrame()) {
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
            PlaySound(_audioClips[2]);
            _anim.SetTrigger("Hit");
        } else {
            PlaySound(_audioClips[0]);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        UIHandlerHealth.Instance.SetHealthValue(currentHealth/(float)maxHealth);
        
        Debug.Log("tienes "+ currentHealth+ " de vida");   
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
        _anim.SetTrigger("Shoot");
        GameObject _bullet = Instantiate(_prefabBullet, _rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Proyectiles _scrip = _bullet.GetComponent<Proyectiles>();
        _scrip.Lauch(_moveDirection, 300);
        PlaySound(_audioClips[1]);
        
        
    }
    void FindFriend() {
        Debug.Log("Lanzando un rayo");
        RaycastHit2D hit = Physics2D.Raycast(_rb.position, _moveDirection, 1.5f, LayerMask.GetMask("NPC"));
        if(hit.collider != null ) {
            Debug.Log("detecto un collider");
            if (hit.collider.CompareTag("NPC")) {
                UIHandlerHealth.Instance.DisplayDialogue();
            }
        }
    }
    public void PlaySound(AudioClip clip) {
        _audiosource.PlayOneShot(clip);
    }
    private void OnDisable() {
        _moveAction.Disable();
        _interact.Disable();
        _shoot.Disable();
    }
}
