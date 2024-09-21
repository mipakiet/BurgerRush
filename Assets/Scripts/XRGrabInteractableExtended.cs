using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableExtended : XRGrabInteractable
{
    [SerializeField] Transform attachLeftHand;
    [SerializeField] Transform attachRightHand;
    [SerializeField] string leftHandTag;
    [SerializeField] string rightHandTag;

    [SerializeField] float ungrabDistance = 0;
    [SerializeField] Transform handler;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.gameObject.tag == leftHandTag){
            base.attachTransform = attachLeftHand;
        }else if(args.interactorObject.transform.gameObject.tag == rightHandTag){
            base.attachTransform = attachRightHand;
        }
        base.OnSelectEntering(args);
    }

    private void Update(){
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable.isSelected) {
            var interactor = grabInteractable.firstInteractorSelecting as XRBaseInteractor;
            if (interactor != null) {
                if(ungrabDistance != 0 && Vector3.Distance(handler.position, interactor.transform.position) >= ungrabDistance){
                    interactionManager.SelectExit(interactor, grabInteractable);
                }
            }
        }
    }
}
