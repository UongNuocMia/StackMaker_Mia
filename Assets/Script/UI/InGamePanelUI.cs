using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanelUI : MonoBehaviour
{
    [SerializeField] private Button settingButton;

    private void OnEnable()
    {
        settingButton.onClick.AddListener(UIManager.Instance.OnSetting);
    }
    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(UIManager.Instance.OnSetting);
    }
}
