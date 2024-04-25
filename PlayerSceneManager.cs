using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Keeps track of the player */

public class PlayerSceneManager : MonoBehaviour
{

	#region Singleton

	public static PlayerSceneManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	public GameObject player;

	public void KillPlayer()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}