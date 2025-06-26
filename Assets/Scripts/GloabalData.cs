using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GloabalData : MonoBehaviour
{
    [SerializeField] private float _maxTime = 2000f; 
    [SerializeField] private float _currentTime = 0f;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _end;
    [SerializeField] private Slider _energySlider;

    [Header("Settings")]
    [SerializeField] private float _energyDrainSpeed = 1f;
    [SerializeField] private bool _countDown = true;

    private static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
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

    private void UpdateTimeDisplay()
    {
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);
        int milliseconds = Mathf.FloorToInt((_currentTime * 1000f) % 1000f);

        _timeText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }

    private void UpdateEnergy()
    {
        float energyPercentage = 1 - (_currentTime / _maxTime);
        _energySlider.value = Mathf.Clamp(energyPercentage, 0f, 1f);
    }
    private void ShowEnd()
    {
        _end.enabled = true;
    }
    private void TimeExpired()
    {
        Debug.Log("Время вышло!");
       
    }
}
