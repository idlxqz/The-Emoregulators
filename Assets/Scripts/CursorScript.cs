using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public AudioSource ClickSound;
    public Texture2D CursorTexture;


    void Awake()
    {
        DontDestroyOnLoad(this);
    }
	// Use this for initialization
	void Start () {
        Cursor.SetCursor(this.CursorTexture, Vector2.zero, CursorMode.Auto);
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.ClickSound.Play();
        }
    }
}
