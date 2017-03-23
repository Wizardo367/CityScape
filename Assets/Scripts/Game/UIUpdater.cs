using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to update a UI element.
/// </summary>
public class UIUpdater : MonoBehaviour
{
	public InputField Field;
	public Image Icon;
	public Text Text;

	private string _gameObjectName;
	private Game _game;

	/// <summary>
	/// Initialises this instance.
	/// </summary>
	private void Start()
	{
		// Get game instance
		_game = GameObject.Find("Game").GetComponent<Game>();
		// Get component
		Field = gameObject.GetComponent<InputField>();
		// Get the type of field
		_gameObjectName = gameObject.name;
	}

	/// <summary>
	/// Updates this instance.
	/// </summary>
	private void Update()
	{
		// Update field based on gameObject
		if (_gameObjectName == "Money_Display")
		{
			// Update the field to show the current amount of money the player has
			int money = _game.Money;
			Field.text = money.ToString();

			if (Text != null)
				Text.color = money < 0 ? Color.red : new Color(0f, 0.44f, 0.13f);
		}
		else if (_gameObjectName == "Population_Display")
		{
			// Update the field to show the current population
			Field.text = _game.Population.ToString();
		}
		else if (_gameObjectName == "Happiness_Display")
		{
			// Update the field to show the current level of happiness
			int happiness = Mathf.Clamp(_game.Happiness, 0, 100);
			Field.text = happiness + "%";

			// Update icon to reflect percentage of happiness
			if (Icon != null)
			{
				string path = happiness >= 67 ? "UI/Happy" : happiness >= 33 ? "UI/Neutral" : "UI/Sad";
				Icon.sprite = Resources.Load<Sprite>(path);
			}
		}
	}
}
