using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour
{
	bool fullscreen = false;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1600, 900, false);
    }

    // Update is called once per frame
    void Update()
    {
		if(fullscreen == false)
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				Screen.SetResolution(1600, 900, true);
				fullscreen = true;
			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				Screen.SetResolution(1600, 900, false);
				fullscreen = false;
			}
		}
    }
}
