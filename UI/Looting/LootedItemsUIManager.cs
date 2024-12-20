using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootedItemsUIManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _lootedItemUIPrefab;

    [SerializeField] 
    private float _timeBetweenLootedItemShow = 1f;

    [SerializeField]
    private int _poolDefaultSize;

    [SerializeField]
    private int _poolMaxSize;

    private PoolManager _lootedItemUIPool;

    private Queue<(string text, Sprite icon)> _lootQueue = new Queue<(string, Sprite)>();
    private bool _isProcessingQueue = false;

    private void Start()
    {
        _lootedItemUIPool = new PoolManager(_lootedItemUIPrefab, this.gameObject.transform, _poolDefaultSize, _poolMaxSize);
    }

    public void EnqueueLoot(string text, Sprite icon)
    {
        _lootQueue.Enqueue((text, icon));

        if (!_isProcessingQueue)
        {
            StartCoroutine(ProcessLootQueue());
        }
    }

    private IEnumerator ProcessLootQueue()
    {
        _isProcessingQueue = true;

        while (_lootQueue.Count > 0)
        {
            var (text, icon) = _lootQueue.Dequeue();

            OnNewItemLooted(text, icon);

            yield return new WaitForSeconds(_timeBetweenLootedItemShow);
        }

        _isProcessingQueue = false;
    }

    public void OnNewItemLooted(string text, Sprite icon)
    {
        GameObject instantiatedLootedShow = _lootedItemUIPool.Get();

        if (instantiatedLootedShow.TryGetComponent<LootedItemUI>(out var lootedItemUI))
        {
            lootedItemUI.transform.SetParent(transform, false);
            lootedItemUI.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            lootedItemUI.SetTextAndIcon(text, icon);
            lootedItemUI.OnDestroyed += OnReturnUIPrefab;
        }

    }

    public void OnReturnUIPrefab(LootedItemUI lootedItemUI)
    {
        lootedItemUI.OnDestroyed -= OnReturnUIPrefab;
        _lootedItemUIPool.Return(lootedItemUI.gameObject);
    }
}