using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
	Animator anim;
	
	float timer = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(timer<=0)
			anim.SetTrigger("Stop");
		else
			timer -= Time.deltaTime;
    }
}
