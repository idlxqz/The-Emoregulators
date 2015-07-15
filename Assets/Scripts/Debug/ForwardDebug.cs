using UnityEngine;
using System.Collections;

public class ForwardDebug : MonoBehaviour {

	public void Start()
	{
	}


	public void Forward()
	{
		GameObject sessionManagerObject = GameObject.Find ("SessionManager");
		if(sessionManagerObject != null)
		{

			if(sessionManagerObject.GetComponent<SessionZeroManager>() != null)
			{
				sessionManagerObject.GetComponent<SessionZeroManager>().Continue = true;
			}
			else
			{
				SessionManager activeSessionManager; 

				if(((activeSessionManager = sessionManagerObject.GetComponent<SessionOneSimplifiedManager>()) != null) ||
				   ((activeSessionManager = sessionManagerObject.GetComponent<SessionFourManager>()) != null))
				{
					activeSessionManager.Continue ();
				}
				else
				{
					Debug.Log("Not a valid Session Manager to skip/forward");
				}
			}
				
		}
		else
		{
			Debug.Log("Not a valid Session to skip/forward");
		}

	}

}
