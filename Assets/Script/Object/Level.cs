using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    public Transform StartPoint => startPoint;
    public Transform EndPoint => endPoint;
    

}
