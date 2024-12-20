using UnityEngine;
using UnityEngine.Pool;

public class PoolManager
{
    private readonly ObjectPool<GameObject> objectPool;
    private readonly GameObject prefab;
    private readonly Transform parent;

    public PoolManager(GameObject prefab, Transform parent = null, int defaultCapacity = 10, int maxSize = 50)
    {
        this.prefab = prefab;
        this.parent = parent;

        objectPool = new ObjectPool<GameObject>(
            createFunc: CreateObject,
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject,
            collectionCheck: false,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public GameObject Get()
    {
        return objectPool.Get();
    }

    public void Return(GameObject obj)
    {
        objectPool.Release(obj);
    }

    public void Clear()
    {
        objectPool.Clear();
    }

    private GameObject CreateObject()
    {
        GameObject obj = Object.Instantiate(prefab, parent);
        obj.SetActive(false);
        return obj;
    }

    private void OnGetObject(GameObject obj)
    {
        obj.SetActive(true);
        IPoolableObject poolable = obj.GetComponent<IPoolableObject>();
        poolable?.OnSpawn();
    }

    private void OnReleaseObject(GameObject obj)
    {
        IPoolableObject poolable = obj.GetComponent<IPoolableObject>();
        poolable?.OnDespawn();
        obj.SetActive(false);
    }

    private void OnDestroyObject(GameObject obj)
    {
        Object.Destroy(obj);
    }
}