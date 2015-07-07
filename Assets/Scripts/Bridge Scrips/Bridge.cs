using UnityEngine;
using System.Collections;

using ION.Core;
using ION.Core.Events;
using ION.Core.Extensions;
using ION.Meta;
using FAtiMA.RemoteAgent;

using System.ComponentModel;

public class Bridge : MonoBehaviour {

	//Dataflow objects
	//public SessionManager SessionManager { get; set; }
	//public GameObject TheEmoregulatorsAssistant { get; set; }

	//Workflow variables

	//Write
	//Say

	//Unity Methods
	public void Start()
	{
	}

	//Dataflow from SessionManager to FAtiMA

	public static void UpdateWorldActivityName(string activityName, string activityScreenName)
	{
		ActivityChanger activityChanger = GameObject.FindGameObjectWithTag("Bridge").GetComponentInChildren<ActivityChanger>();
		
		ActionParameters args = new ActionParameters();
		args.ActionType = "ActivityChanger";
		args.Subject = activityName;
		args.Target = activityScreenName;
		
		activityChanger.Action.Start(args);
	}

	//Dataflow from FAtiMA to SessionManager

	public static void Write(string activityName, string sentenceCode, int waitTime, bool isMultiLine)
	{
		//Inform SessionManager to write a specific string on board
		//StartCoroutine((new Bridge()).WaitAndWrite(sentenceCode,waitTime,isMultiLine));
		Activity activeActivity = SessionManager.ActiveActivity;
		activeActivity.WriteInstruction (GlobalizationService.Instance.Globalize (sentenceCode), isMultiLine);

	}

	public static void Say(string activityName, string sentenceCode)
	{
		//Inform SessionManager to play a specific string
	}

	public static void PassiveAssistanceCompleted(string activityName)
	{
		//Enable the possibility of continue in the active activity
		Activity activeActivity = SessionManager.ActiveActivity;

		activeActivity.EnableEnd ();
	
	}


}
