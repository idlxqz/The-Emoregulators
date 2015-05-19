using UnityEngine;
using System.Collections;

public class MindfullnessScript : Activity {

    //movie to play
    public Renderer currentRenderer;
    public MovieTexture movTexture;
    public AudioSource movAudio;

    //logic
    public bool played;

	// Use this for initialization
	public override void Start () {
        currentRenderer = GetComponent<Renderer>();
        movAudio = GetComponent<AudioSource>();
        played = false;
        this.CanContinue = false;

        this.SensorManager.StartNewActivity();
	}
	
	// Update is called once per frame
	void Update () {
        if (movTexture.isPlaying)
            return;
        else if (!movTexture.isPlaying && !this.CanContinue && !played)
        {
            currentRenderer.enabled = true;
            currentRenderer.material.mainTexture = movTexture;
            movTexture.Play();
            movAudio.Play();
            played = true;
        }
        else
        {
            currentRenderer.enabled = false;
            this.CanContinue = true;
        }
	}
}
