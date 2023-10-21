using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    
    public void PlaySound(String name) {
        // Loop through every sound
        foreach(Sound s in Sounds) {
            // Play the sound with the matching name
            if(s.soundName.Equals(name)) {
                AudioClip clip = s.sound;
                AudioSource.PlayClipAtPoint(clip, new Vector3(), s.volume);
                return;
            }
        }
        Debug.Log("No sound with name \"" + name + "\" was found");
    }

    
    
}
