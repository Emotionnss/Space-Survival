using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static int lives, kills, spawned;
	public GameObject enemy, timeTxt, gameUI, menuUI, info;
	public Text scoreTxt, liveTxt, hstxt;

	private TextMesh timeMesh;
	private float timeUsed;
	private int killsRequired = 10;

	void Start()
	{
		StartPrefs();
	}


	void Update()
	{
		TextDisplay();
		Conditions();
		Timer(true);

		if (timeUsed >= 20)
		{
			StartCoroutine(timeBlink(true));
		}
	}

	#region Starting Preferences
	void StartPrefs() //Reset the values
	{
		Time.timeScale = 0;
		timeUsed = 0.0f;
		kills = 0;
		lives = 3;
		spawned = 0;
		menuUI.SetActive(true);
		gameUI.SetActive(false);
		info.SetActive(false);
		InvokeRepeating("E_Spawner", 0f, 1f);
		timeMesh = timeTxt.GetComponent<TextMesh>();
		timeMesh.color = Color.yellow;
	}
	#endregion

	#region Win and Lose Conditions
	void Conditions() //Win or Lose conditions
	{
		if (lives <= 0 || timeUsed >= 60)
		{
			Timer(false);
			SceneManager.LoadScene("GameOver");
		}
		else if (kills >= killsRequired)
		{
			Timer(false);
			PlayerPrefs.SetInt("curScore", Mathf.RoundToInt(timeUsed));
			Storehighscore(Mathf.RoundToInt(timeUsed));
			SceneManager.LoadScene("GameWin");
		}
	}
	#endregion

	void TextDisplay() //Displays all the texts on screen
	{
		scoreTxt.text = "Kills: " + kills + "/" + killsRequired;
		liveTxt.text = "Lives: " + lives;
		timeMesh.text = "" + Mathf.RoundToInt(timeUsed);
		hstxt.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
	}

	void E_Spawner() //Spawner settings
	{
		if (spawned < 3)
		{
			Instantiate(enemy, new Vector3(Random.Range(-2.7f, 2.7f), 5.4f), Quaternion.identity);
			spawned++;
		}
	}

	public void BGM(AudioClip music) //Changes the background music
	{
		Camera.main.GetComponent<AudioSource>().Stop();
		Camera.main.GetComponent<AudioSource>().clip = music;
		Camera.main.GetComponent<AudioSource>().Play();
	}

	public IEnumerator timeBlink(bool low) //Sets the timer color to blink
	{
		while (low == true)
		{
			timeMesh.color = Color.red;
			yield return new WaitForSeconds(1f);
			timeMesh.color = Color.yellow;
			yield return new WaitForSeconds(1f);
		}
	}

	void Timer(bool finish) //Pauses the timer when the game is over
	{
		if (finish == true)
		{
			timeUsed += Time.deltaTime;
		}
	}

	#region Saving HighScore
	void Storehighscore(int newHighScore) //Saving best time in highscore
	{
		int oldHighScore = PlayerPrefs.GetInt("highscore", 60);
		if (newHighScore < oldHighScore)
		{
			PlayerPrefs.SetInt("highscore", newHighScore);
		}
		PlayerPrefs.Save();
	}
	#endregion

	#region UI
	public void ButtonsUI(int selection) //Used for the buttons UI
	{
		switch (selection)
		{
			case 1: //When play buttons is pressed
				gameUI.SetActive(true);
				menuUI.SetActive(false);
				Time.timeScale = 1;
				E_Move.speed = 3f;
				E_Move.fireRate = 0.9f;
				break;
			case 2:
				info.SetActive(true);
				break;
			case 3:
				info.SetActive(false);
				break;
			case 4: //Quits the game
				System.Diagnostics.Process.GetCurrentProcess().Kill();
				break;
			case 5: //Clears highscore when title is clicked
				PlayerPrefs.DeleteAll();
				break;
			case 6: //Chevy mode when my named is pressed
				E_Move.speed = 5f;
				E_Move.fireRate = 0.1f;
				gameUI.SetActive(true);
				menuUI.SetActive(false);
				Time.timeScale = 1;
				break;
		}
	}
	#endregion
}
