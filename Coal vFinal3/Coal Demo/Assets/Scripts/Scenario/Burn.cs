using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
	Animator anim;
	
	bool burning = false;
	[SerializeField]
	float burningTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(burning)
		{
			anim.SetTrigger("Burn");
			burningTime -= Time.deltaTime;
			if(burningTime<=0)
				Destroy(gameObject);
		}
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "PlayerFire")
		{
			burning = true;
		}
	}
}
