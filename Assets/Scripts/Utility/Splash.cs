using UnityEngine;
using UnityEngine.SceneManagement;

// Website used: http://pixlab-games.tumblr.com/post/121686571742/how-to-make-a-custom-splash-screen-in-unity-5

public class Splash : MonoBehaviour
{
    private float _timer;
    public float Duration = 3f;

    void Start()
    {
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= Duration)
            SceneManager.LoadScene("Game");
    }
}