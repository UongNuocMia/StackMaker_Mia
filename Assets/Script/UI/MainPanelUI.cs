using UnityEngine;
using UnityEngine.UI;

public class MainPanelUI : MonoBehaviour
{
    [SerializeField] private Button startGame;

    private void Start()
    {
        startGame.onClick.AddListener(GameManager.Instance.OnStartGame);
    }
    private void OnDisable()
    {
        startGame.onClick.RemoveListener(GameManager.Instance.OnStartGame);   
    }
}
