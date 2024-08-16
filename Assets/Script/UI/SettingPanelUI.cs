using UnityEngine;
using UnityEngine.UI;

public class SettingPanelUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button tryAgainButton;

    private void OnEnable()
    {
        backButton.onClick.AddListener(UIManager.Instance.BackToGame);
        tryAgainButton.onClick.AddListener(UIManager.Instance.TryAgain);
    }
    private void OnDisable()
    {
        backButton.onClick.RemoveListener(UIManager.Instance.BackToGame);
        tryAgainButton.onClick.RemoveListener(UIManager.Instance.TryAgain);
    }
}
