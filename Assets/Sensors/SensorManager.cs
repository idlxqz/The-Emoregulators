using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensorManager : MonoBehaviour
{
    public static T Max<T>(ICollection<T> collection) where T : struct
    {
        if (collection != null && collection.Any())
        {
            return collection.Max();
        }
        else return default(T);
    }

    public static T Min<T>(ICollection<T> collection) where T : struct
    {
        if (collection != null && collection.Any())
        {
            return collection.Min();
        }
        else return default(T);
    }

    public static double Avg(ICollection<int> collection)
    {
        if (collection != null && collection.Any())
        {
            return collection.Average();
        }
        else return 0;
    }


    public static int HeartRate;
    public static int EDA;
    public static bool Muscle1Active;
    public static bool Muscle2Active;


    public ICollection<int> CurrentActivityHeartRateSamples { get; set; }
    public ICollection<int> SessionHeartRateSamples { get; set; }
    public ICollection<int> BaselineHeartRateSamples { get; set; }
    public ICollection<int> CurrentActivityEDASamples { get; set; }
    public ICollection<int> SessionEDASamples { get; set; }
    public ICollection<int> BaselineEDASamples { get; set; } 

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
        this.SessionEDASamples = new List<int>();
        this.SessionHeartRateSamples = new List<int>();
        this.BaselineHeartRateSamples = new List<int>();
        this.BaselineEDASamples = new List<int>();
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
        this.CurrentActivityEDASamples = new List<int>();
        this.CurrentActivityHeartRateSamples = new List<int>();
    }

    public void StartRecordingBaseline()
    {
        this.BaselineHeartRateSamples = new List<int>();
        this.BaselineEDASamples = new List<int>();
        this.IsRecordingBaseline = true;
    }

    public void StopRecordingBaseline()
    {
        this.IsRecordingBaseline = false;
    }

    
}
