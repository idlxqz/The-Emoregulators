using UnityEngine;

public class ProgressiveMuscleRelaxationScript : CustomTextScript
{

    //public bool finished;
    public GameObject Avatar;
    protected Animator Animator;
    public int AnimationId;
    private bool ExpectedMuscleTense;
    private bool ExpectedMuscleRelaxed;
    public Texture2D GrassBackground;
    public Texture2D SnailBackground;
    public Texture2D SunBackground;
    public Texture2D SandBackground;
    private Sprite SelectedSprite;


    // Use this for initialization
    public override void Start()
    {
        this.ExpectedMuscleTense = false;
        this.ExpectedMuscleRelaxed = false;
        this.Avatar.SetActive(true);
        this.Avatar.GetComponent<RectTransform>().anchoredPosition = new Vector3(400, -300, -50);
        this.Animator = this.Avatar.GetComponentInChildren<Animator>();
        

        //UIManagerScript.EnableSkipping();
    }

    public override void Update()
    {
        if (this.ExpectedMuscleTense && SessionManager.IsMuscleActive)
        {
            this.ExpectedMuscleTense = false;
            //after the muscle being tense, we expected to become relaxed
            this.ExpectedMuscleRelaxed = true;
            SessionManager.PlayerScore += 2;
        }
        else if (this.ExpectedMuscleRelaxed && !SessionManager.IsMuscleActive)
        {
            this.ExpectedMuscleRelaxed = false;
            SessionManager.PlayerScore += 2;
        }

        //check if the waiting time is elapsed
        if (moreInstructions)
        {
            if ((Time.time - timeStart) >= delayBetweenInstructions)
            {
                timeStart = Time.time;
                instructionsPointer++;
                currentInstructions += "\n\n" + instructions[instructionsPointer];

                if (instructionsPointer == 1 || instructionsPointer == 4)
                {
                    this.Animator.SetTrigger(this.AnimationId);
                    this.ExpectedMuscleTense = true;
                }
                //check if there are more to show
                if (instructions.Length == instructionsPointer + 1)
                {
                    moreInstructions = false;
                    //let the user skip from now on
                    UIManagerScript.EnableSkipping();
                    this.OnFinish();
                }
            }
        }
    }

    public override void Setup(System.Action nextPhaseSetup, string[] newInstructions)
    {
        base.Setup(nextPhaseSetup, newInstructions);
        this.ExpectedMuscleRelaxed = false;
        this.ExpectedMuscleTense = false;
    }

    public void SetBackground(Texture2D backgroundTexture)
    {
        var cameraObject = GameObject.Find("Main Camera");
        if (cameraObject != null)
        {
            SpriteRenderer sr = cameraObject.GetComponentInChildren<SpriteRenderer>();

            if (this.SelectedSprite == null)
            {
                //the first time this method is called, it should remember the user's selected sprite for the background
                this.SelectedSprite = sr.sprite;
            }
            
            sr.sprite = Sprite.Create(backgroundTexture, new Rect(0, 0, backgroundTexture.width, backgroundTexture.height), new Vector2(0.5f, 0.5f));
        }
    }

    public void RevertToSessionBackground()
    {
        var cameraObject = GameObject.Find("Main Camera");
        if (cameraObject != null)
        {
            SpriteRenderer sr = cameraObject.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = this.SelectedSprite;
        }
    }
}
