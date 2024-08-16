using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject visualBrick;
    [SerializeField] private BoxCollider boxCollider;

    public GameObject VisualBrick => visualBrick;
    public void OnHideVisual(bool isShow) => visualBrick.SetActive(!isShow);
    public void OnHideCollision(bool isShow) => boxCollider.enabled = !isShow;
}

