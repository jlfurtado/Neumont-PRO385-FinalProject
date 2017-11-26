using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RingDetection : MonoBehaviour {

    //TODO: get the win/lose thing and cache it

    private void OnTriggerEnter(Collider other)
    {
        //TODO: Call a method to notify win/lose of ring death
        GetComponentInChildren<RingParticle>().PlayEffect();
        transform.parent.parent.gameObject.SetActive(false);
    }
}
