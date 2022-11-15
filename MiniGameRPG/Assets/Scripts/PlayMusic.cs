using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource.Play();
    }
}
