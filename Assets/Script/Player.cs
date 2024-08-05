using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    None,
    Forward,
    Down,
    Left,
    Right,
}


public class Player : MonoBehaviour
{
    private Vector2 onClickPosition;
    private Vector2 onReleaseClickPosition;
    private Vector3 dirFromSwipe;
    private Direction direction;

    private float sensitivityThreshold = 50f; // Giá trị ngưỡng nhạy mặc định

    private void Start()
    {
        direction = Direction.None;
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onClickPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            onReleaseClickPosition = Input.mousePosition;
            PlayerHandlerMoveMent();
        }

        Move();
    }

    private void PlayerHandlerMoveMent()
    {

        dirFromSwipe = onReleaseClickPosition - onClickPosition;

        if (dirFromSwipe.magnitude < sensitivityThreshold)
            return;

        // Tính góc của vector dịch chuyển so với trục x
        float angle = Mathf.Atan2(dirFromSwipe.y, dirFromSwipe.x) * Mathf.Rad2Deg;

        // Thiết lập ngưỡng góc (ví dụ: 45 độ)
        float angleThreshold = 45f;


        if (angle > -angleThreshold && angle < angleThreshold)
        {
            // Vuốt chủ yếu theo hướng phải
            Debug.Log("Vuốt phải");
            direction = Direction.Right;
        }
        else if (angle > angleThreshold && angle < 135)
        {
            // Vuốt chủ yếu theo hướng lên
            Debug.Log("Vuốt lên");
            direction = Direction.Forward;
        }
        else if (angle > 135 || angle < -135)
        {
            // Vuốt chủ yếu theo hướng trái
            Debug.Log("Vuốt trái");
            direction = Direction.Left;

        }
        else if (angle > -135 && angle < -45)
        {
            // Vuốt chủ yếu theo hướng xuống
            Debug.Log("Vuốt xuống");
            direction = Direction.Down;

        }
    }
    private void Move()
    {
        Vector3 newPosition = transform.position;
        switch (direction)
        {
            case Direction.None:
                return;
            case Direction.Forward:
                newPosition.x += 1;
                break;
            case Direction.Down:
                newPosition.x += -1;
                break;
            case Direction.Left:
                newPosition.z += 1;
                break;
            case Direction.Right:
                newPosition.z += -1;
                break;
            default:
                break;
        }
        transform.position = newPosition;
        direction = Direction.None;

    }
}
