using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
      
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            audioSource.Play();
        }
    }
}
