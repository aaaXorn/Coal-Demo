using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
	public void Part1()
	{
		SceneManager.LoadScene("Scene1");
	}
	
	public void Part2()
	{
		SceneManager.LoadScene("Scene2");
	}
	
	public void Exit()
	{
		Application.Quit();
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
