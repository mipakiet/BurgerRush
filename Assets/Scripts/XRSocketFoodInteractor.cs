using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketFoodInteractor : XRSocketInteractor
{
    [SerializeField] bool onlyGoodFood = false;
    [SerializeField] Product.ProductType acceptedProductTypes;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        var food = interactable.transform.GetComponent<Food>();
        return base.CanHover(interactable) && 
            food != null &&
            (!onlyGoodFood || food.IsGood()) && 
            acceptedProductTypes.HasFlag(food.product.productType);
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        var food = interactable.transform.GetComponent<Food>();
        return base.CanSelect(interactable) && 
            food != null &&
            (!onlyGoodFood || food.IsGood()) && 
            acceptedProductTypes.HasFlag(food.product.productType);
    }
}
