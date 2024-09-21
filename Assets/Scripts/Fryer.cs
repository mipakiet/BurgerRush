using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fryer : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    AudioSource audioEffect;

    int foodCurrentlyInTrigger = 0;
    
    void Awake(){
        audioEffect = GetComponent<AudioSource>();
    }

    void StartFrying(){
        if(audioEffect != null)
            audioEffect.Play();
        particles.Play();
    }
    
    void StopFrying(){
        if(audioEffect != null)
            audioEffect.Stop();
        particles.Stop();
    }

    void OnTriggerEnter(Collider col) 
    {
        if (col.tag == "Food")
        {
            foodCurrentlyInTrigger++;
            col.transform.gameObject.GetComponent<Food>().StartFrying();
            StartFrying();
        }
    }

    void OnTriggerExit(Collider col) 
    {
        if (col.tag == "Food") 
        {
            foodCurrentlyInTrigger--; 
            col.transform.gameObject.GetComponent<Food>().StopFrying();
            if(foodCurrentlyInTrigger <= 0){
                StopFrying();
            }
        }
    }
}
