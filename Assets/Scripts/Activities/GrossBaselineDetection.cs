using UnityEngine;

public class GrossBaselineDetection : Activity {

    //movie to play
    public Renderer currentRenderer;
    public MovieTexture movTexture;

    //logic
    public bool played;

	// Use this for initialization
	public override void Start ()
	{
	    this.Name = "GrossBaselineDetection";
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
            this.CanContinue = true;
            return;
        }

	    if (movTexture.isPlaying)
	    {
	        return;
	    }
        else if (!movTexture.isPlaying && !this.CanContinue && !played)
        {
            currentRenderer.enabled = true;
            currentRenderer.material.mainTexture = movTexture;
            movTexture.Play();
            played = true;
            Logger.Instance.LogInformation("Gross Baseline Video Started");
        }
        else
        {
            this.CanContinue = true;
            
        }
	}

    public override void EndActivity()
    {
        this.currentRenderer.enabled = false;
        this.SensorManager.StopRecordingBaseline();

        Logger.Instance.LogInformation("Gross Baseline Video Ended");
        Logger.Instance.LogInformation("BaselineHR (avg,min,max): " + SensorManager.Avg(this.SensorManager.BaselineHeartRateSamples) + ", " + SensorManager.Min(this.SensorManager.BaselineHeartRateSamples) + ", " + SensorManager.Max(this.SensorManager.BaselineHeartRateSamples));
        Logger.Instance.LogInformation("BaselineEDA (avg,min,max): " + SensorManager.Avg(this.SensorManager.BaselineEDASamples) + ", " + SensorManager.Min(this.SensorManager.BaselineEDASamples) + ", " + SensorManager.Max(this.SensorManager.BaselineEDASamples));
        Logger.LogPhysiologicalSignals("Baseline", this.SensorManager.BaselineHeartRateSamples, this.SensorManager.BaselineEDASamples);
        
    }
}
