using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class ChoppedObject : MonoBehaviour
{
    [SerializeField] string toolTag;
    [SerializeField] GameObject spawnGO;
    [SerializeField] int spawnGOAmount;
    [SerializeField] int neededCuts;
    [SerializeField] ParticleSystem particles;
    [SerializeField] Transform spawnTransform;
    [SerializeField] bool destroyWhenChoped;
    [Range(0,2)]
    [SerializeField] float timeBetweenCuts;
    float timer = 0;
    [SerializeField] bool rotationFromParent = false;


    void Update(){
        if(timer >= 0)
            timer -= Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == toolTag && timer <= 0){
            var audioSource = collider.gameObject.GetComponent<AudioSource>();

            if(audioSource != null)
                audioSource.Play();
            if(particles != null)
                particles.Play();

            neededCuts--;
            if(neededCuts <= 0){
                for(int i=0; i<spawnGOAmount; i++)
                    if(rotationFromParent)
                        Instantiate(spawnGO, spawnTransform.position, transform.rotation);
                    else
                        Instantiate(spawnGO, spawnTransform.position, Quaternion.identity);
                if(destroyWhenChoped)
                    Destroy(gameObject);
            }
            timer = timeBetweenCuts;
        }
        
    }
}
