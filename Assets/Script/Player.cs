using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string ADD_BRICK_ANIM = "IsAddBrick";
    private string  WIN_ANIM= "IsWin";
    private string  IDLE_ANIM= "Idle";
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Transform brickPerfab;
    [SerializeField] private Transform checkBridge;
    [SerializeField] private Animator anim;
    private bool isOnBridge = false;
    private float heightOfBrick = 0.3f;
    private string currentAnimName;
    private List<Transform> brickList = new();
    private Vector3 playerVisualHeight;

    public int BrickListCount => brickList.Count;
    public bool IsOnBridge => isOnBridge;
    public Transform PlayerVisual => playerVisual;
    public Bridge checkThisBridge;
    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        ChangeAnim(IDLE_ANIM);
        playerVisualHeight = playerVisual.localPosition;
    }

    private void Update()
    {
        RayCastScan();
        HandlerPlayerHeight();
    }

    public void RayCastScan()
    {
        Debug.DrawRay(transform.position, Vector3.up * 0.5f, Color.red);

        float interactRange = 0.5f;
        Vector3 direction = playerMovement.GetDirection();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, interactRange))
        {
            if (hit.transform.TryGetComponent(out Brick brick))
            {
                CheckBrick(brick);
            }
            if (hit.transform.TryGetComponent(out Bridge bridge))
            {
                CheckBridge(bridge);
            }
            if (hit.transform.TryGetComponent(out EndLevel endLevel))
            {
                ReachEndPoint();
            }
        }
        if (Physics.Raycast(checkBridge.position, Vector3.down, out hit, 5))
        {
            if (hit.transform.TryGetComponent(out Bridge bridge))
                isOnBridge = true;
            else
                isOnBridge = false;
        }
    }
    private void AddBrick()
    {
        Transform brickClone = Instantiate(brickPerfab, transform);
        brickList.Add(brickClone);
        brickClone.localPosition = Vector3.zero;
        GameManager.Instance.score++;
        HandlerBrickHeight();
        ChangeAnim(ADD_BRICK_ANIM);
    }
    private void RemoveBrick()
    {
        if (brickList.Count == 0)
            return;
        int lastIndex = brickList.Count - 1;
        brickList[lastIndex].GetComponent<Brick>().OnHideVisual(true);
        brickList.RemoveAt(lastIndex);
    }

    private void HandlerPlayerHeight()
    {
        if (isOnBridge && brickList.Count == 0)
        {
            playerVisual.localPosition = new Vector3(playerVisual.localPosition.x,
                                                     playerVisualHeight.y + heightOfBrick,
                                                     playerVisual.localPosition.z);
            return;
        }
        Vector3 newPlayerHeight = Vector3.zero;
        newPlayerHeight.y = Mathf.Abs(brickList.Count * heightOfBrick) + playerVisualHeight.y;
        playerVisual.localPosition = newPlayerHeight;
    }
    private void HandlerBrickHeight()
    {
        if (brickList.Count == 0) return;
        Vector3 newHeightY = new();
        for (int i = 0; i < brickList.Count; i++)
        {
            newHeightY.y = Mathf.Abs(i * heightOfBrick);
            brickList[i].localPosition = new Vector3(newHeightY.x, newHeightY.y);
        }
    }
  
    public void CheckBrick(Brick brick)
    {
        brick.OnHideVisual(true);
        brick.OnHideCollision(true);
        AddBrick();
        HandlerPlayerHeight();
    }
    private void CheckBridge(Bridge bridge)
    {
        checkThisBridge = bridge;
        HandlerPlayerHeight();
        if (brickList.Count != 0)
        {
            if (!bridge.IsBrickEnable())
            {
                RemoveBrick();
            }
            bridge.ShowBrickOnBridge();
        }

    }

    private void ReachEndPoint()
    {
        StartCoroutine(OnWinningCoroutine());
    }


    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    private IEnumerator OnWinningCoroutine()
    {
        ChangeAnim(WIN_ANIM);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.OnFinishGame();

    }

}
