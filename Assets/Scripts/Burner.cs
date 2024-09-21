using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Burner : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    AudioSource audioEffect;
    
    void Awake(){
        audioEffect = GetComponent<AudioSource>();
    }

    public void StartFry(SelectEnterEventArgs args){
        if(audioEffect != null)
            audioEffect.Play();
        particles.Play();
        args.interactableObject.transform.gameObject.GetComponent<Food>().StartFrying();
    }
    
    public void StopFry(SelectExitEventArgs args){
        if(audioEffect != null)
            audioEffect.Stop();
        particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        args.interactableObject.transform.gameObject.GetComponent<Food>().StopFrying();
    }
}
