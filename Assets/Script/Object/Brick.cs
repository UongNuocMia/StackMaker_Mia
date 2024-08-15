using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private GameObject visualBrick;
    [SerializeField] private BoxCollider boxCollider;

    public GameObject VisualBrick => visualBrick;

    public void ShowVisual()
    {
        visualBrick.SetActive(true);
    }
    public void HideVisual()
    {
        visualBrick.SetActive(false);
    }

    public void CollisionOff()
    {
        boxCollider.enabled = false;
    }

    public void CollisionOn()
    {
        boxCollider.enabled = true;
    }

}

