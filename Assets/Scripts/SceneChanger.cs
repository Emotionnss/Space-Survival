using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public Text curScore;
	void Start()
	{
		if (curScore != null)
		{
			curScore.text = "Time Taken: " + PlayerPrefs.GetInt("curScore"); //Displays current score
		}
	}

	public void Change()
	{
		SceneManager.LoadScene("GameScene"); //Loads back home scene
	}
}
