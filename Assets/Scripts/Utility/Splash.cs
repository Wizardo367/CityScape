using UnityEngine;
using UnityEngine.SceneManagement;

// Website used: http://pixlab-games.tumblr.com/post/121686571742/how-to-make-a-custom-splash-screen-in-unity-5

/// <summary>
/// Used to define an image as a splash screen.
/// </summary>
public class Splash : MonoBehaviour
{
    private CountdownTimer _timer;

	/// <summary>
	/// The next scene to display after the behaviour has completed.
	/// </summary>
	public string NextScene;
	/// <summary>
	/// The duration of time that the screen should appear. (Seconds)
	/// </summary>
	public float Duration = 3f;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
    {
        // Initialisation
        _timer = new CountdownTimer {Seconds = Duration};
        _timer.Begin();
    }

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
    {
        // Load scene after splash timer has ended
        if (_timer.IsDone())
            SceneManager.LoadScene(NextScene);
        else
            _timer.Update();
    }
}