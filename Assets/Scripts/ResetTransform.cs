using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform: MonoBehaviour
{
    [SerializeField] bool setStartTransformAsDefault = true;
    [SerializeField] Vector3 position;
    [SerializeField] Quaternion rotation;
    [SerializeField] Vector3 scale;

    void Start(){
        if(setStartTransformAsDefault){
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }
    }
    
    public void SetTransformToDefault(){
        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
    }
}
