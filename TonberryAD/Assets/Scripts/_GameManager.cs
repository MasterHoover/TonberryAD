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

	public delegate void SceneWasSetupHandler (object source, System.EventArgs e);
	public event SceneWasSetupHandler SceneWasSetup;

	private bool sceneIsLoading;

	void Awake ()
	{
		Initialize ();
	}

	void Start ()
	{
		SetupScene ();
	}

	void OnEnable ()
	{
		SceneManager.sceneLoaded += OnSceneWasLoaded;
	}

	void OnDisable ()
	{
		SceneManager.sceneLoaded -= OnSceneWasLoaded;	
	}

	void OnSceneWasLoaded (Scene scene, LoadSceneMode mode)
	{
		SetupScene ();
		sceneIsLoading = false;
		OnSceneWasSetup (new GameScene (SceneManager.GetActiveScene ().name, spawnIndex));
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

	private void SetupScene ()
	{
		gameAvatar = FetchAvatarInScene ();
		if (gameAvatar == null)
		{
			gameAvatar = SpawnAvatar ();
		}
		cinematicPlayer.FetchSceneCinematics ();
		levelStateMachine.ActivateAutomaticStates ();
		levelStateMachine.FetchAllAndPlayActiveStates ();
	}

	public ControllableSprite SpawnAvatar (int spawnIndex)
	{
		SpawnPoint spawnPoint = FetchSpawnPointInScene (spawnIndex);
		if (spawnPoint != null)
		{
			spawnPoint.cameraView.Activate ();
			return SpawnAvatar (spawnPoint.transform.position, spawnPoint.transform.rotation);
		}
		else
		{
			Debug.LogWarning ("_GameManager/SpawnAvatar (int): No spawnPoint of ID [" + spawnIndex + "] was found in the current scene.");
		}
		return null;
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

	public ControllableSprite SpawnAvatar ()
	{
		SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (spawnPoints[i].id == spawnIndex)
			{
				spawnPoints [i].cameraView.Activate ();
				return SpawnAvatar (spawnPoints [i].transform.position, spawnPoints [i].transform.rotation);
			}
		}
		return null;
	}

	private ControllableSprite FetchAvatarInScene ()
	{
		GameObject gObject = GameObject.FindGameObjectWithTag ("Player");
		if (gObject != null)
		{
		 	return gObject.GetComponent<ControllableSprite> ();
		}
		return null;
	}

	private SpawnPoint[] FetchSpawnPointsInScene ()
	{
		return GameObject.FindObjectsOfType<SpawnPoint> ();
	}

	private SpawnPoint FetchSpawnPointInScene (int spawnID)
	{
		SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint> ();
		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (spawnPoints [i].id == spawnID)
			{
				return spawnPoints [i];
			}
		}
		return null;
	}

	public void LoadScene (string sceneName)
	{
		LoadScene (sceneName, spawnIndex, false);
	}

	public void LoadScene (string sceneName, int spawnIndex)
	{
		LoadScene (sceneName, spawnIndex, false);
	}

	public void LoadScene (string sceneName, int spawnIndex, bool fade)
	{
		this.spawnIndex = spawnIndex;
		if (!fade)
		{
			sceneIsLoading = true;
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
				new TAction[1] { new TAction_LoadScene (sceneName) }, 
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

	void OnSceneWasSetup (GameScene e)
	{
		if (SceneWasSetup != null)
		{
			SceneWasSetup (this, e);
		}
	}

	/*
	void OnLevelLoading (GameScene e)
	{
		if (LevelLoading != null)
		{
			LevelLoading (this, e);
		}
	}
	*/

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

	public bool SceneIsLoading
	{
		get{ return sceneIsLoading; }
	}
}
