using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpiderAI : MonoBehaviour
{
	Animator anim;
	SpriteRenderer render;
	Rigidbody2D rigid;
	
	[SerializeField]
	GameObject player, poof;
	[SerializeField]
	PlayerControl PC;
	[SerializeField]
	HealthControl HC;
	
	public int speedX = 4;
	float currentTime = 0;
	[SerializeField]
	float jumpTime = 1.5f;
	bool jump;
	bool action;
	
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
		render = GetComponent<SpriteRenderer>();
		rigid = GetComponent<Rigidbody2D>();
		
		StartCoroutine("Activate");
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
		{
			Instantiate(poof, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
	{
		if(jump && active)
		{
			currentTime += Time.deltaTime;
			if(currentTime>jumpTime)
			{
				action = true;
				currentTime = 0;
			}
		}
		else if(active == false)
		{
			currentTime = 0;
			action = false;
		}
		
		if(action)
		{
			if(jump == true)
			{
				if(distanceX != 0)
				{
					direction = distanceX / Mathf.Abs(distanceX);
				}
				
				if(direction<0)
				{
					render.flipX = true;
				}
				else
				{
					render.flipX = false;
				}
				anim.SetBool("jumping", true);
				rigid.AddForce(new Vector2(0, 350));
				jump = false;
			}
			rigid.velocity = new Vector2(direction*speedX, rigid.velocity.y);
		}
		else if(action == false || active == false)
		{
			rigid.velocity = new Vector2(0, rigid.velocity.y);
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "JumpReset")
		{
			anim.SetBool("jumping", false);
			jump = true;
			action = false;
		}
		
		if (collision.gameObject.tag == "Player" && PC.damageTaken == false)
		{
			HC.damage = 3;
			PC.damageTaken = true;
			HC.RemoveLives();
		}
		
		if(collision.gameObject.tag == "PlayerFire")
		{
			health--;
		}
		if(collision.gameObject.tag == "BluePFire")
		{
			health -= 2;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			health--;
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
