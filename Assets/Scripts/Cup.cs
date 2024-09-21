using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] float pouringTime = 2f;
    float timer = 0f;

    [SerializeField] Vector3 SodaEndScale, SodaEndPos;
    Vector3 SodaStartScale, SodaStartPos;
    [SerializeField] Transform Soda;
    bool isPouring = false;

    Product soda;
    [SerializeField] Material wastedMaterail;

    void Start(){
        SodaStartScale = Soda.localScale;
        SodaStartPos = Soda.localPosition;
    }

    public void StartPournig(Product product){
        isPouring = true;
        if(soda == null && timer == 0){
            soda = product;
            Soda.GetComponent<MeshRenderer>().material = product.sodaMaterial;
        }else if(soda != product){
            soda = null;
            Soda.GetComponent<MeshRenderer>().material = wastedMaterail;
        }
    }

    public void StopPournig(){
        isPouring = false;
    }

    void Update(){
        if(isPouring){
            timer += Time.deltaTime;
            Soda.localPosition = Vector3.Lerp(SodaStartPos, SodaEndPos, timer/pouringTime);
            Soda.localScale = Vector3.Lerp(SodaStartScale, SodaEndScale, timer/pouringTime);
        }
    }

    public Product GetSoda(){
        return soda;
    }

    public bool IsCupFull(){
        return timer >= pouringTime;
    }
}
