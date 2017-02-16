using UnityEngine;
using UnityEngine.SceneManagement;

// Website used: http://pixlab-games.tumblr.com/post/121686571742/how-to-make-a-custom-splash-screen-in-unity-5

public class Splash : MonoBehaviour
{
    private float _timer;

    public string NextScene;
    public float Duration = 3f;


    private void Start()
    {
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        // Load scene after splash timer has ended
        if (_timer >= Duration)
            SceneManager.LoadScene(NextScene);
    }
}