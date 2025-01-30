using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemiesController : MonoBehaviour
{
    public float _speed;
    public bool vertical;
    public float changeTime = 3.0f;

    public ParticleSystem smokeEffect;
    public ParticleSystem instantiateParticle;

    [SerializeField] float timer;
    int direction = 1;
    Rigidbody2D _rb;
    [SerializeField] float damageColission = 15;
    bool broken = true;
    Animator _anim;

    AudioSource _audioSource;
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _rb= GetComponent<Rigidbody2D>();
        timer = changeTime;
        _anim = GetComponent<Animator>();
        EnemiesContainer.Instance.AddEnemies(this);
    }
    private void Update() {
        timer -= Time.deltaTime;
        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
    }
    private void FixedUpdate() {
        if (!broken) return;
        MovementEnemie();
    }
    void MovementEnemie() {
        
        
        Vector2 position = _rb.position;
        if (vertical) {
            _anim.SetFloat("MoveX", 0);
            _anim.SetFloat("MoveY", direction);
            position.y = position.y + _speed * direction * Time.fixedDeltaTime;
        } else {
            position.x = position.x + _speed * direction * Time.fixedDeltaTime;
            _anim.SetFloat("MoveX", direction);
            _anim.SetFloat("MoveY", 0);
        }
        _rb.MovePosition(position);
    }
    public void Fix() {
        broken = false;
        _rb.simulated = false;
        _anim.SetTrigger("Fixed");
        if(instantiateParticle!= null) {
            Instantiate(instantiateParticle.gameObject, transform.position, Quaternion.identity);
        }
        EnemiesContainer.Instance.RemoveEnemies(this);
        smokeEffect.Stop();
        _audioSource.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null) {
            player.ChangeHealth((int)damageColission);
        }
    }
}
