using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerPoolBack : MonoBehaviour {
    public float Seconds = 3;
	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(Seconds);
        ObjectPooler.PoolBack(gameObject);
	}

    private void OnEnable()
    {
        StartCoroutine(Start());
    }

}
