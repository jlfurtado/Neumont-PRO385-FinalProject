using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RingDetection : MonoBehaviour {

    [SerializeField] private UnityEvent m_callback;

    private void OnTriggerEnter(Collider other)
    {
        m_callback.Invoke();
        MeshCollider[] meshColliders = gameObject.GetComponents<MeshCollider>();
        for (int k = 0; k < meshColliders.Length; k++)
        {
            if (meshColliders[k].isTrigger)
            {
                meshColliders[k].enabled = false;
            }
        }
    }

    public void TestCallback()
    {
        Debug.Log("Hello Callback");
    }
}
