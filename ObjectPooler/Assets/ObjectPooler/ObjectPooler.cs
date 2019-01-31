using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolInfo
{
    public GameObject GO;
    public int instanceCount=3;
    public List<GameObject> GO_Instances = new List<GameObject>();
}

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler m_instance;
    public static ObjectPooler instance {
        get {
            if (!m_instance){
                m_instance = FindObjectOfType<ObjectPooler>();
            }
            if (!m_instance) {
                m_instance = Instantiate((GameObject)Resources.Load("ObjectPooler")).GetComponent<ObjectPooler>();
            }
            return m_instance;
        }
    }

    public List<ObjectPoolInfo> Prefabs = new List<ObjectPoolInfo>();
    List<ObjectPoolInfo> PooledObjects = new List<ObjectPoolInfo>();
    public int defaultCount = 3;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < Prefabs.Count; i++)
        {
            if (Prefabs[i].instanceCount > 0)
            {
                ObjectPoolInfo obj = new ObjectPoolInfo();
                for (int j = 0; j < Prefabs[i].instanceCount; j++)
                {
                    GameObject go = Instantiate(Prefabs[i].GO);
                    go.name = Prefabs[i].GO.name;
                    obj.GO_Instances.Add(go);
                    obj.GO = go;
                    obj.instanceCount = Prefabs[i].instanceCount;
                    go.transform.SetParent(transform);
                    go.SetActive(false);
                }
                PooledObjects.Add(obj);
            }
            else
            {
                ObjectPoolInfo obj = new ObjectPoolInfo();
                for (int j = 0; j < defaultCount; j++)
                {
                    GameObject go = Instantiate(Prefabs[i].GO);
                    go.name = Prefabs[i].GO.name;
                    obj.GO_Instances.Add(go);
                    obj.GO = go;
                    obj.instanceCount = defaultCount;
                    go.transform.SetParent(transform);
                    go.SetActive(false);
                }
                PooledObjects.Add(obj);
            }
        }
    }

    List<GameObject> ObjectsOutside = new List<GameObject>();

    public static GameObject GrabObject(string Name, Vector3 Position, Quaternion Rotation) {
        GameObject go = null;
        for (int i = 0; i < instance.PooledObjects.Count; i++) {
            if (Name == instance.PooledObjects[i].GO.name)
            {
                for (int j = 0; j < instance.PooledObjects[i].GO_Instances.Count; j++)
                {
                    go = instance.PooledObjects[i].GO_Instances[j];
                    go.transform.SetParent(null);
                    go.transform.position = Position;
                    go.transform.rotation = Rotation;
                    go.SetActive(true);
                    instance.ObjectsOutside.Add(go);
                    instance.PooledObjects[i].GO_Instances.RemoveAt(j);
                    break;
                }
            }
        }
        return go;
    }

	public static GameObject GrabObject(int Index, Vector3 Position, Quaternion Rotation) {
		GameObject go = null;
		for (int i = 0; i < instance.PooledObjects.Count; i++) {
			if (Index == i)
			{
				for (int j = 0; j < instance.PooledObjects[i].GO_Instances.Count; j++)
				{
					go = instance.PooledObjects[i].GO_Instances[j];
					go.transform.SetParent(null);
					go.transform.position = Position;
					go.transform.rotation = Rotation;
					go.SetActive(true);
					instance.ObjectsOutside.Add(go);
					instance.PooledObjects[i].GO_Instances.RemoveAt(j);
					break;
				}
			}
		}
		return go;
	}

    public static void PoolBack(GameObject go)
    {
        for (int i = 0; i < instance.PooledObjects.Count; i++)
        {
            if (go.name == instance.PooledObjects[i].GO.name)
            {
                go.transform.SetParent(instance.transform);
                go.SetActive(false);
                instance.PooledObjects[i].GO_Instances.Add(go);
                if (instance.PooledObjects[i].GO_Instances.Count >= instance.PooledObjects[i].instanceCount && instance.ObjectsOutside.Count>0) {
                    for (int j = 0; j < instance.ObjectsOutside.Count; j++) {
                        if (instance.ObjectsOutside[j] != null)
                        {
                            if (go.name == instance.ObjectsOutside[j].name)
                            {
                                instance.ObjectsOutside.RemoveAt(j);
                            }
                        }
                    }
                }
            }
        }
    }

}
