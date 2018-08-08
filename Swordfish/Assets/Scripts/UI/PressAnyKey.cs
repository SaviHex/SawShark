using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    public string sceneName;
    public float delay = 1.0f;

	// Update is called once per frame
	void Update ()
    {
        delay -= Time.deltaTime;

        if (delay <= 0f && Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneName);
        }
	}
}
