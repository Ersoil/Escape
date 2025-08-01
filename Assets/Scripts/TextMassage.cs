using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

public class TextMassage : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float clearingSpeed = 0.03f;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string newText;
    [SerializeField] private float clearTime = 5f;
    [SerializeField] bool canBrokeTyping = false;
    InputSystem_Actions Actions;
    bool isBrokeTyping;
    public bool isTyping;
    private string fullText;
    public UnityEvent onEndTyping;
    public UnityEvent onEndClear;
    private void Start()
    {
        Actions = new InputSystem_Actions();
        if (canBrokeTyping) Actions.Enable();
        Actions.PlayerControl.Jump.performed += context => brokeTyping();
        Actions.PlayerControl.Jump.canceled += context => exitBrokeTyping();
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        for (int i = 0; i<fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            if (isBrokeTyping)
            {
                Debug.Log("break");
                textComponent.text = fullText;
                break;
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        onEndTyping?.Invoke();
        isTyping = false;
    }
    private IEnumerator ClearText()
    {
        isTyping = true;
        for (int i  = textComponent.text.Length;i>0;i--)
        {
            textComponent.text = textComponent.text.Remove(textComponent.text.Length-1);
            if (isBrokeTyping)
            {
                textComponent.text = "";
                break;
            }
            yield return new WaitForSeconds(clearingSpeed);
        }
        onEndClear?.Invoke();
        isTyping = false;
    }
    public IEnumerator clearTimer()
    {
        float TimerTemp = clearTime;
        while (clearTime > 0)
        {
            yield return new WaitForSeconds(1f);
            clearTime-=1f;
        }
        clearTime = TimerTemp;
        StartCoroutine(ClearText());
    }

    public void startClearTimer()
    {
        StartCoroutine(clearTimer());
    }

    private void brokeTyping()
    {
        isBrokeTyping = true;
    }

    private void exitBrokeTyping()
    {
        isBrokeTyping = false;
    }

    public void newMessage(string message)
    {
        newText = message;
    }

    public void StartTyping()
    {
        fullText = newText;
        textComponent.text = "";
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }

    private void Awake()
    {
        if(newText.Length!=0)
        StartTyping();
    }
}
