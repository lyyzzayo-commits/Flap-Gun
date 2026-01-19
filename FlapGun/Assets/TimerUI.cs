using UnityEngine;
using UnityEngine.UI;

public sealed class TimerUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private void OnEnable()
    {
        GameSignals.TimeUpdated += OnTimeUpdated;
    }

    private void OnDisable()
    {
        GameSignals.TimeUpdated -= OnTimeUpdated;
    }

    private void OnTimeUpdated(float remaining, float max)
    {
        if (max <= 0f) return;

        fillImage.fillAmount = remaining / max;
    }
}
