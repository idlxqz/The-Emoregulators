using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SessionTwoManager : MonoBehaviour {

	public GameObject facilitator;
	public GameObject candle;
	public GameObject memeter;
	public GameObject ibox;

	public GameObject videoButton;
	public GameObject facilitatorFrame;
	public GameObject basicphText;
	public GameObject basicph;
	public GameObject card;
	public GameObject bigibox;

	bool isCardChosen;

	// Use this for initialization
	void Start () {
		/*facilitator.SetActive (false);
		memeter.SetActive (false);
		ibox.SetActive (false);
		facilitatorFrame.SetActive (false);
		basicphText.SetActive (false);
		basicph.SetActive (false);
		card.SetActive (false);
		bigibox.SetActive (false);
		candle.SetActive (true);
		isCardChosen = false;
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
	/*
	public void candleToggle ()
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
			Application.LoadLevel("SessionThreeOneScene");
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
			StartCoroutine(VideoIntro(3));
		}
		else  {
			StartCoroutine(endFacilitatorExplaining(4));
		}
	}

	IEnumerator VideoIntro (float duration)
	{
		videoButton.SetActive (true);
		videoButton.GetComponent<Animator> ().Play ("video_button_show_intro");
		facilitator.GetComponent<Animator> ().enabled = false;
		facilitator.transform.localPosition = new Vector3 (-140,-130,-40);
		yield return new WaitForSeconds(duration);
		facilitator.transform.localPosition = new Vector3 (347, -482, -40);
		facilitatorFrame.SetActive (true);
		videoButton.GetComponent<Animator> ().Play ("video_button_slide_left_intro");
		yield return new WaitForSeconds(duration);
		videoButton.SetActive (false);
		StartCoroutine (BasicPHIntro (3));

	}

	IEnumerator BasicPHIntro (float duration)
	{
		facilitatorFrame.SetActive (false);
		facilitator.transform.localPosition = new Vector3 (-140,-130,-40);
		basicphText.SetActive (true);
		basicphText.GetComponent<Animator> ().Play ("basic_ph_slide_intro");
		yield return new WaitForSeconds(duration);
		basicphText.SetActive (false);
		facilitatorFrame.SetActive (true);
		facilitator.transform.localPosition = new Vector3 (347, -482, -40);
		basicph.SetActive (true);
		card.SetActive (true);
	}
	
	public void CardDrag()
	{
		// convert the touched screen position to 3D world position
		// Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position); // when using touch
		Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // when using mouse
		
		// get the current position of the paddle GameObject or whatever you want to move
		Vector3 originalPaddlePos = card.transform.position;
		
		// replace the y coordinate with the y of touched pos.
		originalPaddlePos.x = touchPos.x;
		originalPaddlePos.y = touchPos.y;
		
		// set the paddle position to the modified one
		card.transform.position = originalPaddlePos;
	}

	public void CardDrop()
	{
		ibox.SetActive (false);
		card.SetActive (false);
		basicph.transform.localPosition = new Vector3 (0, -65, 0);
		bigibox.SetActive (true);
	}

	public void InsertBasicPH(){
		basicph.SetActive (false);
		card.SetActive (true);
		card.transform.localPosition = new Vector3 (0, -190, 0);
		isCardChosen = true;
	}

	public void endMEMeter()
	{
		if (isCardChosen) {
			card.SetActive(false);
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
