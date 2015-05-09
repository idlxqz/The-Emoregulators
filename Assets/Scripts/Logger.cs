using UnityEngine;
using System.Collections;

public class Logger{

    private static Logger instance = null;

    public static Logger Instance
    {
        get
        {
            if (instance == null) instance = new Logger();
            return instance;
        }
    }

	//generalize the logging system
	private Logger ()
	{}

	public void LogInformation(string toLog){
		Debug.Log("INFO["+GetTimeStamp()+"]:"+toLog);
	}

	private static string GetTimeStamp(){
		return System.DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss");
	}
}
