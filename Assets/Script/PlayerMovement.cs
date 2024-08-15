using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    None,
    Forward,
    Down,
    Left,
    Right,
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField] private Player player;
    [SerializeField] private Transform playerVisual;
    private Vector2 onClickPosition;
    private Vector2 onReleaseClickPosition;
    private Vector3 dirFromSwipe;
    private Vector3 targetPosition;
    private Direction directionMove;
    private Direction directionCanMoveOnBridge;
    private float sensitivityThreshold = 50f; // Giá trị ngưỡng nhạy mặc định
    private Rigidbody rb;
    private float timer = 10;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        directionMove = Direction.None;
        targetPosition = transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        Debug.DrawRay(transform.position, GetDirection() * 10, Color.red);

        directionCanMoveOnBridge = CheckDirection(directionMove);
        if (Input.GetMouseButtonDown(0))
        {
            onClickPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && GameManager.IsState(GameState.GamePlay))
        {
            onReleaseClickPosition = Input.mousePosition;
            timer = 10;
            InputHandler();
            Move();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void InputHandler()
    {
        dirFromSwipe = onReleaseClickPosition - onClickPosition;
        if (dirFromSwipe.magnitude < sensitivityThreshold)
            return;

        // Tính góc của vector dịch chuyển so với trục x
        float angle = Mathf.Atan2(dirFromSwipe.y, dirFromSwipe.x) * Mathf.Rad2Deg;

        // Thiết lập ngưỡng góc
        float angleThreshold = 45f;

        if (angle > -angleThreshold && angle < angleThreshold)
        {
            Debug.Log("Vuốt phải");
            directionMove = Direction.Right;
        }
        else if (angle > angleThreshold && angle < 135)
        {
            Debug.Log("Vuốt lên");
            directionMove = Direction.Forward;
        }
        else if (angle > 135 || angle < -135)
        {
            Debug.Log("Vuốt trái");
            directionMove = Direction.Left;

        }
        else if (angle > -135 && angle < -45)
        {
            Debug.Log("Vuốt xuống");
            directionMove = Direction.Down;
        }
    }
    private void Move()
    {
        Vector3 newPosition = transform.position;
        while (!CheckWall() && timer > 0)
        {
            timer -= Time.deltaTime;
           
                switch (directionMove)
                {
                    case Direction.Forward:
                        newPosition.x += 1;
                        break;
                    case Direction.Down:
                        newPosition.x -= 1;
                        break;
                    case Direction.Left:
                        newPosition.z += 1;
                        break;
                    case Direction.Right:
                        newPosition.z -= 1;
                        break;
                    default:
                        return;
                }
            targetPosition = newPosition;
        }

        ChangeRotationPlayerVisual();
    }

    public Vector3 GetDirection()
    {
        switch (directionMove)
        {
            case Direction.Forward:
                return Vector3.right;
            case Direction.Down:
                return Vector3.left;
            case Direction.Left:
                return Vector3.forward;
            case Direction.Right:
                return Vector3.back;
            default:
                return Vector3.zero;
        }
    }

    private void ChangeRotationPlayerVisual()
    {

        Quaternion forwardRot = Quaternion.Euler(0, -60, 0);
        Quaternion backRot = Quaternion.Euler(0, 108, 0);
        Quaternion leftRot = Quaternion.Euler(0, 201, 0);
        Quaternion rightRot = Quaternion.Euler(0, 16, 0);

        switch (directionMove)
        {
            case Direction.None:
                break;
            case Direction.Forward:
                playerVisual.localRotation = forwardRot;
                break;
            case Direction.Down:
                playerVisual.localRotation = backRot;
                break;
            case Direction.Left:
                playerVisual.localRotation = leftRot;
                break;
            case Direction.Right:
                playerVisual.localRotation = rightRot;

                break;
            default:
                break;
        }
    }
    private bool CheckWall()
    {
        float interactRange = 0.6f;
        Vector3 direction = GetDirection();
        RaycastHit hit;
        if (Physics.Raycast(targetPosition, direction, out hit, interactRange))
        {
            if (hit.transform.TryGetComponent(out Wall wall))
            {
                Debug.Log("true");
                return true;// đụng tường
            }
        }
        return false;
    }

    private Direction CheckDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.None:
                break;
            case Direction.Forward:
                return Direction.Down;
            case Direction.Down:
                return Direction.Forward;
            case Direction.Left: 
                return Direction.Right;
            case Direction.Right: 
                return Direction.Left;
            default:
                break;
        }
        return Direction.None;
    }
}
