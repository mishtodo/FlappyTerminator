using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : SpawnableObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private GameObject _objectPool;

    private ObjectPool<T> _pool;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxCapacity = 20;

    private readonly List<T> _activeObjects = new List<T>();

    //public event Action<Vector3> ObjectReleased;

    public void Initialize(T prefab, GameObject container)
    {
        _prefab = prefab;
        _objectPool = container;

        _pool = new ObjectPool<T>(
            createFunc: () => CreateFunc(),
            actionOnGet: (T) => ActionOnGet(T),
            actionOnRelease: (T) => ActionOnRelease(T),
            actionOnDestroy: (T) => Destroy(T),
            collectionCheck: true,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxCapacity
        );
    }

    public T Spawn(Vector3 spawnPosition, Quaternion rotation)
    {
        T obj = _pool.Get();
        obj.transform.position = spawnPosition;
        obj.transform.rotation = rotation;
        return obj;
    }

    public void ClearActiveObjects()
    {
        for (int i = _activeObjects.Count - 1; i >= 0; i--)
        {
            if (_activeObjects[i] != null)
            {
                _activeObjects[i].NotifyDying();
            }
        }

        _activeObjects.Clear();
    }

    private T CreateFunc()
    {
        T obj = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
        return obj;
    }

    private void ActionOnGet(T obj)
    {
        obj.OnDying += HandleObjectDestroyed;
        obj.gameObject.SetActive(true);
        _activeObjects.Add(obj);
    }

    private void ActionOnRelease(T obj)
    {
        obj.OnDying -= HandleObjectDestroyed;
        obj.InitializePosition(_objectPool.transform.position);
        obj.InitializeRotation(_objectPool.transform.rotation);
        obj.gameObject.SetActive(false);
        _activeObjects.Remove(obj);
    }

    private void HandleObjectDestroyed(SpawnableObject obj)
    {
        //ObjectReleased?.Invoke(obj.transform.position);
        _pool.Release((T)obj);
    }
}