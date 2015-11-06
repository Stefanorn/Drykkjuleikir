using UnityEngine;
using System.Collections;

public class DiceSound : MonoBehaviour {

    AudioSource source;


    void Start()
    {

        source = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision col)
    {

        Debug.Log( col.relativeVelocity.magnitude);
        if (!source.isPlaying)
        {
            source.Play();
            source.pitch = Random.Range(0.8F, 1.2F);
        }
    }
}
