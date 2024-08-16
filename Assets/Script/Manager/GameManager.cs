using UnityEngine;

public enum GameState { MainMenu, GamePlay, Finish, Setting }


public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject playerPrefab;
    private Transform playerTransform;
    private static GameState gameState;
    public int currentLevel;
    public int score;
    public bool isTryAgain;

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;

    private void Awake()
    {
        //tranh viec nguoi choi cham da diem vao man hinh
        Input.multiTouchEnabled = false;
        //target frame rate ve 60 fps
        Application.targetFrameRate = 60;
        //tranh viec tat man hinh
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //xu tai tho
        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    private void Start()
    {
        currentLevel = Data.Instance.GetLevel();
        ChangeState(GameState.MainMenu);
        //UIManager.Ins.OpenUI<UIMainMenu>();
    }
    private void Update()
    {
        if (playerTransform != null && playerTransform.position == LevelManager.Instance.GetCurrentEndPoint())
        {
            OnFinishGame();
        }
    }

    public void OnStartGame()
    {
        if (LevelManager.Instance.isEndGame)
        {
            UIManager.Instance.EndGame();
            return;
        }
        score = 0;
        DestroyCurrentPlayer();
        LevelManager.Instance.DestroyMap();
        LevelManager.Instance.OnLoadMap(currentLevel);
        SetUpPlayer();
        UIManager.Instance.OnStartGame();
        ChangeState(GameState.GamePlay);
    }
    public void OnSetting(bool isSetting)
    {

        if (isSetting)
            ChangeState(GameState.Setting);
        else
            ChangeState(GameState.GamePlay);
    }
    public void OnFinishGame()
    {
        Debug.Log("Let See");
        ChangeState(GameState.Finish);
        UIManager.Instance.OnFinishGame();

    }

    public void OnNextLevel()
    {
        currentLevel++;
        Data.Instance.SetLevel(currentLevel);
        OnStartGame();
    }
    public void OnPlayAgain()
    {
        ChangeState(GameState.MainMenu);
        Data.Instance.SetLevel(0);
        currentLevel = 0;
        OnStartGame();
    }
    private void SetUpPlayer()
    {
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = LevelManager.Instance.GetCurrentStartPoint();
        playerTransform = player.transform;
        CameraFollow.Instance.FindPlayer(player.GetComponent<Player>().PlayerVisual);
    }

    private void DestroyCurrentPlayer()
    {
        if (playerTransform != null)
            Destroy(playerTransform.gameObject);
    }
}
