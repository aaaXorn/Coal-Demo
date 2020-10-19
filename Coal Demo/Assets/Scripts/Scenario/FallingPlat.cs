using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlat : MonoBehaviour
{
	Animator anim;
	BoxCollider2D box;
	
	bool timerStart;
	[SerializeField]
	float destroyTimer = 0.8f, respawnTimer = 20;
	
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if(box.enabled && timerStart)
		{
			anim.SetTrigger("Destroy");
			destroyTimer -= Time.deltaTime;
			if(destroyTimer<=0)
			{
				anim.SetBool("Invis", true);
				respawnTimer = 20;
				timerStart = false;
				box.enabled = false;
			}
		}
		else if (box.enabled == false)
		{
			respawnTimer -= Time.deltaTime;
			if(respawnTimer<=0)
			{
				anim.SetBool("Invis", false);
				destroyTimer = 0.8f;
				box.enabled = true;
			}
		}
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
			timerStart = true;
	}
}
