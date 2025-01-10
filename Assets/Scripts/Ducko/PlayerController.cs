using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction _leftAction;
    public InputAction _moveAction;
    public float _velocity;
    private void OnEnable() {
        _leftAction.Enable();
        _moveAction.Enable();
    }
    private void Update() {
        Vector2 _dir = _moveAction.ReadValue<Vector2>();
        Vector2 position = (Vector2)transform.position +
            _dir * _velocity * Time.deltaTime;
        transform.position = position;
    }
    private void OnDisable() {
        _leftAction.Disable();
        _moveAction.Disable();
    }
}
