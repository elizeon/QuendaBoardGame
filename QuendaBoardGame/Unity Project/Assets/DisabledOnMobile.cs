using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisabledOnMobile : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if(Application.isMobilePlatform)
        {
            Destroy(this.gameObject);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
