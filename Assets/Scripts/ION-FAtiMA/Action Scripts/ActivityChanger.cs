using UnityEngine;
using System;
using System.Collections;

using ION.Core;
using ION.Core.Events;
using ION.Core.Extensions;
using ION.Meta;
using FAtiMA.RemoteAgent;

public class ActivityChanger : IONAction
{
	//Action attributes
	private string _subject;
	private string _actionName;
	private string _target;
	
	
	//Getters and Setters
	protected string ActionSubject { get { return _subject; } }
	protected override string ActionName { get { return "ActivityChanger"; } }
	protected string ActionTarget { get { return _target; } }


	//Unity Methods
	public void Start()
	{
	}

	public void Initialize()
	{
		base.Initialize ();
	}


	//ION-FAtiMA methods
	#region ION Event Handlers
	public override void OnStart(IStarted<EntityAnimationAction<ActionParameters>> startEvt){
		ActionParameters actionParameters = startEvt.Action.StartArguments;
		
		_subject = actionParameters.Subject;
		_target = actionParameters.Target;

		FAtiMA.RemoteAgent.ApplicationLogger.Instance().WriteLine("<OnStart> " + this.ActionSubject + " ActivityChanger " + this.ActionTarget + " </OnStart>");	
		Debug.Log("<OnStart> " + this.ActionSubject + " ActivityChanger " + this.ActionTarget + " </OnStart>");
	}

	public override void OnStep(IStepped<EntityAnimationAction<ActionParameters>> steppedEvt){
		this.Action.Stop(true);
	}
	
	public override void OnStop(IStopped<EntityAnimationAction<ActionParameters>> stoppedEvt){
		FAtiMA.RemoteAgent.ApplicationLogger.Instance().WriteLine("<OnStopped> " + this.ActionSubject + " ActivityChanger " + this.ActionTarget + " </OnStopped>");	
		Debug.Log("<OnStopped> " + this.ActionSubject + " ActivityChanger " + this.ActionTarget + " </OnStopped>");
	}
	#endregion
}


