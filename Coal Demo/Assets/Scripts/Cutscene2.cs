using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene2 : MonoBehaviour
{
	float timer = 18;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
		if(timer<=0)
			SceneManager.LoadScene("Scene1.5");
		
		if(Input.GetKeyDown(KeyCode.Escape))
			SceneManager.LoadScene("Menu");
    }
}
