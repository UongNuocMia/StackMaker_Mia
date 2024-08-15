using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]private List<GameObject> levelList;
    private GameObject currentMap;
    private Transform currentStartPoint;
    private Transform currentEndPoint;

    public bool isEndGame => GameManager.Instance.currentLevel >= levelList.Count;

    public void OnLoadMap(int level)
    {
        if (level <= levelList.Count)
        {
            currentMap = Instantiate(levelList[level]);
            currentMap.transform.position = Vector3.zero;
            currentStartPoint = currentMap.GetComponent<Level>().StartPoint;
            currentEndPoint = currentMap.GetComponent<Level>().EndPoint;
        }
    }
    public void DestroyMap()
    {
        if (currentMap == null)
            return;
        Destroy(currentMap);
    }

    public Vector3 GetCurrentStartPoint()
    {
        return currentStartPoint.localPosition;
    }
    public Vector3 GetCurrentEndPoint()
    {
        return currentEndPoint.localPosition;
    }


}
