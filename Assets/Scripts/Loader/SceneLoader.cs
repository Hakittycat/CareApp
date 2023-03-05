using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [Header("Resources")]
    public Animator animator;

    public CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup.alpha = 1;
    }

    public void LoadScene(string scene) {
        StartCoroutine(StartLoading(scene));
    }

    private IEnumerator StartLoading(string scene) {
        animator.Play("move_in");
        yield return new WaitForSeconds(1.5f);
        //var operation = SceneManager.LoadSceneAsync(scene);
        SceneManager.LoadScene(scene);
        //while (!operation.isDone) {
        //    yield return new WaitForSeconds(0.1f);
        //}
    }
}