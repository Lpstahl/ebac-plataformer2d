using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public GameObject coin;
    public ParticleSystem particleSystem;

    private void Awake()
    {
        if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        if (particleSystem != null)
        {
            particleSystem.transform.SetParent(null);
            particleSystem.Play();  
            Destroy(particleSystem.gameObject, 2f);
        }
        coin.gameObject.SetActive(false);
        OnCollect();

    }
    protected virtual void OnCollect() 
    {
        
    }


}


