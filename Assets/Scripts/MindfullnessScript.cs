using UnityEngine;
using System.Collections;

public class MindfullnessScript : MonoBehaviour {

    //movie to play
    public Renderer renderer;
    public MovieTexture movTexture;
    public AudioSource movAudio;

    //logic
    public bool played;
    public bool finished;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
        movAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (movTexture.isPlaying)
            return;
        else if (!movTexture.isPlaying && !finished)
        {
            renderer.enabled = true;
            renderer.material.mainTexture = movTexture;
            movTexture.Play();
            movAudio.Play();
        }
        else
        {
            renderer.enabled = false;
            finished = true;
        }
	}
}
