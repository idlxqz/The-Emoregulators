using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FacialMindfulnessScript : MonoBehaviour {

    //logic control
    SessionManager.Gender userGender;
    public bool finished;

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
	void Start () {
	    
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

        UIManagerScript.EnableSkipping();
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

    public void Setup(SessionManager.Gender _selectedGender)
    {
        finished = false;
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

        faceRect = new Rect(
            Screen.width / 2 - (selectedTexture.width * scale) / 2, 
            Screen.height / 2 - (selectedTexture.height * scale) / 2, 
            selectedTexture.width * scale, 
            selectedTexture.height * scale);

        if (userGender == SessionManager.Gender.Male)
        {
            //parts of the face rectangles
            headRect = new Rect(269, 225, 222, 72);
            eyesRect = new Rect(299, 299, 161, 47);
            leftCheekRect = new Rect(299, 352, 50, 85);
            mouthRect = new Rect(351, 391, 66, 54);
            rightCheekRect = new Rect(420, 350, 34, 85);
        }
        else
        {
            //parts of the face rectangles
            headRect = new Rect(267, 228, 233, 102);
            eyesRect = new Rect(300, 334, 172, 42);
            leftCheekRect = new Rect(300, 379, 48, 68);
            mouthRect = new Rect(353, 409, 64, 46);
            rightCheekRect = new Rect(420, 382, 47, 64);
        }

        facePositions = new Dictionary<FacePosition,FacePositionState>() {
            {FacePosition.Head, FacePositionState.Red},
            {FacePosition.Eyes, FacePositionState.Red},
            {FacePosition.LeftCheek, FacePositionState.Red},
            {FacePosition.Mouth, FacePositionState.Red},
            {FacePosition.RightCheek, FacePositionState.Red}
        };
    }

    bool GUIButtonTexture2D(Rect r, Texture2D t)
    {
        GUI.DrawTexture(r, t);
        return GUI.Button(r, "", GUIStyle.none);
    }
}
