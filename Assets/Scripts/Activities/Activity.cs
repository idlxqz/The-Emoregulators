using System.Linq;
using UnityEngine;

public class Activity : MonoBehaviour
{
    protected StandardConfigurations Configurations;
    protected SensorManager SensorManager;
    public bool CanContinue;

    protected virtual void Awake()
    {
        this.Configurations = GameObject.FindObjectOfType<StandardConfigurations>();
        this.SensorManager = GameObject.FindObjectOfType<SensorManager>();
    }

    public virtual void Start()
    {
        this.CanContinue = false;
        this.SensorManager.StartNewActivity();
    }

    public virtual void EndActivity()
    {
        this.enabled = false;

        Logger.Instance.LogInformation("Player score: " + SessionManager.PlayerScore);
        Logger.Instance.LogInformation("Activity HR (Avg,Min,Max): " + this.SensorManager.CurrentActivityHeartRateSamples.Average() + "," + this.SensorManager.CurrentActivityHeartRateSamples.Min() + "," + this.SensorManager.CurrentActivityHeartRateSamples.Max());
        Logger.Instance.LogInformation("Activity EDA (Avg,Min,Max): " + this.SensorManager.CurrentActivityEDASamples.Average() + "," + this.SensorManager.CurrentActivityEDASamples.Min() + "," + this.SensorManager.CurrentActivityEDASamples.Max());
    }
}
