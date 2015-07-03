using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using SessionState = SessionManager.SessionState;

public class SessionZeroManager : MonoBehaviour
{
    public GrossBaselineDetection GrossBaselineScript;
    public CustomTextScript CustomText;
    public Text ContinueButtonText;

    private SessionManager.SessionState currentState;
    public bool Continue { get; set; }
	// Use this for initialization
	protected void Start()
	{
	    this.currentState = SessionState.PreBaseline;
	    this.Continue = false;
	    this.ContinueButtonText.text = GlobalizationService.Instance.Globalize(GlobalizationService.ContinueButton);
	}
	
	// Update is called once per frame
    protected void Update()
    {
        System.Action setupNextPhase;
		//coordinate the session state
		switch (currentState) {

            case SessionState.PreBaseline:

                Logger.Instance.LogInformation("Started PreBaseline Screen.");
                setupNextPhase = () =>
                {
                    Logger.Instance.LogInformation("Ended PreBaseline Screen.");
                    this.GrossBaselineScript.enabled = true;
                    Logger.Instance.LogInformation("Started GrossBaseLineDetection Screen");
                    this.currentState = SessionState.GrossBaselineDetection;
	            };
				
				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName("Session Zero", "PreBaseline");
					SessionManager.ActiveActivity = CustomText;
					this.CustomText.Setup("PreBaseline",setupNextPhase);
				}
				else
				{
					this.CustomText.Setup("PreBaselineText",setupNextPhase, GlobalizationService.Instance.Globalize(GlobalizationService.PreBaselineText));
				}

	            this.CustomText.enabled = true;
	            this.currentState = SessionState.CustomText;
		        break;
           
            case SessionState.GrossBaselineDetection:
		        if (this.GrossBaselineScript.CanContinue)
		        {
                    Logger.Instance.LogInformation("Ended GrossBaseLineDetection Screen");
		            this.GrossBaselineScript.EndActivity();
		            this.GrossBaselineScript.enabled = false;
		            this.currentState = SessionState.PostBaseline;
		        }
			    break;

            case SessionState.PostBaseline:
                Logger.Instance.LogInformation("Started PostBaseline Screen");
                setupNextPhase = () =>
                {
                    Logger.Instance.LogInformation("Ended PostBaseline Screen");
                    Application.LoadLevel("SessionOneSimplified");
	            };

				if(StandardConfigurations.IsTheEmoregulatorsAssistantActive)
				{
					Bridge.UpdateWorldActivityName("Session Zero", "PostBaseline");
					SessionManager.ActiveActivity = CustomText;
					this.CustomText.Setup("PostBaseline",setupNextPhase);
				}
				else
				{
					this.CustomText.Setup("PostBaselineText",setupNextPhase, GlobalizationService.Instance.Globalize(GlobalizationService.PostBaselineText));
				}

	            this.CustomText.enabled = true;
	            this.currentState = SessionState.CustomText;
		        break;

            case SessionState.CustomText:
                if (this.Continue)
                {
                    this.Continue = false;
                    UIManagerScript.DisableSkipping();
                    this.CustomText.EndActivity();
                    //setup the next phase
                    this.CustomText.setupNextPhase();
                }
                break;
		    default:
			    Debug.LogError("Unknown/unhandled session state for this session.");
			    break;
		}
	}
}
