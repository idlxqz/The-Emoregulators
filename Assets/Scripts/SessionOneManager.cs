using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionOneManager : MonoBehaviour {

	public GameObject facilitator;
	public GameObject candle;
	public GameObject memeter;
	public GameObject ibox;

	public GameObject facilitatorFrame;

	public GameObject introducingOurselves;

	// Use this for initialization
	void Start () {
		facilitator.SetActive (false);
		candle.SetActive (false);
		memeter.SetActive (false);
		ibox.SetActive (false);
		facilitatorFrame.SetActive (false);
		introducingOurselves.SetActive (false);
		facilitator.transform.localPosition = new Vector3 (0, -130, -40);
		StartCoroutine(FacilitatorIntro (4));
	}
	
	// Update is called once per frame
	void Update () {
		if (!candle.GetComponent<CandleScript> ().isHidden && candle.GetComponent<CandleScript> ().requireAction) {
			candleToggle();
		}
		if (!memeter.GetComponent<MEMeterScript> ().isHidden && memeter.GetComponent<MEMeterScript> ().requireAction) {
			MEMeterToggle();
		}
	}

	IEnumerator FacilitatorIntro (float duration)
	{
		facilitator.SetActive (true);
		yield return new WaitForSeconds(duration);
		facilitator.GetComponent<Animator> ().Play ("facilitator_slide_left_intro");
		StartCoroutine(CandleIntro (3));
	}

	IEnumerator CandleIntro (float duration)
	{
		candle.SetActive (true);
		candle.GetComponent<Animator> ().Play ("candle_show_intro");
		yield return new WaitForSeconds(duration);
		facilitator.GetComponent<Animator> ().enabled = false;
		facilitator.transform.localPosition = new Vector3 (347, -482, -40);
		facilitatorFrame.SetActive (true);
		candle.GetComponent<Animator> ().Play ("candle_slide_left_intro");
		candle.GetComponent<CandleScript> ().isHidden = false;
	}

	public void candleToggle ()
	{
		candle.GetComponent<CandleScript> ().requireAction = false;
		candle.GetComponent<CandleScript> ().isHidden = true;

		if (!candle.GetComponent<CandleScript> ().isActive) {
			candle.GetComponent<CandleScript> ().isActive = true;
			candle.SetActive(false);
			candle.GetComponent<Image>().sprite = candle.GetComponent<CandleScript>().onCandleSprite;
			introducingOurselves.SetActive (true);
		}
		else  {
			Application.LoadLevel("SessionTwoScene");
		}
	}

	public void checkButton ()
	{
		introducingOurselves.SetActive (false);
		facilitatorFrame.SetActive(false);
		facilitator.transform.localPosition = new Vector3 (-140, -130, -40);
		StartCoroutine(MEMeterIntro (4));
	}

	IEnumerator MEMeterIntro (float duration)
	{
		memeter.SetActive (true);
		memeter.GetComponent<Animator> ().Play ("memeter_show_intro");
		yield return new WaitForSeconds(duration);
		facilitator.GetComponent<Animator> ().enabled = false;
		facilitator.transform.localPosition = new Vector3 (347, -482, -40);
		facilitatorFrame.SetActive (true);
		memeter.GetComponent<Animator> ().Play ("memeter_slide_left_intro");
		memeter.GetComponent<MEMeterScript> ().isHidden = false;
	}

	public void MEMeterToggle ()
	{
		memeter.GetComponent<MEMeterScript> ().requireAction = false;
		memeter.GetComponent<MEMeterScript> ().isHidden = true;

		if (!memeter.GetComponent<MEMeterScript> ().isActive) {
			memeter.GetComponent<MEMeterScript> ().isActive = true;
			memeter.SetActive(false);
			facilitatorFrame.SetActive(false);
			StartCoroutine(FacilitatorExplaining(4));
		}
		else  {
			StartCoroutine(endFacilitatorExplaining(4));
		}
	}

	IEnumerator FacilitatorExplaining (float duration)
	{
		facilitator.SetActive (true);
		facilitator.GetComponent<Animator> ().enabled = false;
		facilitator.transform.localPosition = new Vector3 (0, -130, -40);
		yield return new WaitForSeconds(duration);
		facilitator.GetComponent<Animator> ().enabled = true;
		facilitator.GetComponent<Animator> ().Play ("facilitator_slide_left_intro");
		StartCoroutine (IBoxIntro (3));
	}

	IEnumerator IBoxIntro (float duration)
	{
		ibox.SetActive (true);
		ibox.GetComponent<Animator> ().Play ("ibox_show_intro");
		yield return new WaitForSeconds(duration);
		facilitator.GetComponent<Animator> ().enabled = false;
		facilitator.transform.localPosition = new Vector3 (0, -130, -40);
		ibox.GetComponent<Image> ().sprite = ibox.GetComponent<IBoxScript> ().offBoxSprite;
		ibox.GetComponent<Animator> ().Play ("ibox_slide_right");
		ibox.GetComponent<IBoxScript> ().isHidden = false;
		yield return new WaitForSeconds(duration);
		endMEMeter ();
	}

	public void endMEMeter()
	{
		memeter.GetComponent<MEMeterScript> ().isHidden = false;
		facilitator.SetActive (false);
		memeter.SetActive (true);
		memeter.GetComponent<Animator> ().enabled = false;
		memeter.transform.localPosition = new Vector3 (0, -80, 0);
	}

	IEnumerator endFacilitatorExplaining (float duration)
	{
		memeter.SetActive (false);
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
	}
}
