using System;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectProvider : MonoBehaviour
{
    private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, Stack<GameObject>> _gameObjects = new Dictionary<string, Stack<GameObject>>();

    public static GameObjectProvider Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public GameObject GetOrCreateGameObjectByName(string prefabName)
    {
        if (_gameObjects.ContainsKey(prefabName))
        {
            var stack = _gameObjects[prefabName];
            if (stack.Count > 0)
            {
                var returnGameObject = stack.Pop();
                returnGameObject.SetActive(true);
                return returnGameObject;
            }
        }

        GameObject prefab = null;
        if (_prefabs.ContainsKey(prefabName))
        {
            prefab = _prefabs[prefabName];
        }
        else
        {
            prefab = Resources.Load<GameObject>(prefabName);
            _prefabs.Add(prefabName, prefab);
        }

        var go = Instantiate(prefab);
        return go;
    }

    public void SendBackToPool(string prefabName, GameObject go)
    {
        if (!_gameObjects.ContainsKey(prefabName))
        {
            _gameObjects.Add(prefabName, new Stack<GameObject>());
        }
        
        go.SetActive(false);
        go.transform.SetParent(transform);
        _gameObjects[prefabName].Push(go);
    }
}