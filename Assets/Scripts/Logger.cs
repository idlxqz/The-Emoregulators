using System.IO;
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
                this.LogStreamWriter = new StreamWriter(new FileStream("logs\\" + SessionManager.UserCode + ".log", FileMode.OpenOrCreate));        
	        }
	    }

	    if (this.LogStreamWriter != null)
	    {
	        this.LogStreamWriter.WriteLine(message);
            this.LogStreamWriter.Flush();
	    }
	}

	private static string GetTimeStamp(){
		return System.DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss");
	}
}
