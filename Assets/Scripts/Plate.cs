using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Plate : MonoBehaviour
{
    [SerializeField] Transform attachTransform;
    [SerializeField] GameObject socket;

    [SerializeField] int maxIngredients = 6;
    BoxCollider boxCollider;

    List<Product> ingredientsQueue;

    AudioSource audioEffect;

    void Awake(){
        audioEffect = GetComponent<AudioSource>();
    }
    
    void Start(){
        boxCollider = GetComponent<BoxCollider>();
        ingredientsQueue = new List<Product>();
    }

    public void PutOnPlate(SelectEnterEventArgs args){
        if(audioEffect != null)
            audioEffect.Play();

        var interactableObjectTransform = args.interactableObject.transform;
        var height = interactableObjectTransform.GetComponent<BoxCollider>().size.y;

        Destroy(interactableObjectTransform.GetComponent<BoxCollider>());
        Destroy(interactableObjectTransform.GetComponent<XRGrabInteractable>());
        interactableObjectTransform.GetComponent<Rigidbody>().isKinematic = true;
        interactableObjectTransform.position = attachTransform.position;
        interactableObjectTransform.parent = gameObject.transform;
        interactableObjectTransform.localRotation = Quaternion.identity;

        attachTransform.localPosition += Vector3.up * height;
        boxCollider.size += Vector3.up * height;
        boxCollider.center += Vector3.up * height/2;

        var food = interactableObjectTransform.GetComponent<Food>();
        food.PutOnPlateEvent();
        ingredientsQueue.Add(food.product);

        if(ingredientsQueue.Count >= maxIngredients){
            socket.gameObject.SetActive(false);
        } 
    }

    public Product[] GetIngerdients(){
        return ingredientsQueue.ToArray();
    }
}
