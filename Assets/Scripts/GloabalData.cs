using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GloabalData : MonoBehaviour
{
    [SerializeField] private float _maxTime = 2000f; 
    [SerializeField] private float _currentTime = 0f;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject _end;
    [SerializeField] private GameObject _pause;
    [SerializeField] private Slider _energySlider;
    public static Action onEnergyEnd;
    [Header("Settings")]
    [SerializeField] private float _energyDrainSpeed = 1f;
    [SerializeField] private bool _countDown = true;
    public static Action onExitPressed;
    public  UnityEvent onAdShow;
    private static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            SceneLoaderManager.onAdShow += ShowAd;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void ActivatePause()
    {
        _pause.SetActive(true);
    }
    public void ExitPressed()
    {
        onExitPressed?.Invoke();
    }
    public void DestroyHud()
    {
        Destroy(this.gameObject);
    }
    private void Update()
    {
        
        _currentTime += Time.deltaTime;

        
        UpdateTimeDisplay();

        
        UpdateEnergy();

        
        if (_countDown && _currentTime >= _maxTime)
        {
            TimeExpired();
        }
    }
    private void OnDestroy()
    {
        SceneLoaderManager.onAdShow -= ShowAd;
    }
    public void ShowAd()
    {
        _pause.SetActive(true);
        onAdShow?.Invoke();
    }
    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((_currentTime * 1000f) % 1000f);

        _timeText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    private void UpdateEnergy()
    {
        float energyPercentage = 1 - (_currentTime*_energyDrainSpeed/ _maxTime);
        _energySlider.value = Mathf.Clamp(energyPercentage, 0f, 1f);
    }
    private void ShowEnd()
    {
        _end.SetActive(true);
        onEnergyEnd?.Invoke();
    }
    private void TimeExpired()
    {
        ShowEnd();
        Debug.Log("Время вышло!");
    }
}
