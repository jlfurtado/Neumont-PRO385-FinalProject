using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFuncs
{
    private static System.Random rand = new System.Random();

    public static GameObject MakeAt(GameObject prefab, Vector3 position, float scale, GameObject parent, string name)
    {
        GameObject created = GameObject.Instantiate(prefab);
        created.transform.SetParent(parent.transform);
        created.transform.position = Vector3.zero;
        created.transform.localPosition = position;
        created.transform.localScale = Vector3.one * scale;
        created.name = name;
        return created;
    }

    public static Vector3 RandVec(Vector3 min, Vector3 max)
    {
        return new Vector3(((float)rand.NextDouble()) * (max.x - min.x) + min.x,
                           ((float)rand.NextDouble()) * (max.y - min.y) + min.y,
                           ((float)rand.NextDouble()) * (max.z - min.z) + min.z);
    }
}