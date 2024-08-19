using TMPro;
using UnityEngine;

public class TextCounter<T> : MonoBehaviour where T : MonoBehaviour, IPoolItem<T>
{
    [SerializeField] private GenericPool<T> _pool;
    [SerializeField] private TextMeshProUGUI _allItemsCountText;
    [SerializeField] private TextMeshProUGUI _newItemsCountText; 
    [SerializeField] private TextMeshProUGUI _activeItemsCountText;

    private void OnEnable()
    {
        _pool.AllItemsCountChanged += ChangeAllItemsCount;
        _pool.NewItemsCountChanged += ChangeNewItemsCount;
        _pool.ActiveItemsCountChanged += ChangeActiveItemsCount;

        ChangeAllItemsCount(0);
        ChangeNewItemsCount(0);
        ChangeActiveItemsCount(0);
    }

    private void OnDisable()
    {
        _pool.AllItemsCountChanged -= ChangeAllItemsCount;
        _pool.NewItemsCountChanged -= ChangeNewItemsCount;
        _pool.ActiveItemsCountChanged -= ChangeActiveItemsCount;

    }

    private void ChangeAllItemsCount(int count)
    {
        _allItemsCountText.text = $"Всего: {count}";
    }

    private void ChangeNewItemsCount(int count)
    {
        _newItemsCountText.text = $"Новые: {count}";
    }

    private void ChangeActiveItemsCount(int count)
    {
        _activeItemsCountText.text = $"На сцене: {count}";
    }
}
