using UnityEngine;
using System.Collections;

using ION.Core;
using FAtiMA.RemoteAgent;
using ION.Core.Extensions;
using System;
using System.IO;

[RequireComponent (typeof(IONEntity))]
public class FAtiMAMind : MonoBehaviour {
	
	protected IONEntity _body;
	protected RemoteMind _mind;
	public bool AgentLaunched { get; protected set; }
	public bool Initialized { get; protected set; }
	
	public string _sex = "M";
	public string _role = "Role";
	public string _scenario = "ConflictScenario";
	public string _scenariosFile = "ConflictScenarios.xml";
	public string _scenarioPath = "data/characters/minds/";
	
	public float _timeForLaunch = 5.0f;
	
	public static void StartAllAgents()
	{
		FAtiMAMind[] agents = GameObject.FindSceneObjectsOfType(typeof(FAtiMAMind)) as FAtiMAMind[];
		if(agents != null)
			foreach(FAtiMAMind agent in agents)
			{
				agent._mind.SendCMDStart();
				agent._mind.SendCMDResumeTime();
			}
	}
	
	public static void StopAllAgents()
	{
		FAtiMAMind[] agents = GameObject.FindSceneObjectsOfType(typeof(FAtiMAMind)) as FAtiMAMind[];
		
		if(agents != null)
			foreach(FAtiMAMind agent in agents)
			{
				agent._mind.SendCMDStopTime();
				agent._mind.SendCMDStop();
			}
	}
	
	public static void StopAllAgentsButMe(string agentName)
	{
		FAtiMAMind[] agents = GameObject.FindSceneObjectsOfType(typeof(FAtiMAMind)) as FAtiMAMind[];
		
		if(agents != null)
		{
			foreach(FAtiMAMind agent in agents)
			{
				if(!agent._body.entityName.Equals(agentName))
				{
					agent._mind.SendCMDStopTime();
					agent._mind.SendCMDStop();
				}
			}
		}
	}
	
	private void OnLevelWasLoaded (int level) {
    }
	
	private void Awake()
	{
		this._body = this.GetComponent<IONEntity>();
		//SyncInitManager.register_entity(_body.Entity);
	}
	
	// Use this for initialization
	void Start () {	
		this._mind = new RemoteMind(_body.Entity,_sex,_role);
		SyncInitManager.register_entity(_body.Entity, _body.entityName);
		
	}
	
	void Update(){
		if(!this.AgentLaunched){
			
			RemoteAgentLoader loader = new RemoteAgentLoader(_mind);
			ScenarioParser.Scenario scenario = ScenarioParser.Instance.GetScenario(_scenarioPath + _scenariosFile, _scenario);
			int port = scenario.GetCharacter(_body.entityName).Port;
			
			LoadRemoteAgentArgs arguments = new LoadRemoteAgentArgs(false, port, _scenarioPath, _scenariosFile, _scenario,"", "", _role, _scenarioPath);
						
			loader.Launch(arguments);			
			//StartCoroutine(WaitForAgentProcess());			
			this.AgentLaunched = true;
		}
		if(this._mind.ConnectionReady == true)
		{
			this._mind.Start();
			this.enabled = false;
		}
	}
	
	void OnDestroy(){
		_mind.SendCMDShutdown();
	}
	
	public void Save()
	{
		this._mind.SendCMDSave();
	}
	
	// GAIPS - Henrique - A little hack for the time being...
	protected IEnumerator WaitForAgentProcess(){
		yield return new WaitForSeconds(_timeForLaunch);
		this.Initialized = true;
		yield return null;
	}
}
