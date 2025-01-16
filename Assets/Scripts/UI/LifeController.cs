using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    Slider _lifeSlider;


    public float _maxLife;
    public float _lifeCurrent;
    private void Start() {
        _lifeSlider = GetComponentInChildren<Slider>();
        
        InitialiteLife(_maxLife,_lifeCurrent);
    }
    public void AddLife(float value) {
        if(value +_lifeCurrent > _maxLife) {
            _lifeCurrent = _maxLife;
            
        } else {
            _lifeCurrent += value;
            
        }
        _lifeSlider.value = _lifeCurrent;
    }
    public void RestLife(float value) {
        _lifeCurrent -= value;
        _lifeSlider.value = _lifeCurrent;
        if (_lifeCurrent <= 0) { 
            Debug.Log("Lose Game");
        }
    }
    public void InitialiteLife(float maxLife, float life) {
        _lifeSlider.maxValue = maxLife;
        _lifeSlider.value = life;
        _lifeCurrent = Mathf.Clamp(_lifeCurrent, 0, _maxLife);
    }
}
