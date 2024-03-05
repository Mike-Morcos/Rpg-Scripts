using UnityEngine;

public class HeldObject : MonoBehaviour
{
    [SerializeField] private InventoryItemSO _inventoryItemSO;
    private IHeldObjectParent _heldObjectParent;

    public void SetHeldObjectParent(IHeldObjectParent newParent)
    {
        _heldObjectParent?.ClearHeldObject();
        _heldObjectParent = newParent;
        if (!_heldObjectParent.HasHeldObject())
        {
            _heldObjectParent.SetHeldObject(this);
            transform.SetParent(_heldObjectParent.GetHeldObjectTransform(), false);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("Parent already has a HeldObject.", this);
        }
    }

    public InventoryItemSO GetInventoryItemSO() => _inventoryItemSO;

    public IHeldObjectParent GetHeldObjectParent() => _heldObjectParent;
}
