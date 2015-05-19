using UnityEngine;

public class GrossBaselineDetection : Activity {

    //movie to play
    public Renderer currentRenderer;
    public MovieTexture movTexture;

    //logic
    public bool played;

	// Use this for initialization
	public override void Start () {
        currentRenderer = GetComponent<Renderer>();
        played = false;
        this.CanContinue = false;

	    this.SensorManager.StartRecordingBaseline();
	}
	
	// Update is called once per frame
	public void Update ()
	{

	    var sensorSocket = GameObject.FindObjectOfType<OpenSignalsSocket>();

	    if (sensorSocket == null || !sensorSocket.SocketReady)
	    {
            this.EndActivity();
            Application.LoadLevel("MainMenuScene");
	        return;
	    }
        
        if (movTexture.isPlaying)
            return;
        else if (!movTexture.isPlaying && !this.CanContinue && !played)
        {
            currentRenderer.enabled = true;
            currentRenderer.material.mainTexture = movTexture;
            movTexture.Play();
            played = true;
        }
        else
        {
            this.EndActivity();
            Application.LoadLevel("MainMenuScene");
        }
	}

    public override void EndActivity()
    {
        this.SensorManager.StopRecordingBaseline();
    }
}
