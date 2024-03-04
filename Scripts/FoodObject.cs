using UnityEngine;

public class FoodObject : MonoBehaviour
{
    [SerializeField] private FoodSO _foodSO; // Serialized for editor visibility, private for encapsulation.

    private IFoodObjectParent _foodObjectParent; // Interface type for flexibility.

    public void SetFoodObjectParent(IFoodObjectParent newParent)
    {
        // Clear previous parent's reference if any.
        _foodObjectParent?.ClearFoodObject();

        // Assign new parent and update this food object's transform to follow the new parent.
        _foodObjectParent = newParent;
        if (!_foodObjectParent.HasFoodObject())
        {
            _foodObjectParent.SetFoodObject(this);
            transform.SetParent(_foodObjectParent.GetFoodObjectTransform(), false);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.LogError("Parent already has a FoodObject.", this);
        }
    }

    public FoodSO GetFoodSO() => _foodSO;

    public IFoodObjectParent GetFoodObjectParent() => _foodObjectParent;
}
