using UnityEngine;
using System.Collections;

public class BaseProfile {
	
	static BaseProfile _instance;
	public static BaseProfile instance {
		get {
			if(_instance == null)
				_instance = new BaseProfile();			
			return _instance;
		}
	}

	public int CurrentLevel {
		get	{			
			return PlayerPrefs.GetInt("CurrentLevel", 1);
		}
		set	{
			PlayerPrefs.SetInt("CurrentLevel", value);
			Save ();
		}
	}
  

    public void Save ()	{
		PlayerPrefs.Save ();

	}
}