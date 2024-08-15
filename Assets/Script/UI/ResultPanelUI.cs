using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanelUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Button nextLevel;
    [SerializeField] private Button tryAgainLevel;


    private void OnEnable()
    {
        ShowScore();
        nextLevel.onClick.AddListener(NextLevelOnClick);
        tryAgainLevel.onClick.AddListener(UIManager.Instance.TryAgain);
    }
    private void OnDisable()
    {
        nextLevel.onClick.RemoveListener(NextLevelOnClick);
        tryAgainLevel.onClick.RemoveListener(UIManager.Instance.TryAgain);
    }

    private void NextLevelOnClick()
    {
        GameManager.Instance.currentLevel++;
        GameManager.Instance.OnStartGame();
    }

    private void ShowScore()
    {
        scoreText.text = GameManager.Instance.score.ToString();
    }
}
