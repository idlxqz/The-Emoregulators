using UnityEngine;
using System.Collections.Generic;
using ION.Meta;
using ION.Core;
using ION.Core.Extensions;

public 
	class
	SyncInitManager  
{
	static List<Element> entities2Wait4 = new List<Element>(10);
	
	static Entity statusManager = null;	
		
	public static Entity StatusManager{set{ statusManager = value;}}	
		
	/// <summary>
	/// Register_entity the specified _entity.
	/// </summary>
	public static void register_entity(Element _entity, string _name = ""){
		if(entities2Wait4.Contains(_entity) == false){
			entities2Wait4.Add(_entity);
			_entity.EventHandlers.Add<InitialisationFinishedEvent>(OnInitialisationFinished);
			Debug.LogWarning("<SyncInitManager> Waiting 4 Entity: " + _name);				
		}
	}/*END OF register_entity(IONEntity)*/	
		
	static void OnInitialisationFinished(InitialisationFinishedEvent _event){
		if(entities2Wait4.Contains(_event.Entity) == true){
			entities2Wait4.Remove(_event.Entity);
			_event.Entity.EventHandlers.Remove<InitialisationFinishedEvent>(OnInitialisationFinished);
				
			Debug.LogWarning("<SyncInitManager> Entity: " + _event.Name + " has just finished initialisation!");
			if(entities2Wait4.Count == 0){
				if(statusManager != null)
					statusManager.SetProperty<bool>("*", "initDone", true);
				Debug.LogWarning("<SyncInitManager> Initialisation completed!");
			}
		}	
	}/* END OF OnInitialisationFinished(InitialisationFinishedEvent) */
}

public 
	class 
	InitialisationFinishedEvent : 
		ION.Meta.Event
{
	Element entity = null;
	string name = "";
	public string Name{get{return name;}}
	public Element Entity{get{return entity;}}	
	public InitialisationFinishedEvent(Element _entity, string _name = ""){entity = _entity; name = _name;}
}/*END OF event class InitialisationFinishedEvent*/
