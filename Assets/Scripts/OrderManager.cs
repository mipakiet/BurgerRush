using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;
using TMPro;


public class OrderManager : MonoBehaviour
{
    //Burger
    [Header("Burger")]
    [SerializeField] Product burgerIngredientTop;
    [SerializeField] Product burgerIngredientBottom;
    [SerializeField] Product[] burgerIngredientsMiddle;
    [SerializeField] int burgerMaxIngredients = 6;
    [SerializeField] Transform burgerIngredients2DParent;
    [SerializeField] Transform burgerDrawPos;

    Product[] burgerOrder;

    [SerializeField] XRSocketTagInteractor plateSocket;
    GameObject plateGO;

    //Soda
    [Header("Soda")]
    [SerializeField] Product[] sodaProduct;
    [SerializeField] Transform sodaTransorm2DParent;
    [Range(0f, 1f)]
    [SerializeField] float noSodaInOrderProbability;

    Product sodaOrder;

    [SerializeField] XRSocketTagInteractor cupSocket;
    GameObject cupGO;

    //Fries
    [Header("Fries")]
    [SerializeField] Product friesProduct;
    [SerializeField] Transform friesTransorm2DParent;
    [Range(0f, 1f)]
    [SerializeField] float noFriesInOrderProbability;

    bool friesInOrder;

    [SerializeField] XRSocketTagInteractor freisPackSocket;
    GameObject friesPackGO;

    //Others
    [Header("Others")]
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text orderPriceText;
    float money;
    float orderPrice;

    [SerializeField] ParticleSystem correctOrderParticles;
    [SerializeField] AudioSource audioEffect;

    void Start(){
        ResetOrderAndShow();
        money = 0;
        moneyText.text = MoneyToString(money);
    }

    void SetRandomOrder(){
        //Burger
        burgerOrder = new Product[Random.Range(3,burgerMaxIngredients+1)];

        burgerOrder[0] = burgerIngredientBottom;
        burgerOrder[^1] = burgerIngredientTop;
        orderPrice += burgerIngredientBottom.price;
        orderPrice += burgerIngredientTop.price;

        for(int i=1; i<burgerOrder.Length-1; i++){
            burgerOrder[i] = burgerIngredientsMiddle[Random.Range(0, burgerIngredientsMiddle.Length)];
            if(burgerOrder[i] == burgerOrder[i-1]){
                i--;
            }
            else{
                orderPrice += burgerOrder[i].price;
            }
        }

        //Soda
        if(Random.Range(0f, 1f) < noSodaInOrderProbability){
            sodaOrder = null;
        }else{
            sodaOrder = sodaProduct[Random.Range(0, sodaProduct.Length)];
            orderPrice += sodaOrder.price;
        }

        //Fries
        if(Random.Range(0f, 1f) < noFriesInOrderProbability){
            friesInOrder = false;
        }else{
            friesInOrder = true;
            orderPrice += friesProduct.price;
        }
    }

    void DrawOnMonitor(){
        //Burger
        foreach(Transform child in burgerIngredients2DParent.transform){
            if(child.tag == "Product2D")
                Destroy(child.gameObject);
        }

        for(int i=0; i<burgerOrder.Length; i++){
            var ingredient2D = Instantiate(burgerOrder[i].prefab2D, burgerDrawPos.position, Quaternion.identity, burgerIngredients2DParent);
            ingredient2D.GetComponentInChildren<SpriteRenderer>().sortingOrder = i;
            ingredient2D.transform.localRotation = Quaternion.identity;
            burgerDrawPos.localPosition += Vector3.up * burgerOrder[i].spriteHigh * 1.2f;
        }
        
        //Soda
        foreach(Transform child in sodaTransorm2DParent.transform){
            Destroy(child.gameObject);
        }
        if(sodaOrder != null)
            Instantiate(sodaOrder.prefab2D, sodaTransorm2DParent);

        //Fries
        foreach(Transform child in friesTransorm2DParent.transform){
            Destroy(child.gameObject);
        }
        if(friesInOrder)
            Instantiate(friesProduct.prefab2D, friesTransorm2DParent);

        //Money
        orderPriceText.text = MoneyToString(orderPrice);
    }

    public void ResetOrderAndShow(){
        burgerDrawPos.localPosition = Vector3.zero;
        orderPrice = 0;
        SetRandomOrder();
        DrawOnMonitor();
    }
    
    bool IngedientArraysEqual(Product[] firstArray, Product[] secondArray)
    {
        if (firstArray.Length != secondArray.Length)
            return false;

        for (int i = 0; i < firstArray.Length; i++)
        {
            if (firstArray[i] != secondArray[i])
                return false;
        }

        return true;
    }

    bool CheckOrderCorectness(){
        IXRSelectInteractable plateInteractable = plateSocket.GetOldestInteractableSelected();
        if(plateInteractable != null){
            plateGO = plateInteractable.transform.gameObject;
            var plate = plateGO.GetComponent<Plate>();
            if(!IngedientArraysEqual(plate.GetIngerdients(), burgerOrder)){
                Debug.Log("Wrong ingerdients");
                return false;
            }
        }else{
            Debug.Log("No plate");
            return false;
        }

        IXRSelectInteractable cupInteractable = cupSocket.GetOldestInteractableSelected();
        if(cupInteractable != null){
            cupGO = cupInteractable.transform.gameObject;
            var cup = cupGO.GetComponent<Cup>();
            var soda = cup.GetSoda();
            if(sodaOrder == null || soda == null || sodaOrder.id != soda.id || !cup.IsCupFull()){
                Debug.Log("Soda but wrong or not needed or not full");
                return false;
            }
        }else{
            if(sodaOrder != null){
                Debug.Log("No soda");
                return false;
            }
        }

        IXRSelectInteractable friesPackInteractable = freisPackSocket.GetOldestInteractableSelected();
        if(friesPackInteractable != null){
            friesPackGO = friesPackInteractable.transform.gameObject;

            if(!friesInOrder || !friesPackGO.GetComponent<FriesPack>().HasFries){
                Debug.Log("Fries mot needed or empty pack");
                return false;
            }
        }else{
            if(friesInOrder){
                Debug.Log("No fires");
                return false;
            }
        }

        return true;
    }

    public void CloseOrderIfCorrect(){
        if(CheckOrderCorectness()){
            if(audioEffect != null)
                audioEffect.Play();
            correctOrderParticles.Play();
            if(plateGO != null)
                Destroy(plateGO);
            if(cupGO != null)
                Destroy(cupGO);
            if(friesPackGO != null)
                Destroy(friesPackGO);
            money += orderPrice;
            moneyText.text = MoneyToString(money);
            ResetOrderAndShow();
        }
    }

    public string MoneyToString(float money){
        return money.ToString("0.00") + " $";
    }

    public float GetMoney(){
        return money;
    }

    public void ResetMoney(){
        money = 0;
    }
}
