using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoad = 1f;  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);    
            UIFade.Instance.FadeToBlack();
            StartCoroutine(WaitToLoadScence()); 
        }
    }

    private IEnumerator WaitToLoadScence()
    {
       while(waitToLoad > 0)
        {
            waitToLoad -= Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
