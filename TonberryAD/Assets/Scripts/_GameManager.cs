using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class _GameManager : MonoBehaviour
{
	public ControllableSprite prefab_gameAvatar;
	private static _GameManager instance;
	private ControllableSprite gameAvatar;
	private CameraPanner camPanner;
	private int spawnIndex;
	private CinematicPlayer cinematicPlayer;
	private LevelStateMachine levelStateMachine;

	public delegate void LevelLoadedHandler (object source, System.EventArgs e);
	public event LevelLoadedHandler LevelLoaded;

	public delegate void LevelLoadingHandler (object source, System.EventArgs e);
	public event LevelLoadingHandler LevelLoading;

	void Awake ()
	{
		Initialize ();
	}

	void Start ()
	{
		InitialSetup ();
	}

	void OnLevelWasLoaded (int level)
	{
		FetchAvatar ();
		if (cinematicPlayer == null)
		{
			Debug.Log ("CinematicPlayer does not exist");
		}
		cinematicPlayer.FetchSceneCinematics ();
		levelStateMachine.ActivateAutomaticStates ();
		levelStateMachine.FetchAllAndPlayActiveStates ();
		OnLevelLoaded (new GameScene (SceneManager.GetActiveScene ().name, spawnIndex));
	}

	private void Initialize ()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
			camPanner = GetComponent<CameraPanner> ();
			cinematicPlayer = GetComponent<CinematicPlayer> ();
			levelStateMachine = GetComponent<LevelStateMachine> ();
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.KeypadMultiply))
		{
			cinematicPlayer.PlayCinematic (new TAction_PlayCinematic ("Complex"));
		}
		else if (Input.GetKeyDown (KeyCode.KeypadMinus))
		{
			cinematicPlayer.PlayCinematic (new TAction_PlayCinematic ("Complex"));
		}
	}

	private void InitialSetup ()
	{
		FetchAvatar ();
		cinematicPlayer.FetchSceneCinematics ();
		levelStateMachine.ActivateAutomaticStates ();
		levelStateMachine.FetchAllAndPlayActiveStates ();
		if (gameAvatar == null)
		{
			SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
			for (int i = 0; i < spawnPoints.Length; i++)
			{
				if (spawnPoints[i].id == 0)
				{
					SpawnAvatar (spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
					spawnPoints[i].cameraView.Activate ();
				}
			}
		}
	}

	public ControllableSprite SpawnAvatar (Vector3 position, Quaternion rotation)
	{
		if (gameAvatar != null)
		{
			Debug.LogWarning ("_GameManager[" + name + "]/SpawnAvatar (Vector3, Quaternion) : Spawning avatar, but one already exists.");
		}
		gameAvatar = (ControllableSprite) Instantiate (prefab_gameAvatar, position, rotation);
		return gameAvatar;
	}

	private void FetchAvatar ()
	{
		GameObject gObject = GameObject.FindGameObjectWithTag ("Player");
		if (gObject != null)
		{
		 	gameAvatar = gObject.GetComponent<ControllableSprite> ();
		}
	}

	public void LoadScene (string sceneName)
	{
		LoadScene (sceneName, 0, false);
	}

	public void LoadScene (string sceneName, int spawnIndex)
	{
		LoadScene (sceneName, spawnIndex, false);
	}

	public void LoadScene (string sceneName, int spawnIndex, bool fade)
	{
		this.spawnIndex = spawnIndex;
		Debug.Log ("Spawn index is now " + this.spawnIndex);
		if (!fade)
		{
			SceneManager.LoadScene (sceneName);
		}
		else
		{
			cinematicPlayer.PlayCinematic ("ChangeScene[" + sceneName + "] transition", ChangeSceneTransition (sceneName));
		}
	}

	private Cinematic ChangeSceneTransition (string sceneName)
	{
		return new Cinematic (new TAction[][] 
			{ new TAction[1] { new TAction_FadeIn () }, 
				new TAction[1] { new TAction_LoadScene (sceneName, spawnIndex) }, 
				new TAction[1] { new TAction_FadeOut ()}});
	}

	private SpawnPoint FindSpawnPoint (int spawnIndex)
	{
		SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
		Debug.Log ("SpawnPointLength : " + spawnPoints.Length);
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (spawnPoints[i].id == spawnIndex)
			{
				return spawnPoints[i];
			}
		}
		return null;
	}

	void OnLevelLoaded (GameScene e)
	{
		if (LevelLoaded != null)
		{
			LevelLoaded (this, e);
		}
	}

	void OnLevelLoading (GameScene e)
	{
		if (LevelLoading != null)
		{
			LevelLoading (this, e);
		}
	}

	public static _GameManager Instance
	{
		get{return instance;}
	}

	public ControllableSprite GameAvatar
	{
		get{return gameAvatar;}
	}

	public CameraPanner CameraPanner
	{
		get{return camPanner;}
	}

	public CinematicPlayer CinematicPlayer
	{
		get{return cinematicPlayer;}
	}

	public string LevelName
	{
		get{return SceneManager.GetActiveScene ().name;}
	}

	public LevelStateMachine LevelStateMachine
	{
		get{return levelStateMachine;}
	}

	public int SpawnIndex
	{
		get{return spawnIndex;}
	}
}
