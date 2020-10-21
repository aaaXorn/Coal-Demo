using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
	Animator anim;
	SpriteRenderer render;
	Rigidbody2D rigid;
	
	[SerializeField]
	GameObject player;
	[SerializeField]
	PlayerControl PC;
	[SerializeField]
	HealthControl HC;
	
	[SerializeField]
	int health = 40;
	[SerializeField]
	float deathTimer = 2;
	
	bool attacking;
	bool attackUsed = false;
	[SerializeField]
	float attackT = 0, idleTime = 4;
	int attackType = 0;
	public int randomAttack;
	
	public bool hitByBoulder = false;
	
	[SerializeField]
	GameObject Melee, AcidSpitR, AcidSpitL, RockProjR, RockProjL, AcidProjR, AcidProjL;
	
	[SerializeField]
	float distanceActivate = 11, distanceDeactivate = 14;
	bool active;
	[SerializeField]
	float distanceX, distanceY;
	[SerializeField]
	bool melee = false, onPlat = false;
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
			anim.SetTrigger("Death");
			deathTimer -= Time.deltaTime;
			if(deathTimer<=0)
			{
				if(PC.mendel == false)
					SceneManager.LoadScene("Scene2");
				else
					Destroy(gameObject);
			}
		}
		else
		{
			if(active == true && attacking == false)
			{
				randomAttack = Random.Range (0, 2);//gera aleatóriamente um 0 ou um 1
				
				if(melee == true && onPlat == false)
				{
					if(randomAttack == 0)
						attackType = 1;
					else
						attackType = 3;
				}
				else if(melee == true && onPlat == true)
				{
					if(randomAttack == 0)
						attackType = 2;
					else
						attackType = 3;
				}
				else if(melee == false && onPlat == false)
				{
					if(randomAttack == 0)
						attackType = 4;
					else
						attackType = 5;
				}
				else if(melee == false && onPlat == true)
				{
					attackType = 6;
				}
			}
			
			switch(attackType)
			{
				case 1:
					MeleeLow();
					break;
				case 2:
					MeleeHigh();
					break;
				case 3:
					MeleeBoth();
					break;
				case 4:
					RockProjectile();
					break;
				case 5:
					AcidSpit();
					break;
				case 6:
					AcidProjectile();
					break;
				default:
					break;
			}
		}
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "PlayerFire")
		{
			health -= 2;
		}
		if(collision.gameObject.tag == "BluePFire")
		{
			health -= 4;
		}
		if (collision.gameObject.tag == "Push")
		{
			hitByBoulder = true;
			health -= 10;
			Destroy(collision.gameObject);
		}
	}
	
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && PC.damageTaken == false)
		{
			HC.damage = 6;
			PC.damageTaken = true;
			HC.RemoveLives();
		}
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "PlayerAttack")
		{
			health -= 2;
		}
	}
	
	private void AttackTimer()
	{
		attacking = true;
		
		if(attackUsed == false)
		{
			anim.SetBool("Attack", true);
			attackT += Time.deltaTime;
		}
		idleTime -= Time.deltaTime;
		if(attackUsed == true)
		{
			anim.SetBool("Attack", false);
			attackT = 0;
		}
		if(idleTime<=0)
		{
			attacking = false;
			attackUsed = false;
			idleTime = 4;
		}
	}
	
	private void MeleeLow()
	{
		AttackTimer();
		anim.SetTrigger("MeleeDown");
		if(render.flipX == false && attackT >= 0.5f)
		{
			attackUsed = true;
			Instantiate(Melee, transform.position + new Vector3(2.9f, -1, 0), Quaternion.identity);
		}
		else if(render.flipX == true && attackT >= 0.5f)
		{
			attackUsed = true;
			Instantiate(Melee, transform.position + new Vector3(-2.9f, -1, 0), Quaternion.identity);
		}
	}
	
	private void MeleeHigh()
	{
		AttackTimer();
		anim.SetTrigger("MeleeUp");
		if(render.flipX == false && attackT >= 0.5f)
		{
			attackUsed = true;
			Instantiate(Melee, transform.position + new Vector3(2.9f, 0.3f, 0), Quaternion.identity);
		}
		else if(render.flipX == true && attackT >= 0.5f)
		{
			attackUsed = true;
			Instantiate(Melee, transform.position + new Vector3(-2.9f, 0.3f, 0), Quaternion.identity);
		}
	}
	
	private void MeleeBoth()
	{
		AttackTimer();
		anim.SetTrigger("MeleeBoth");
		if(render.flipX == false && attackT >= 0.5f)
		{
			attackUsed = true;
			Instantiate(Melee, transform.position + new Vector3(2.9f, 0.3f, 0), Quaternion.identity);
			Instantiate(Melee, transform.position + new Vector3(2.9f, -1, 0), Quaternion.identity);
		}
		else if(render.flipX == true && attackT >= 0.5f)
		{
			attackUsed = true;
			Instantiate(Melee, transform.position + new Vector3(-2.9f, 0.3f, 0), Quaternion.identity);
			Instantiate(Melee, transform.position + new Vector3(-2.9f, -1, 0), Quaternion.identity);
		}
	}
	
	private void AcidSpit()
	{
		AttackTimer();
		anim.SetTrigger("AcidSpit");
		if(render.flipX == false && attackT >= 1)
		{
			attackUsed = true;
			Instantiate(AcidSpitR, transform.position + new Vector3(2.5f, -0.8f, 0), Quaternion.identity);
		}
		else if(render.flipX == true && attackT >= 1)
		{
			attackUsed = true;
			Instantiate(AcidSpitL, transform.position + new Vector3(-2.5f, -0.8f, 0), Quaternion.identity);
		}
	}
	
	private void AcidProjectile()
	{
		AttackTimer();
		anim.SetTrigger("AcidProj");
		if(render.flipX == false && attackT>=1)
		{
			attackUsed = true;
			Instantiate(AcidProjR, transform.position + new Vector3(0.75f, 1, 0), Quaternion.identity);
		}
		else if(render.flipX == true && attackT >= 1)
		{
			attackUsed = true;
			Instantiate(AcidProjL, transform.position + new Vector3(-0.75f, 1, 0), Quaternion.identity);
		}
		
	}
	
	private void RockProjectile()
	{
		AttackTimer();
		anim.SetTrigger("RockProj");
		if(render.flipX == false && attackT >= 1)
		{
			attackUsed = true;
			Instantiate(RockProjR, transform.position + new Vector3(2.5f, -1.2f, 0), Quaternion.identity);
		}
		else if(render.flipX == true && attackT >= 1)
		{
			attackUsed = true;
			Instantiate(RockProjL, transform.position + new Vector3(-2.5f, -1.2f, 0), Quaternion.identity);
		}
	}
	
	IEnumerator Activate()
	{
		while(true)
		{
			distanceX = player.transform.position.x - transform.position.x;
			distanceY = player.transform.position.y - transform.position.y;
			
			if(distanceX<0)
				render.flipX = true;
			else
				render.flipX = false;
			
			if(Mathf.Abs(distanceX)<distanceActivate && Mathf.Abs(distanceY)<distanceActivate && active == false)
			{
				active = true;
			}
			else if(Mathf.Abs(distanceX)>distanceDeactivate && active == true || Mathf.Abs(distanceY)>distanceActivate && active == true)
			{
				active = false;
			}
			
			if(Mathf.Abs(distanceX)>4.2f)
				melee = false;
			else
				melee = true;
			
			if(distanceY>1)
				onPlat = true;
			else
				onPlat = false;
			
			yield return new WaitForSeconds(0.3f);
		}
	}
}
