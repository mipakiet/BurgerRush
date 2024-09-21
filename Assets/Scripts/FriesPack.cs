using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class FriesPack : MonoBehaviour
{
    bool hasFries = false;
    public bool HasFries{ get{ return hasFries;}}

    public void OnFriesSelected(SelectEnterEventArgs args){
        Destroy(args.interactableObject.transform.gameObject);
        hasFries = true;
    }
}
