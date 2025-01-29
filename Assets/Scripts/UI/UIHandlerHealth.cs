using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandlerHealth : MonoBehaviour
{
    public static UIHandlerHealth Instance {  get; private set; }
    VisualElement _healthBar;
    VisualElement m_NonPlayerDialogue;
    [SerializeField] float m_TimerDisplay;
    public float displayTime = 4.0f;

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        UIDocument _uiDocument = GetComponent<UIDocument>();
        _healthBar = _uiDocument.rootVisualElement.Q<VisualElement>("HealtBar");
        m_NonPlayerDialogue = _uiDocument.rootVisualElement.Q <VisualElement> ("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f; 
        SetHealthValue(1.0f);
    }
    private void Update() {
        if (m_TimerDisplay > 0) {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0) {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }

    }
    public void SetHealthValue(float percent) {
        _healthBar.style.width = Length.Percent(100 * percent);
    }
    public void DisplayDialogue() {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }
}
