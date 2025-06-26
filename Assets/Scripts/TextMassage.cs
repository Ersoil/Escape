using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMassage : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private string newText;
    private string fullText;

    private void Start()
    {
        fullText = textComponent.text;
        textComponent.text = "";
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Для запуска эффекта вручную
    public void StartTyping()
    {
        fullText = newText;
        textComponent.text = "";
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }

    private void Awake()
    {
        StartTyping();
    }
}
