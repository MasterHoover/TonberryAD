using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

public class _GameManager : MonoBehaviour
{
	public ControllableSprite prefab_gameAvatar;
	private static _GameManager instance;
	private _GameManagerDB db;
	public GameObject empty;
	public Texture2D black;
	private ControllableSprite gameAvatar;
	private CameraPanner camPanner;
	private int spawnIndex;

	public delegate void SceneWasSetupHandler (GameScene gameScene);
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

	public ScreenOverlay CreateCamOverlay ()
	{
		ScreenOverlay so = Camera.main.GetComponent<ScreenOverlay> ();
		if (so == null)
		{
			so = Camera.main.GetComponent<ScreenOverlay> ();
		}
		so.blendMode = ScreenOverlay.OverlayBlendMode.AlphaBlend;
		so.texture = black;
		so.intensity = 0f;
		return so;
	}

	public void LoadScene (string name)
	{
		sceneIsLoading = true;
		SceneManager.LoadScene (name);
	}

	public void LoadScene (string name, int spawnID)
	{
		this.spawnIndex = spawnID;
		LoadScene (name);
	}

	public void LoadSceneWithFading (string name, int index)
	{
		Cutscene cs = db.changeSceneWithFading;
		((Action_ChangeScene)cs.actions [1]).sceneName = name;
		LaunchCutscene (cs);
	}

	public Cutscene CreateCutscene (Action action)
	{
		Action[] actions = new Action[1];
		actions [0] = action;
		return CreateCutscene (actions);
	}

	public Cutscene CreateCutscene (Action[] actions)
	{
		GameObject empty = Instantiate<GameObject> (this.empty);
		Cutscene cs = empty.AddComponent<Cutscene> ();
		cs.actions = actions;
		return cs;
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
			db = GetComponent<_GameManagerDB> ();
			camPanner = GetComponent<CameraPanner> ();
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
			SceneWasSetup (e);
		}
	}

	public void LaunchCutscene (Cutscene c)
	{
		CutscenePlayer cp = gameObject.AddComponent<CutscenePlayer> ();
		Cutscene cClone = Instantiate<Cutscene> (c);
		cClone.transform.parent = transform;
		Action[] clones = new Action[c.actions.Length];
		for (int i = 0; i < c.actions.Length; i++)
		{
			clones [i] = Instantiate<Action> (c.actions [i]);
			clones [i].transform.parent = cClone.transform;
		}
		cClone.actions = clones;
		cp.Launch (cClone);
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

	public string LevelName
	{
		get{return SceneManager.GetActiveScene ().name;}
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
