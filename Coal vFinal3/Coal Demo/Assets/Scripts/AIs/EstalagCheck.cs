using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstalagCheck : MonoBehaviour
{
	Animator anim;
	
	[SerializeField]
	GameObject player;
	public GameObject EstalagFall;
	
	[SerializeField]
	bool spawn = false;
	
	float distanceActivateX = 1, distanceActivateY = 6;
	float distanceX, distanceY;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
		
        StartCoroutine("Activate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	IEnumerator Activate()
	{
		while(true)
		{
			if(spawn == false)
			{
				distanceX = player.transform.position.x - transform.position.x;
				distanceY = player.transform.position.y - transform.position.y;
				
				if(Mathf.Abs(distanceX)<distanceActivateX && Mathf.Abs(distanceY)<distanceActivateY)
				{
					anim.SetTrigger("Used");
					Instantiate(EstalagFall, transform.position, Quaternion.identity);
					spawn = true;
				}
			}
			yield return new WaitForSeconds(0.3f);
		}
	}
}
