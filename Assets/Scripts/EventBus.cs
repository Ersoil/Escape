using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public UnityEvent onPlayerDeath;
    public UnityEvent onPlayerWinLevel;
    public UnityEvent onTimeEnd;
}
