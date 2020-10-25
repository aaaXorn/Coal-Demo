using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
	Animator anim;
	
	float timer = 0.5f;
	float currentTime;
	[SerializeField]
	float meleeTime = 1;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime+=Time.deltaTime;
		if(currentTime>meleeTime)
		{
			Destroy(gameObject);
		}
		
		if(timer<=0)
			anim.SetTrigger("Stop");
		else
			timer -= Time.deltaTime;
    }
}
