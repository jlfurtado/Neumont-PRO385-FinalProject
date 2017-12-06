using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTree : MonoBehaviour {
    [SerializeField] private float m_fireShakeIntensity = 1.0f;
    [SerializeField] private float m_shakeDuration = 1.0f;

    private BreathFire m_breath;
    private FollowCenter m_camera;

	// Use this for initialization
	void Start () {
        m_camera = Camera.main.GetComponent<FollowCenter>();
        m_breath = transform.parent.GetComponent<BreathFire>();
	}

    private void OnTriggerStay(Collider other)
    {
        if (m_breath.IsBreathingFire())
        {
            m_camera.LowPriorityShake(m_shakeDuration, m_fireShakeIntensity);
            BurnableScript burnable = other.gameObject.GetComponent<BurnableScript>();
            if (!burnable.IsMaxFire())
            {
                burnable.AddFire(Time.deltaTime);
                burnable.Burning();
            }
        }
    }
}
