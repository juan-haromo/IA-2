using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static Clock Instance;
    [SerializeField] float timeMultiplier = 10;
    public float time;
    [SerializeField] TextMeshProUGUI timeDisplay;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        time += Time.deltaTime * timeMultiplier;
        time %= 86400;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        timeDisplay.text = timeSpan.ToString(@"hh\:mm");
    }
}
