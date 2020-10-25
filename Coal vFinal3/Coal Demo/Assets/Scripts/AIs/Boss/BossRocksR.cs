using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocksR : MonoBehaviour
{
	[SerializeField]
	float deleteTimer = 0.075f;
	
	[SerializeField]
	GameObject Rock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deleteTimer -= Time.deltaTime;
		if(deleteTimer<=0)
		{
			deleteTimer = 0.075f;
			Instantiate(Rock, transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
			Destroy(gameObject);
		}
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
	}
}
