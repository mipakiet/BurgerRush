using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{
    public Product product;
    public UnityEvent putOnPlate;
    [SerializeField] Material friedMaterial, burnnedMaterial;
    MeshRenderer meshRenderer;

    [SerializeField] float timeToFry;
    float timer;
    bool frying;

    enum State{
        raw, good, burned
    } 
    [SerializeField] State state;


    void Awake(){
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    void Update(){
        if(frying){
            timer += Time.deltaTime;
            if(timer >= timeToFry){
                Fry();
                timer = 0;
            }
        }
    }

    public void StartFrying(){
        timer = 0;
        frying = true;
    }
    
    public void StopFrying(){
        frying = false;
    }

    void Fry(){
        if(state == State.raw){
            state = State.good;
            meshRenderer.material = friedMaterial;
        }
        else if(state == State.good){
            state = State.burned;
            meshRenderer.material = burnnedMaterial;
        }
    }
    
    public bool IsGood(){
        return state == State.good;
    }
    
    public void PutOnPlateEvent(){
        putOnPlate.Invoke();
    }
}
