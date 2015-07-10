using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionThreeOneManager : MonoBehaviour {

	public GameObject facilitator;
	public GameObject candle;
	public GameObject memeter;
	public GameObject ibox;
	public GameObject avatar;

	public GameObject bigibox;
	public GameObject card;
	public GameObject sensorsIcon;
	public GameObject soundIcon;
	public GameObject facilitatorFrame;
	
	bool isMusicReady;
	
	// Use this for initialization
	void Start () {
		/*facilitator.SetActive (false);
		memeter.SetActive (false);
		ibox.SetActive (false);
		sensorsIcon.SetActive (false);
		avatar.SetActive (false);
		facilitatorFrame.SetActive (false);
		card.SetActive (false);
		bigibox.SetActive (false);
		soundIcon.SetActive (false);
		isMusicReady = false;
		candle.SetActive (true);
		candle.GetComponent<CandleScript>().isHidden = false;*/
	}
	
	// Update is called once per frame
	void Update () {
		/*if (!candle.GetComponent<CandleScript> ().isHidden && candle.GetComponent<CandleScript> ().requireAction) {
			candleToggle();
		}
		if (!memeter.GetComponent<MEMeterScript> ().isHidden && memeter.GetComponent<MEMeterScript> ().requireAction) {
			MEMeterToggle();
		}*/
	}

	/*public void candleToggle ()
	{
		candle.GetComponent<CandleScript> ().requireAction = false;
		candle.GetComponent<CandleScript> ().isHidden = true;
		
		if (!candle.GetComponent<CandleScript> ().isActive) {
			candle.GetComponent<CandleScript> ().isActive = true;
			candle.SetActive(false);
			candle.GetComponent<Image>().sprite = candle.GetComponent<CandleScript>().onCandleSprite;
			memeter.SetActive(true);
			memeter.GetComponent<MEMeterScript>().isHidden = false;
			ibox.SetActive(true);
		}
		else  {
			//Application.LoadLevel("SessionThreeTwoScene");
		}
	}
	
	public void MEMeterToggle ()
	{
		memeter.GetComponent<MEMeterScript> ().requireAction = false;
		memeter.GetComponent<MEMeterScript> ().isHidden = true;
		
		if (!memeter.GetComponent<MEMeterScript> ().isActive) {
			memeter.GetComponent<MEMeterScript> ().isActive = true;
			memeter.SetActive(false);
			facilitatorFrame.SetActive(false);
			facilitator.SetActive(true);
			StartCoroutine(SensorsIntro(4));
		}
		else  {
			StartCoroutine(endFacilitatorExplaining(4));
		}
	}

	IEnumerator SensorsIntro (float duration)
	{
		sensorsIcon.SetActive (true);
		sensorsIcon.GetComponent<Animator> ().Play ("video_button_show_intro");
		facilitator.GetComponent<Animator> ().enabled = false;
		facilitator.transform.localPosition = new Vector3 (-140,-130,-40);
		yield return new WaitForSeconds(duration);
		facilitator.transform.localPosition = new Vector3 (347, -482, -40);
		facilitatorFrame.SetActive (true);
		sensorsIcon.SetActive (false);
		StartCoroutine (BreathingRegulation (5));
	}

	IEnumerator BreathingRegulation (float duration){
		avatar.SetActive (true);
		yield return new WaitForSeconds(duration);
		avatar.SetActive (false);
		StartCoroutine (SafePlaceIntro (4));
	}

	IEnumerator SafePlaceIntro (float duration){
		facilitatorFrame.SetActive (false);
		facilitator.transform.localPosition = new Vector3 (-140,-130,-40);
		soundIcon.SetActive (true);
		yield return new WaitForSeconds(duration);
		facilitatorFrame.SetActive (true);
		facilitator.transform.localPosition = new Vector3 (347, -482, -40);
		avatar.SetActive (true);
		yield return new WaitForSeconds(duration);
		iBoxSafePlace ();
	}

	void iBoxSafePlace(){
		avatar.SetActive (false);
		bigibox.SetActive (true);
		card.SetActive (true);
		soundIcon.SetActive (true);
		card.transform.localPosition = new Vector3 (-84, -190, 0);
		soundIcon.transform.localPosition = new Vector3 (76, -188, 0);
		isMusicReady = true;
	}

	public void endMEMeter()
	{
		if (isMusicReady) {
			card.SetActive(false);
			soundIcon.SetActive(false);
			bigibox.SetActive(false);
			memeter.GetComponent<MEMeterScript> ().isHidden = false;
			memeter.SetActive (true);
			memeter.GetComponent<Animator> ().enabled = false;
			memeter.transform.localPosition = new Vector3 (0, -80, 0);
		}
	}
	
	IEnumerator endFacilitatorExplaining (float duration)
	{
		memeter.SetActive (false);
		facilitatorFrame.SetActive (false);
		facilitator.SetActive (true);
		facilitator.GetComponent<Animator> ().enabled = false;
		facilitator.transform.localPosition = new Vector3 (0, -130, -40);
		yield return new WaitForSeconds(duration);
		endCandle ();
	}
	
	public void endCandle()
	{
		candle.GetComponent<CandleScript> ().isHidden = false;
		ibox.SetActive (false);
		facilitator.SetActive (false);
		candle.SetActive (true);
		candle.GetComponent<Animator> ().enabled = false;
		candle.transform.localPosition = new Vector3 (0, -80, 0);
	}*/
}
