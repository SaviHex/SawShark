using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    private bool isLoading = false;
    
    [SerializeField]    
    private Animator canvasAnim;
    [SerializeField]
    private TextMeshProUGUI progressText;
    [SerializeField]
    private TextMeshProUGUI messageText;

    public void LoadScene(int sceneId)
    {
        if (!isLoading)
        {
            isLoading = true;            
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvasAnim.gameObject);
            StartCoroutine(LoadSceneAsync(sceneId));
        }

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("App quiting...");
    }

    public void DebugMessage(string message)
    {
        Debug.Log(message);
    }

    private IEnumerator LoadSceneAsync(int sceneId)
    {
        canvasAnim.SetTrigger("EnterScene");
        yield return new WaitForSeconds(canvasAnim.GetCurrentAnimatorStateInfo(0).length);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneId);

        while (!async.isDone)
        {
            progressText.text = ((async.progress + 0.1f) * 100).ToString();
            yield return new WaitForEndOfFrame();
        }

        canvasAnim.SetTrigger("LeaveScene");
    }

    
}
