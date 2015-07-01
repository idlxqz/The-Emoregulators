using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour {

	//Dataflow objects
	public SessionManager SessionManager { get; set; }
	public GameObject TheEmoregulatorsAssistant { get; set; }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Dataflow from SessionManager to FAtiMA

	public void UpdateWorldActivityName(string newActivityName)
	{
		//change FAtiMA World
	}

	//Dataflow from FAtiMA to SessionManager

	public void Write(string activityName, string sentenceCode)
	{
		//Inform SessionManager to write a specific string on board
	}

	public void Say(string activityName, string sentenceCode)
	{
		//Inform SessionManager to play a specific string
	}


}
