using UnityEngine;
using System.Collections;

public class BottleSpinSound : MonoBehaviour {

    public float angvelToPitchDivider = 600f;
    Rigidbody2D rb2D;
    AudioSource source;

	// Use this for initialization
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float angVel = Mathf.Sqrt( rb2D.angularVelocity * rb2D.angularVelocity);

        if (angVel > 300  && rb2D.rotation % 90 < 20 )
        {
            PlaySoundBite(angVel);
        }
        else if (angVel > 20 && rb2D.rotation % 90 < 20 && !source.isPlaying)
        {
            PlaySoundBite(angVel);
        }
        
    }

    private void PlaySoundBite(float angVel)
    {
        source.pitch = Mathf.Clamp((angVel / angvelToPitchDivider), 1f, 2f);
        source.Play();
    }
}
