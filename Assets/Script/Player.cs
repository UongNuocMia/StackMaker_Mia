using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform playerVisual;
    [SerializeField] private Transform brickPerfab;
    private List<Transform> brickList = new();
    private float heightOfBrick = 0.3f;
    private Vector3 targetPosition;
    private Transform brickOnBridge;
    private bool isOnBridge = false;

    public int BrickListCount => brickList.Count;
    public bool IsOnBridge => isOnBridge;

    private void Start()
    {
        targetPosition = playerVisual.localPosition;
    }

    private void Update()
    {
        RayCastScan();
    }

    public void RayCastScan()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.red);

        float interactRange = 0.5f;
        Vector3 direction = playerMovement.GetDirection();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, interactRange))
        {
            Debug.Log("hit info name____" + hit.transform.gameObject.name);

            if (hit.transform.TryGetComponent(out Brick brick))
            {

                CheckBrick(brick);
            }
            if (hit.transform.TryGetComponent(out Bridge bridge) && BrickListCount > 0)
            {
                CheckBridge(bridge);
            }
            if (hit.transform.TryGetComponent(out EndLevel endLevel))
            {
                OnFinishGame();
            }
        }
    }
    private void AddBrick()
    {
        Transform brickClone = Instantiate(brickPerfab, transform);
        brickList.Add(brickClone);
        brickClone.localPosition = Vector3.zero;
        GameManager.Instance.score++;
    }
    private void RemoveBrick()
    {
        if (brickList.Count == 0)
            return;
        int lastIndex = brickList.Count - 1;
        brickList[lastIndex].GetComponent<Brick>().HideVisual();
        brickList.RemoveAt(lastIndex);
    }

    private void HandlerPlayerHeight()
    {
        if (playerVisual.localPosition.y == targetPosition.y && isOnBridge)
        {
            playerVisual.localPosition = new Vector3(playerVisual.localPosition.x,
                                                     playerVisual.localPosition.y + heightOfBrick,
                                                     playerVisual.localPosition.z);
        }

        Vector3 newPlayerHeight = Vector3.zero;
        newPlayerHeight.y = Mathf.Abs(brickList.Count * heightOfBrick) + targetPosition.y;
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
        brick.HideVisual();
        brick.CollisionOff();
        AddBrick();
        HandlerPlayerHeight();
        HandlerBrickHeight();
    }
    private void CheckBridge(Bridge bridge)
    {
        if (!bridge.IsBrickEnable())
            RemoveBrick();

        bridge.ShowBrickOnBridge();
        isOnBridge = true;
        HandlerPlayerHeight();
    }

    private void OnFinishGame()
    {
        GameManager.Instance.OnFinishGame();
    }
}
