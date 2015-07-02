using UnityEngine;
using System;
using System.Collections;

using ION.Core;
using ION.Core.Events;
using ION.Core.Extensions;
using ION.Meta;
using FAtiMA.RemoteAgent;

public class Write : IONAction
{
	//Constants
	private const int ActionSentenceIndex = 0;
	private const int MultiLineIndex = 1;


	//Action attributes
	private string _subject;
	private string _target;
	private string[] _parameters;


	//Getters and Setters
	protected string ActionSubject { get { return _subject; } }
	protected override string ActionName { get { return "Write"; } }
	protected string ActionTarget { get { return _target; } }
	protected string[] ActionParameters { get { return _parameters; } }
	protected string ActionSentence { get { return this.ActionParameters [ActionSentenceIndex]; } }
	protected bool IsMultiLine { get { return Convert.ToBoolean(this.ActionParameters [MultiLineIndex]); } }
	
	
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
		
		_parameters = actionParameters.Parameters.ToArray ();

		FAtiMA.RemoteAgent.ApplicationLogger.Instance().WriteLine("<OnStart> " + this.ActionSubject + " started " + this.ActionName.ToUpper() + " action on screen " + this.ActionTarget + " with sentence " + this.ActionSentence + " </OnStart>");	
		Debug.Log("<OnStart> " + this.ActionSubject + " started " + this.ActionName.ToUpper() + " action on screen " + this.ActionTarget + " with sentence " + this.ActionSentence + " </OnStart>");
	}
	
	public override void OnStep(IStepped<EntityAnimationAction<ActionParameters>> steppedEvt){
		Bridge.Write (this.ActionTarget, this.ActionSentence, this.IsMultiLine);

		this.Action.Stop(true);
	}
	
	public override void OnStop(IStopped<EntityAnimationAction<ActionParameters>> stoppedEvt){
		FAtiMA.RemoteAgent.ApplicationLogger.Instance().WriteLine("<OnStopped> " + this.ActionSubject + " stopped " + this.ActionName.ToUpper() + " action on screen " + this.ActionTarget + " with sentence " + this.ActionSentence + " </OnStopped>");	
		Debug.Log("<OnStopped> " + this.ActionSubject + " stopped " + this.ActionName.ToUpper() + " action on screen " + this.ActionTarget + " with sentence " + this.ActionSentence + " </OnStopped>");
	}
	#endregion
}

