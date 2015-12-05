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
        if (!source.isPlaying)
        {
            float diceColForce = Mathf.Clamp(col.relativeVelocity.magnitude, 10, 100);

            source.Play();
            source.volume  = (diceColForce / 100f);
        }
    }
}
