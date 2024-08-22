using UnityEngine;
using UnityEngine.UI;

public class InGamePanelUI : MonoBehaviour
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Text currentLevelText;
    private int currentlLevel;

    private void Start()
    {
    }
    private void OnEnable()
    {
        currentlLevel = GameManager.Instance.currentLevel;
        currentlLevel++;
        settingButton.onClick.AddListener(UIManager.Instance.OnSetting);
        currentLevelText.text = "Level " + currentlLevel;
    }
    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(UIManager.Instance.OnSetting);
    }
}
