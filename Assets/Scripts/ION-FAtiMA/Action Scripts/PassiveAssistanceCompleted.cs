using UnityEngine;
using System;
using System.Collections;

using ION.Core;
using ION.Core.Events;
using ION.Core.Extensions;
using ION.Meta;
using FAtiMA.RemoteAgent;

public class PassiveAssistanceCompleted : IONAction
{
	//Action attributes
	private string _subject;
	private string _target;
	
	
	//Getters and Setters
	protected string ActionSubject { get { return _subject; } }
	protected override string ActionName { get { return "PassiveAssistanceCompleted"; } }
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
		
		FAtiMA.RemoteAgent.ApplicationLogger.Instance().WriteLine("<OnStart> " + this.ActionSubject + " PassiveAssistanceCompleted " + this.ActionTarget + " </OnStart>");	
		Debug.Log("<OnStart> " + this.ActionSubject + " PassiveAssistanceCompleted " + this.ActionTarget + " </OnStart>");
	}
	
	public override void OnStep(IStepped<EntityAnimationAction<ActionParameters>> steppedEvt){
		Bridge.PassiveAssistanceCompleted (this.ActionTarget);

		this.Action.Stop(true);
	}
	
	public override void OnStop(IStopped<EntityAnimationAction<ActionParameters>> stoppedEvt){
		FAtiMA.RemoteAgent.ApplicationLogger.Instance().WriteLine("<OnStopped> " + this.ActionSubject + " PassiveAssistanceCompleted " + this.ActionTarget + " </OnStopped>");	
		Debug.Log("<OnStopped> " + this.ActionSubject + " PassiveAssistanceCompleted " + this.ActionTarget + " </OnStopped>");
	}
	#endregion
}


