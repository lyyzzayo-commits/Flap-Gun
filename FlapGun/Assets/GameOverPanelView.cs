using UnityEngine;

public class GameOverPanelView : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Awake()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void OnEnable()
    {
        GameSignals.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameSignals.GameOver -= OnGameOver;
    }

    private void OnGameOver(GameOverReason reason)
    {
        if (panel != null) panel.SetActive(true);
        // reason 텍스트 표시도 여기서 처리
    }
}
