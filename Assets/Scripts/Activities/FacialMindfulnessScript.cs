using UnityEngine;
using System.Collections.Generic;

public class FacialMindfulnessScript : Activity {

    //logic control
    SessionManager.Gender userGender;
    //drawing control
    Texture2D selectedTexture;
    public Texture2D maleFace;
    public Texture2D femaleFace;
    public Texture2D noMask;
    public Texture2D redMask;
    public Texture2D blueMask;
    public float scale;
    Rect faceRect;
    public Rect headRect, eyesRect, leftCheekRect, rightCheekRect, mouthRect; 

    enum FacePositionState
    {
        Normal,
        Red, 
        Blue
    }

    enum FacePosition
    {
        Head,
        Eyes, 
        LeftCheek,
        RightCheek,
        Mouth
    }

    //face coordination
    Dictionary<FacePosition,FacePositionState> facePositions;

	// Use this for initialization
	public override void Start () {
        this.SensorManager.StartNewActivity();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        //draw the face
        GUI.DrawTexture(faceRect, selectedTexture);
        //draw the masks for the face positions
        if (GUIButtonTexture2D(headRect, GetMask(FacePosition.Head)))
            UpdateFacePositionState(FacePosition.Head);
        if (GUIButtonTexture2D(eyesRect, GetMask(FacePosition.Eyes)))
            UpdateFacePositionState(FacePosition.Eyes);
        if (GUIButtonTexture2D(leftCheekRect, GetMask(FacePosition.LeftCheek)))
            UpdateFacePositionState(FacePosition.LeftCheek);
        if (GUIButtonTexture2D(mouthRect, GetMask(FacePosition.Mouth)))
            UpdateFacePositionState(FacePosition.Mouth);
        if (GUIButtonTexture2D(rightCheekRect, GetMask(FacePosition.RightCheek)))
            UpdateFacePositionState(FacePosition.RightCheek);

    }

    void UpdateFacePositionState(FacePosition _toUpdate)
    {
        switch (facePositions[_toUpdate])
        {
            case FacePositionState.Normal:
                facePositions[_toUpdate] = FacePositionState.Red;
                break;
            case FacePositionState.Red:
                facePositions[_toUpdate] = FacePositionState.Blue;
                break;
            case FacePositionState.Blue:
                facePositions[_toUpdate] = FacePositionState.Normal;
                break;
            default:
                break;
        }

        if (!this.CanContinue)
        {
            this.CanContinue = true;
            UIManagerScript.EnableSkipping();
            SessionManager.PlayerScore += 10;
        }
    }

    Texture2D GetMask(FacePosition _toGet)
    {
        switch (facePositions[_toGet])
        {
            case FacePositionState.Normal:
                return noMask;
            case FacePositionState.Red:
                return redMask;
            case FacePositionState.Blue:
                return blueMask;
            default:
                return noMask;
        }
    }

    public void Setup(string description, SessionManager.Gender _selectedGender)
    {
        this.Name = description;
        this.CanContinue = false;
        userGender = _selectedGender;
        if (userGender == SessionManager.Gender.Male)
        {
            //face texture
            selectedTexture = maleFace;
        }
        else
        {
            //face texture
            selectedTexture = femaleFace;
        }

        float faceX = Screen.width / 2 - (selectedTexture.width * scale) / 2;
        float faceY = Screen.height / 2 - (selectedTexture.height * scale) / 2;

        faceRect = new Rect(
            faceX, 
            faceY, 
            selectedTexture.width * scale, 
            selectedTexture.height * scale);

        if (userGender == SessionManager.Gender.Male)
        {
            headRect = new Rect(faceX + 85 * scale, faceY + 148 * scale, 308 * scale, 124 * scale);
            eyesRect = new Rect(faceX + 85 * scale, faceY + 271 * scale, 308 * scale, 100 * scale);
            leftCheekRect = new Rect(faceX + 85 * scale, faceY + 371 * scale, 90 * scale, 171 * scale);
            mouthRect = new Rect(faceX + 176 * scale, faceY + 453 * scale, 125 * scale, 110 * scale);
            rightCheekRect = new Rect(faceX + 302 * scale, faceY + 371 * scale, 90 * scale, 171 * scale);
        }
        else
        {
            //parts of the face rectangles
            headRect = new Rect(faceX + 105 * scale, faceY + 207 * scale, 345 * scale, 124 * scale);
            eyesRect = new Rect(faceX + 105 * scale, faceY + 330 * scale, 345 * scale, 100 * scale);
            leftCheekRect = new Rect(faceX + 110 * scale, faceY + 431 * scale, 100 * scale, 125 * scale);
            mouthRect = new Rect(faceX + 210 * scale, faceY + 485 * scale, 134 * scale, 100 * scale);
            rightCheekRect = new Rect(faceX + 345 * scale, faceY + 431 * scale, 100 * scale, 125 * scale);
        }

        facePositions = new Dictionary<FacePosition,FacePositionState>() {
            {FacePosition.Head, FacePositionState.Normal},
            {FacePosition.Eyes, FacePositionState.Normal},
            {FacePosition.LeftCheek, FacePositionState.Normal},
            {FacePosition.Mouth, FacePositionState.Normal},
            {FacePosition.RightCheek, FacePositionState.Normal}
        };
    }

    bool GUIButtonTexture2D(Rect r, Texture2D t)
    {
        GUI.DrawTexture(r, t);
        return GUI.Button(r, "", GUIStyle.none);
    }

    public override void EndActivity()
    {
        foreach (var facePositionPair in facePositions)
        {
            Logger.Instance.LogInformation("FacePosition: " + facePositionPair.Key + " - " + facePositionPair.Value);
        }
        base.EndActivity();
    }
}
