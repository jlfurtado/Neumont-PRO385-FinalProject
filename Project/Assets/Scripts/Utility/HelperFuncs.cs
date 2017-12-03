using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFuncs
{
    private static System.Random rand = new System.Random();

    public static GameObject MakeAt(GameObject prefab, Vector3 position, float scale, GameObject parent, string name)
    {
        return PutAt(GameObject.Instantiate(prefab), position, scale, parent, name);
    }

    public static GameObject PutAt(GameObject obj, Vector3 position, float scale, GameObject parent, string name)
    {
        obj.transform.SetParent(parent.transform);
        obj.transform.position = Vector3.zero;
        obj.transform.localPosition = position;
        obj.transform.localScale = Vector3.one * scale;
        obj.name = name;
        return obj;
    }

    public static Vector3 RandVec(Vector3 min, Vector3 max)
    {
        return new Vector3(((float)rand.NextDouble()) * (max.x - min.x) + min.x,
                           ((float)rand.NextDouble()) * (max.y - min.y) + min.y,
                           ((float)rand.NextDouble()) * (max.z - min.z) + min.z);
    }
}