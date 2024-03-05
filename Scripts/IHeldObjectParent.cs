using UnityEngine;

public interface IHeldObjectParent
{
    public Transform GetHeldObjectTransform(); 
    public void SetHeldObject(HeldObject heldObject);
    public HeldObject GetHeldObject();
    public void ClearHeldObject();
    public bool HasHeldObject();
}
