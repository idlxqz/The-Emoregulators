using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    public static int HeartRate;
    public static double EDA;
    public static bool MuscleActive;


    public ICollection<int> CurrentActivityHeartRateSamples { get; set; }
    public ICollection<int> SessionHeartRateSamples { get; set; }

    public ICollection<int> BaselineHeartRateSamples { get; set; } 
    public ICollection<double> CurrentActivityEDASamples { get; set; }
    public ICollection<double> SessionEDASamples { get; set; }

    public ICollection<double> BaselineEDASamples { get; set; } 

    private float MeasuringInterval { get; set; }

    private float LastMeasurementTime { get; set; }

    private bool IsRecordingBaseline { get; set; }

    public SensorManager()
    {
        this.MeasuringInterval = 0.1f;
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        this.SessionEDASamples = new List<double>();
        this.SessionHeartRateSamples = new List<int>();
        this.BaselineHeartRateSamples = new List<int>();
        this.BaselineEDASamples = new List<double>();
        this.StartNewActivity();
        this.IsRecordingBaseline = false;
        this.LastMeasurementTime = Time.time;
    }

    public void Update()
    {
        if (Time.time - this.LastMeasurementTime > this.MeasuringInterval)
        {
            this.LastMeasurementTime = Time.time;
            this.SessionEDASamples.Add(SensorManager.EDA);
            this.CurrentActivityEDASamples.Add(SensorManager.EDA);
            this.SessionHeartRateSamples.Add(SensorManager.HeartRate);
            this.CurrentActivityHeartRateSamples.Add(SensorManager.HeartRate);

            if (this.IsRecordingBaseline)
            {
                this.BaselineHeartRateSamples.Add(SensorManager.HeartRate);
                this.BaselineEDASamples.Add(SensorManager.EDA);
            }
        }
        
    }

    public void StartNewActivity()
    {
        this.CurrentActivityEDASamples = new List<double>();
        this.CurrentActivityHeartRateSamples = new List<int>();
    }

    public void StartRecordingBaseline()
    {
        this.BaselineHeartRateSamples = new List<int>();
        this.BaselineEDASamples = new List<double>();
        this.IsRecordingBaseline = true;
    }

    public void StopRecordingBaseline()
    {
        this.IsRecordingBaseline = false;
    }
}
