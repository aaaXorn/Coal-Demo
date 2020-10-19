using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plat : MonoBehaviour
{
	PlatformEffector2D plat;
	[SerializeField]
	float timer = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        plat = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKey(KeyCode.S))
		{
			timer = 0.5f;
			plat.rotationalOffset = 180;
		}
		
		if(timer<=0)
			plat.rotationalOffset = 0;
		else
			timer -= Time.deltaTime;
    }
}
