using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TecelaAI : MonoBehaviour
{
	Animator anim;
	
	[SerializeField]
	GameObject player, poof;
	[SerializeField]
	PlayerControl PC;
	[SerializeField]
	HealthControl HC;
	
	[SerializeField]
	float swingTime = 0.125f;
	bool swingUp = false;
	int swingDirection = -1;
	int swingState = 0;
	
	int health = 2;
	
	[SerializeField]
	float distanceActivate = 10, distanceDeactivate = 13;
	bool active;
	float distanceX;
	float distanceY;
	float direction;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		
		StartCoroutine("Activate");
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true)
		{
			swingTime -= Time.deltaTime;
			if(swingTime<=0)
			{
				if(swingUp == false)
				{
					swingState++;
					swingDirection = -1;
				}
				else
				{
					swingState--;
					swingDirection = 1;
				}
				anim.SetInteger("SwingState", swingState);
				switch(swingState)
				{
					case 0:
						transform.Translate(0, swingDirection*0.2f, 0);
						swingUp = false;
						swingTime = 3;
						break;
					case 1:
						transform.Translate(0, swingDirection*0.2f, 0);
						swingTime = 0.05f;
						break;
					case 2:
						transform.Translate(0, swingDirection*0.2f, 0);
						swingTime = 0.05f;
						break;
					case 3:
						if(swingUp == false)
							transform.Translate(0, swingDirection*0.2f, 0);
						else
							transform.Translate(0, swingDirection*0.15f, 0);
						swingTime = 0.05f;
						break;
					case 4:
						if(swingUp == false)
							transform.Translate(0, swingDirection*0.15f, 0);
						else
							transform.Translate(0, swingDirection*0.2f, 0);
						swingTime = 0.05f;
						break;
					case 5:
						transform.Translate(0, swingDirection*0.2f, 0);
						swingTime = 0.05f;
						break;
					case 6:
						if(swingUp == false)
							transform.Translate(0, swingDirection*0.2f, 0);
						else
							transform.Translate(0, swingDirection*0.45f, 0);
						swingTime = 0.05f;
						break;
					case 7:
						transform.Translate(0, swingDirection*0.45f, 0);
						swingUp = true;
						swingTime = 3;
						break;
				}
			}
		}
		else
		{
			swingTime = 1;
		}
		
		if(health<=0)
		{
			Instantiate(poof, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && PC.damageTaken == false)
		{
			HC.damage = 4;
			PC.damageTaken = true;
			HC.RemoveLives();
		}
		
		if(collision.gameObject.tag == "PlayerFire")
		{
			health--;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			health--;
		}
		if(collision.gameObject.tag == "BluePFire")
		{
			health -= 2;
		}
	}
	
	IEnumerator Activate()
	{
		while(true)
		{
			distanceX = player.transform.position.x - transform.position.x;
			distanceY = player.transform.position.y - transform.position.y;
			
			if(Mathf.Abs(distanceX)<distanceActivate && Mathf.Abs(distanceY)<distanceActivate && active == false)
			{
				active = true;
			}
			else if(Mathf.Abs(distanceX)>distanceDeactivate && active == true || Mathf.Abs(distanceY)>distanceActivate && active == true)
			{
				active = false;
			}
			
			yield return new WaitForSeconds(0.3f);
		}
	}
}
