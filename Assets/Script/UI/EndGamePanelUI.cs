using UnityEngine;
using UnityEngine.UI;

public class EndGamePanelUI : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;

    private void OnEnable()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainClick);
    }
    private void OnDisable()
    {
        playAgainButton.onClick.RemoveListener(OnPlayAgainClick);
    }

    private void OnPlayAgainClick()
    {
        GameManager.Instance.OnPlayAgain();
        this.gameObject.SetActive(false);
    }
}
