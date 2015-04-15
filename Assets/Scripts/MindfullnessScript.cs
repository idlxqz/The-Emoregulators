using UnityEngine;
using System.Collections;

public class MindfullnessScript : MonoBehaviour {

    //movie to play
    public Renderer currentRenderer;
    public MovieTexture movTexture;
    public AudioSource movAudio;

    //logic
    public bool played;
    public bool finished;

	// Use this for initialization
	void Start () {
        currentRenderer = GetComponent<Renderer>();
        movAudio = GetComponent<AudioSource>();
        played = false;
        finished = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (movTexture.isPlaying)
            return;
        else if (!movTexture.isPlaying && !finished && !played)
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
            finished = true;
        }
	}
}
