using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainPanelUI mainPanelUI;
    [SerializeField] private ResultPanelUI resultPanelUI;
    [SerializeField] private SettingPanelUI settingPanelUI;
    [SerializeField] private InGamePanelUI inGamePanelUI;
    [SerializeField] private GameObject EndGamePanel;

    private void Start()
    {
        mainPanelUI.gameObject.SetActive (true);
        resultPanelUI.gameObject.SetActive(false);
        settingPanelUI.gameObject.SetActive(false);
        inGamePanelUI.gameObject.SetActive(false);
        EndGamePanel.SetActive(false);
    }
    public void OnStartGame()
    {
        inGamePanelUI.gameObject.SetActive(true);
        mainPanelUI.gameObject.SetActive(false);
        resultPanelUI.gameObject.SetActive(false);
    }
    public void OnFinishGame()
    {
        resultPanelUI.gameObject.SetActive(true);
        inGamePanelUI.gameObject.SetActive(false);
    }
    public void TryAgain()
    {
        inGamePanelUI.gameObject.SetActive(true);
        resultPanelUI.gameObject.SetActive(false);
        settingPanelUI.gameObject.SetActive(false);
        GameManager.Instance.OnStartGame();
    }

    public void BackToGame()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        inGamePanelUI.gameObject.SetActive(true);
        settingPanelUI.gameObject.SetActive(false);
    }
    public void OnSetting()
    {
        GameManager.Instance.ChangeState(GameState.Setting);
        settingPanelUI.gameObject.SetActive(true);
        inGamePanelUI.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        EndGamePanel.SetActive(true);
    }

    //public void OpenUI<T>() where T : MonoBehaviour
    //{
    //    GameObject uiPrefab = Resources.Load<GameObject>("UI/" + typeof(T).Name);
    //    if (uiPrefab != null)
    //    {
    //        Instantiate(uiPrefab);
    //    }
    //    else
    //    {
    //        Debug.LogError("UI prefab not found: " + typeof(T).Name);
    //    }
    //}
}
