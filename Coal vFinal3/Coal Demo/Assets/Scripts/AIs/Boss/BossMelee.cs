using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour
{
	[SerializeField]
	float meleeTimer = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meleeTimer-=Time.deltaTime;
		if(meleeTimer<=0)
		{
			Destroy(gameObject);
		}
    }
}
