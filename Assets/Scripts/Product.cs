using System;
using UnityEngine;
using EditorHelpers;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "New Product", menuName = "Product")]
public class Product : ScriptableObject
{
    public int id;
    public GameObject prefab2D;
    public float spriteHigh;
    public float price = 1;

    [Flags]
    public enum ProductType{
        BurgerIngredient = 1, 
        Soda = 2, 
        Fries = 4,
    }
    [SingleEnumFlagSelect(EnumType = typeof(ProductType))]
    public ProductType productType;
    public Material sodaMaterial;

    public static bool isValidProductType(ProductType collection, ProductType check){
        return collection.HasFlag(check);
    }
}
