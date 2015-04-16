using UnityEngine;
using System.Collections;
using System;

public class UserData {

    public Guid UserID { get; set; }
    public string UserName { get; set; }
    public int Score { get; set; }
    public int MeMeterLevel { get; set; }
	
    //TODO: gather all the data that should be tracked and saved to be loaded between sessions
}
