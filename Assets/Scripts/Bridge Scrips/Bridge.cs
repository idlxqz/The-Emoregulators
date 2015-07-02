using UnityEngine;
using System.Collections;

using ION.Core;
using ION.Core.Events;
using ION.Core.Extensions;
using ION.Meta;
using FAtiMA.RemoteAgent;

public static class Bridge {

	//Dataflow objects
	//public SessionManager SessionManager { get; set; }
	//public GameObject TheEmoregulatorsAssistant { get; set; }

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

	public static void Write(string activityName, string sentenceCode, bool isMultiLine)
	{
		//Inform SessionManager to write a specific string on board
		CustomTextScript activeActivity = (CustomTextScript)SessionManager.ActiveActivity;

		if (!activeActivity.IsMultiLineSet) 
		{
			activeActivity.IsMultiLine = isMultiLine;

			if(isMultiLine)
			{
				activeActivity.DefineConfigurationAlignment(TextAnchor.UpperLeft);
			}
			else
			{
				activeActivity.DefineConfigurationAlignment(TextAnchor.MiddleLeft);
			}

			activeActivity.IsMultiLineSet = true;
		}

		activeActivity.SetCurrentInstructions (activeActivity.GetCurrentInstructions () + GlobalizationService.Instance.Globalize(sentenceCode));
		activeActivity.SetFullInstructions (activeActivity.GetCurrentInstructions ());
	}

	public static void Say(string activityName, string sentenceCode)
	{
		//Inform SessionManager to play a specific string
	}

	public static void PassiveAssistanceCompleted(string activityName)
	{
		//Enable the possibility of continue in the active activity
		CustomTextScript activeActivity = (CustomTextScript)SessionManager.ActiveActivity;

		activeActivity.EnableEnd ();
	}


}
