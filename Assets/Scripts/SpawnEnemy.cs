using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
	
	public GameObject EnemyPrefab;
	public float spawnTimer;
	public float spawnTimerSetting;
	// Use this for initialization
	void Start () {
		Instantiate(EnemyPrefab,transform.position,Quaternion.identity);
		spawnTimer = spawnTimerSetting;
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnTimer <= 0)
		{
		Instantiate(EnemyPrefab,transform.position,Quaternion.identity);
		spawnTimer = spawnTimerSetting;
			spawnTimerSetting= spawnTimerSetting-0.20f;
		}
		else{spawnTimer= (spawnTimer-(Time.fixedDeltaTime * 1));}
	
	}
}
