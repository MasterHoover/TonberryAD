using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour 
{
	public int id;
	public View cameraView;

	/*
	void Awake ()
	{
		_GameManager.Instance.LevelLoaded += OnLevelLoaded;
	}
	*/

	/*
	void OnDestroy ()
	{
		if (_GameManager.Instance != null)
		{
			_GameManager.Instance.LevelLoaded -= OnLevelLoaded;
		}
	}
	*/

	/*
	private void OnLevelLoaded (object source, System.EventArgs e)
	{
		Debug.Log ("[" + name + "]: This id is [" + this.id + "]; spawnIndex is [" + _GameManager.Instance.SpawnIndex + "]."); 
		if (this.id == _GameManager.Instance.SpawnIndex)
		{
			Debug.Log ("[" + name + "]: Spawning"); 
			if (_GameManager.Instance.GameAvatar == null)
			{
				_GameManager.Instance.SpawnAvatar (transform.position, transform.rotation);
			}
			_GameManager.Instance.GameAvatar.ChangeRotation (new Vector2 (transform.forward.x, transform.forward.z));
			cameraView.Activate ();
		}
	}
	*/

	public void SpawnAvatar ()
	{
		_GameManager.Instance.SpawnAvatar (transform.position, transform.rotation);
		_GameManager.Instance.GameAvatar.ChangeRotation (new Vector2 (transform.forward.x, transform.forward.z));
		cameraView.Activate ();
	}
}
