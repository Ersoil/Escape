using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    public List<string> Messages;
    public bool isAutoUse = false;
    public bool isCustomTextMessage = false;
    public bool isJustOne = false;
    [SerializeField] GameObject textMessageObject;
    [SerializeField] private int startTime = 5;
    [SerializeField] private int endTime = 10;
    private float currentTime = 0;
    private GameObject hud;
    private int counts = 0;

    public void DisableHud()
    {
        GameObject.FindWithTag("HUD").SetActive(false);
    }
    public void EnableHud()
    {
        GameObject.FindWithTag("HUD").SetActive(true);
    }
    public void DestroyHUD()
    {
        GameObject.FindWithTag("HUD").GetComponent<GloabalData>().DestroyHud();
    }
    private void Awake()
    {
        if (!isCustomTextMessage) hud = GameObject.Find("HUD");
        else hud = textMessageObject;
        currentTime = Random.Range(startTime, endTime);
        if (hud == null)
        {
            Debug.Log("Hud not found!!!");
        }
    }
    public void SendMassage(string Message)
    {
        hud.GetComponent<TextMassage>().newMessage(Message);
        hud.GetComponent<TextMassage>().StartTyping();
    }

    public void SetCameraMessage(string Message)
    {
        hud.transform.Find("Level").GetComponent<TextMassage>().newMessage(Message);
        hud.transform.Find("Level").GetComponent<TextMassage>().StartTyping();
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        Debug.Log($"Messages{currentTime}");
        if(currentTime<=0 && Messages.Count!=0 && isAutoUse && counts==0)
        {
            Debug.Log(isAutoUse);
            if (isJustOne) counts++; 
            currentTime = Random.Range(startTime, endTime);
            if(!hud.gameObject.GetComponent<TextMassage>().isTyping) SendMassage(Messages[Random.Range(0, Messages.Count)]);
        }
    }
}
