using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnOnSelect : MonoBehaviour
{
    [SerializeField] GameObject spawnOnSelectPrefab;

    public void SpawnPrefab(SelectEnterEventArgs args){
        if(spawnOnSelectPrefab != null){
            var prefabGo = Instantiate(spawnOnSelectPrefab, args.interactorObject.transform.position, Quaternion.identity);
            FindFirstObjectByType<XRInteractionManager>().SelectEnter(args.interactorObject, prefabGo.GetComponent<XRGrabInteractable>());
        }
    }
}
