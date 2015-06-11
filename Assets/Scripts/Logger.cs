using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using UnityEngine;

public class Logger{

    private static Logger instance = null;

    private StreamWriter LogStreamWriter;

    public static Logger Instance
    {
        get
        {
            if (instance == null) instance = new Logger();
            return instance;
        }
    }

	//generalize the logging system
    private Logger()
    {
        
    }

	public void LogInformation(string toLog){
        var message = "[" + GetTimeStamp() + "]:" + toLog;
        Debug.Log(message);

	    if (this.LogStreamWriter == null)
	    {
	        if (SessionManager.UserCode != null)
	        {
	            System.IO.Directory.CreateDirectory("logs\\" + SessionManager.UserCode);
                this.LogStreamWriter = new StreamWriter(new FileStream("logs\\" + SessionManager.UserCode + "\\log.txt", FileMode.OpenOrCreate));        
	        }
	    }

	    if (this.LogStreamWriter != null)
	    {
	        this.LogStreamWriter.WriteLine(message);
            this.LogStreamWriter.Flush();
	    }
	}

    public static void LogPhysiologicalSignals(string activityName, ICollection<int> heartRateSamples, ICollection<int> edaSamples)
    {
        var streamWriter = new StreamWriter(new FileStream("logs\\" + SessionManager.UserCode + "\\" + activityName + "_signal_details.csv", FileMode.OpenOrCreate));
        streamWriter.WriteLine("Time\tHR\tEDA");
        for (int i = 0; i < heartRateSamples.Count; i++)
        {
            streamWriter.WriteLine(i*0.1f + "\t" + heartRateSamples.ElementAtOrDefault(i) + "\t" + edaSamples.ElementAtOrDefault(i));
        }
        streamWriter.Flush();
        streamWriter.Close();

        Logger.LogActivitySignalsSummary(activityName, heartRateSamples, edaSamples);
    }

    private static void LogActivitySignalsSummary(string activityName, ICollection<int> heartRateSamples, ICollection<int> edaSamples)
    {
        var streamWriter = new StreamWriter(new FileStream("logs\\" + activityName + "_all_users.csv", FileMode.Append));
        streamWriter.WriteLine(
            SessionManager.UserCode + "\t" 
            + Logger.GetTimeStamp() + "\t" 
            + SensorManager.Avg(heartRateSamples) + "\t"
            + SensorManager.Min(heartRateSamples) + "\t"
            + SensorManager.Max(heartRateSamples) + "\t"
            + SensorManager.Avg(edaSamples) + "\t"
            + SensorManager.Min(edaSamples) + "\t"
            + SensorManager.Max(edaSamples));
        streamWriter.Flush();
        streamWriter.Close();
    }



    public void Close()
    {
        if (this.LogStreamWriter != null)
        {
            this.LogStreamWriter.Flush();
            this.LogStreamWriter.Close();
        }
    }

	private static string GetTimeStamp(){
		return System.DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss");
	}
}
