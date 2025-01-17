using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandlerHealth : MonoBehaviour
{
    public static UIHandlerHealth Instance {  get; private set; }
    VisualElement _healthBar;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        UIDocument _uiDocument = GetComponent<UIDocument>();
        _healthBar = _uiDocument.rootVisualElement.Q<VisualElement>("HealtBar");
        SetHealthValue(1.0f);
    }
    public void SetHealthValue(float percent) {
        _healthBar.style.width = Length.Percent(100 * percent);
    }
}
