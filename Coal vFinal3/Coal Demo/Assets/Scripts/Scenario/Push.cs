using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
	[SerializeField]
	GameObject player;
	[SerializeField]
	PlayerControl PC;
	
	float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		distance = player.transform.position.y - transform.position.y;
    }
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player" && Mathf.Abs(distance)<1)
			PC.pushing = true;
	}
	
	private void OnCollisionExit2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
			PC.pushing = false;
	}
}