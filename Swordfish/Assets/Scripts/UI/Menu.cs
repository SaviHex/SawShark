using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("App quiting");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    public void MenuSlideUp()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("KeyPressed");
    }

    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        var anim = GetComponent<Animator>();
        anim.SetTrigger("LeaveScene");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
        anim.SetTrigger("EnterScene");
    }
}
