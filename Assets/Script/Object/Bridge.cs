using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Brick brickOnBridge;
    private void Start()
    {
        brickOnBridge.OnHideVisual(true);
        brickOnBridge.OnHideCollision(true);
    }
    public void ShowBrickOnBridge()
    {
        brickOnBridge.OnHideVisual(false);
    }
    public bool IsBrickEnable()
    {
        return brickOnBridge.VisualBrick.activeInHierarchy;
    }
}
