using UnityEngine;
using System;
using System.Collections;

using ION.Core;
using ION.Core.Events;
using ION.Core.Extensions;
using ION.Meta;
using FAtiMA.RemoteAgent;

public class Say : IONAction
{
	//Constants
	private const int ActionUtteranceIndex = 0;


	//Action attributes
	private string _subject;
	private string _target;
	private string _actionName;
	private string[] _parameters;


	//Getters and Setters
	protected string ActionSubject { get { return _subject; } }
	protected override string ActionName { get { return _actionName; } }
	protected string ActionTarget { get { return _target; } }
	protected string[] ActionParameters { get { return _parameters; } }
	protected string ActionUtterance { get { return this.ActionParameters [0]; } }

	
	//Unity start method
	public void Start ()
	{

	}


	//ION-FAtiMA methods
	#region ION Event Handlers
	public override void OnStart(IStarted<EntityAnimationAction<ActionParameters>> startEvt){
		ActionParameters actionParameters = startEvt.Action.StartArguments;

		_subject = actionParameters.Subject;
		_actionName = actionParameters.ActionType;
		_target = actionParameters.Target;

		_parameters = actionParameters.Parameters.ToArray ();

		Debug.Log("<OnStart> " + this.ActionSubject + " started " + this.ActionName.ToUpper() + " action on screen " + this.ActionTarget + " with utterance " + this.ActionUtterance + " </OnStart>");
	}
	
	public override void OnStep(IStepped<EntityAnimationAction<ActionParameters>> steppedEvt){

	}
	
	public override void OnStop(IStopped<EntityAnimationAction<ActionParameters>> stoppedEvt){
		Debug.Log("<OnStopped> " + this.ActionSubject + " stopped " + this.ActionName.ToUpper() + " action on screen " + this.ActionTarget + " with utterance " + this.ActionUtterance + " </OnStart>");
	}
	#endregion
}

