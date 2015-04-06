using UnityEngine;
using System.Collections;

public class Logger{

	//generalize the logging system
	public Logger ()
	{
		
	}

	public void LogInformation(string toLog){
		Debug.Log("INFO["+GetTimeStamp()+"]:"+toLog);
	}

	private static string GetTimeStamp(){
		return System.DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss");
	}
}
