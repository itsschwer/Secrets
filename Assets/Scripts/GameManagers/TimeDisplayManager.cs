﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeDisplayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay = default;
    [SerializeField] private Image timeImage = default;
    [SerializeField] private Sprite dayTimeSprite = default;
    [SerializeField] private Sprite nightTimeSprite = default;

    private void OnEnable() => TimeManager.OnTimeChanged += UpdateUI;
    private void OnDisable() => TimeManager.OnTimeChanged -= UpdateUI;

    private void UpdateUI(float normalizedTimeOfDay)
    {
        var hour = Mathf.FloorToInt(24 * normalizedTimeOfDay);
        var minute = Mathf.FloorToInt((24 * 60 * normalizedTimeOfDay) % 60);
        timeDisplay.text = hour.ToString("00") + ":" + minute.ToString("00");

        UpdateSprite(normalizedTimeOfDay);
    }

    private void UpdateSprite(float normalizedTimeOfDay)
    {
        // 0.23f ≈ 05:30; 0.73f ≈ 17:30; ∴ Night: 17:30 — 05:30
        if (normalizedTimeOfDay <= 0.23f || normalizedTimeOfDay >= 0.73f)
        {
            timeImage.sprite = nightTimeSprite;
        }
        else
        {
            timeImage.sprite = dayTimeSprite;
        }
    }
}
