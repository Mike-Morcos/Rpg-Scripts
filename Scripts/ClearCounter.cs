using UnityEngine;

public class ClearCounter : MonoBehaviour, IFoodObjectParent
{
    [SerializeField] private Transform _counterTopPosition;
    [SerializeField] private FoodSO _foodSO;

    private FoodObject _foodObject;

    // Interacts with the player or sets a new food object on the counter.
    public void Interact(Player player)
    {
        // If there is no food object currently on the counter, instantiate a new one.
        if (_foodObject == null)
        {
            InstantiateFoodObjectAtCounter();
        }
        // If there is already a food object, assign it to the player.
        else
        {
            _foodObject.SetFoodObjectParent(player);
        }
    }

    // Instantiates a FoodObject based on the _foodSO prefab at the counter's position.
    private void InstantiateFoodObjectAtCounter()
    {
        Transform foodObjectTransform = Instantiate(_foodSO.prefab, _counterTopPosition.position, Quaternion.identity, _counterTopPosition);
        FoodObject instantiatedFoodObject = foodObjectTransform.GetComponent<FoodObject>();
        if (instantiatedFoodObject != null)
        {
            instantiatedFoodObject.SetFoodObjectParent(this);
            _foodObject = instantiatedFoodObject;
        }
        else
        {
            Debug.LogError("Instantiated object does not have a FoodObject component.", this);
        }
    }

    public Transform GetFoodObjectTransform() => _counterTopPosition;

    public void SetFoodObject(FoodObject foodObject) => _foodObject = foodObject;

    public FoodObject GetFoodObject() => _foodObject;

    public void ClearFoodObject() => _foodObject = null;

    public bool HasFoodObject() => _foodObject != null;
}
