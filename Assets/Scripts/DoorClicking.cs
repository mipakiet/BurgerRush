using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorClicking : MonoBehaviour
{
    [SerializeField] Transform deafultTransform;
    [SerializeField] float clickRotationY;
    Rigidbody rb;

    void Start(){

        rb = GetComponent<Rigidbody>();
    }

    public void Update(){
        var grabInteractable = GetComponent<XRGrabInteractableExtended>();
        if (!grabInteractable.isSelected) {
            if(rb.velocity != Vector3.zero && Math.Abs(transform.rotation.y - deafultTransform.rotation.y) <= clickRotationY){
                transform.position = deafultTransform.position;
                transform.rotation = deafultTransform.rotation;
                transform.localScale = deafultTransform.localScale;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
