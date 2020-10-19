using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
	float currentTime;
	[SerializeField]
	float meleeTime = 1;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        currentTime+=Time.deltaTime;
		if(currentTime>meleeTime)
		{
			Destroy(gameObject);
		}
    }
}
