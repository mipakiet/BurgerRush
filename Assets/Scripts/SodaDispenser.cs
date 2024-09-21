using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SodaDispenser : MonoBehaviour
{
    [SerializeField] XRSocketTagInteractor[] sockets;
    [SerializeField] ParticleSystem[] particlesPouring;
    [SerializeField] Product[] products;
    [SerializeField] AudioSource[] audioEffects;

    public void StartPouring(int index){
        if(audioEffects[index] != null)
            audioEffects[index].Play();
        IXRSelectInteractable cup = sockets[index].GetOldestInteractableSelected();
        particlesPouring[index].Play();
        if(cup != null){
            cup.transform.GetComponent<Cup>().StartPournig(products[index]);
        }
    }
    public void StopPouring(int index){
        if(audioEffects[index] != null)
            audioEffects[index].Stop();
        IXRSelectInteractable cup = sockets[index].GetOldestInteractableSelected();
        particlesPouring[index].Stop();
        if(cup != null){
            cup.transform.GetComponent<Cup>().StopPournig();
        }
    }

}
