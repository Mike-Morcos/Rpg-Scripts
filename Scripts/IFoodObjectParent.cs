//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public interface IFoodObjectParent
{
    // Getter method for the food object's transform, though the method name might suggest returning the FoodObject's transform instead of the countertop's.
    public Transform GetFoodObjectTransform();
    // Setter method to set the food object. It's clear and straightforward.
    public void SetFoodObject(FoodObject foodObject);

    // Getter method for the food object. It provides a good way to access the private field _foodObject from other classes.
    public FoodObject GetFoodObject();

    // Method to clear the reference to the food object. Simple and effective for resetting the state.
    public void ClearFoodObject();

    public bool HasFoodObject();
}
