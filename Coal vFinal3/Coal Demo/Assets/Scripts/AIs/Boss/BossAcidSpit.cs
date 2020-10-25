using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAcidSpit : MonoBehaviour
{
	Rigidbody2D rigid;
	
	[SerializeField]
	float deleteTimer = 6;
    // Start is called before the first frame update
    void Start()
    {
		rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        deleteTimer -= Time.deltaTime;
		if(deleteTimer<=0)
			Destroy(gameObject);
    }
	
	void FixedUpdate()
	{
		rigid.velocity = new Vector2(3, rigid.velocity.y);
	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player" && collision.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
	}
}
