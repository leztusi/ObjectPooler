using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerExample : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			ObjectPooler.GrabObject("failed", Random.insideUnitSphere*5, Quaternion.identity);
		}
	}

	private IEnumerator Start()
	{
		while(true){
			int rnd = Random.Range(0, ObjectPooler.instance.Prefabs.Count);
			ObjectPooler.GrabObject(rnd, Random.insideUnitSphere * 5, Quaternion.identity);
			yield return new WaitForSeconds(.5f);
		}
	}

}
