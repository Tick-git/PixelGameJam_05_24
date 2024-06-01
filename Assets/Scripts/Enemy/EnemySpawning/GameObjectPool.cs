using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectPool
{
    public int CountInactive => _inactiveGameObjects.Count;

    Vector3 _inactivePosition;

    protected Stack<GameObject> _inactiveGameObjects;
    protected int _preInstantiationCount;


    public GameObjectPool(int preInstantiationCount)
    {
        _preInstantiationCount = preInstantiationCount;
        _inactiveGameObjects = new Stack<GameObject>(preInstantiationCount);
        _inactivePosition = new Vector3(-100, -100, 0);
    }

    protected abstract GameObject CreateObject();
   

    public void InstantiateGameObjects()
    {
        for (int i = 0; i < _preInstantiationCount; i++)
        {
            Release(CreateObject());
        }
    }

    public GameObject Get()
    {
        GameObject gameObject;

        if (_inactiveGameObjects.Count == 0)
        {
            _inactiveGameObjects.Push(CreateObject());
        }

        gameObject = _inactiveGameObjects.Pop();
        gameObject.SetActive(true);

        return gameObject;
    }

    public void Release(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = _inactivePosition;
        _inactiveGameObjects.Push(gameObject);
    }

    public void Clear()
    {
        foreach (var enemy in _inactiveGameObjects)
        {
            Object.Destroy(enemy);
        }

        _inactiveGameObjects.Clear();
    }
}


