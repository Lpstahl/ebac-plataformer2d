using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public GameObject coin;
    public ParticleSystem particleSystem;

    [Header("Sounds")]
    public AudioSource audioSource;

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

        //Desligar collider da moeda apos ser coletada
        Collider2D[] cols = gameObject.GetComponentsInChildren<Collider2D>();
        for(int i = 0; i < cols.Length; ++i)
        {
            cols[i].enabled = false;
        }        

        coin.gameObject.SetActive(false);
        OnCollect();
        

    }
    protected virtual void OnCollect() 
    {
        if (audioSource != null) audioSource.Play();
    }


}


