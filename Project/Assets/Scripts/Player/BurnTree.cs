using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTree : MonoBehaviour {

    private BreathFire m_breath;

	// Use this for initialization
	void Start () {
        m_breath = transform.parent.GetComponent<BreathFire>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (m_breath.IsBreathingFire())
        {
            BurnableScript burnable = other.gameObject.GetComponent<BurnableScript>();
            if (!burnable.IsMaxFire())
            {
                burnable.AddFire(Time.deltaTime);
                burnable.Burning();
            }
        }
    }
}
