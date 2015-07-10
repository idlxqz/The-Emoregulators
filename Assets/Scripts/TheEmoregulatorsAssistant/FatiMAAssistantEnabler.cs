using UnityEngine;
using System.Collections;

public class FatiMAAssistantEnabler : MonoBehaviour {

	//Attributes
	public bool ActiveAssistant;
	public GameObject TheEmoregulatorsAssistant;
	public GameObject Bridge;

	// Use this for initialization
	void Start () {
		if (ActiveAssistant) 
		{
			transform.gameObject.GetComponent<KeepAlive>().enabled = true;
			transform.gameObject.GetComponent<IONSimulation>().enabled = true;

			TheEmoregulatorsAssistant.SetActive(true);
			Bridge.SetActive(true);
		}

		StandardConfigurations.IsTheEmoregulatorsAssistantActive = ActiveAssistant;
	}
}
