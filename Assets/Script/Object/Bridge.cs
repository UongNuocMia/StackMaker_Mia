using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Brick brickOnBridge;
    private void Start()
    {
        brickOnBridge.HideVisual();
        brickOnBridge.CollisionOff();
    }

    public void ShowBrickOnBridge()
    {
        brickOnBridge.ShowVisual();
    }
    public bool IsBrickEnable()
    {
        return brickOnBridge.VisualBrick.activeInHierarchy;
    }
}
