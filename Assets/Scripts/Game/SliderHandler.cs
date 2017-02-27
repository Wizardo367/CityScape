using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    private Slider _slider;
    private Game _game;

    public Text DisplayText;

    private void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
        _game = Game.Instance;

        // Set default display value
        string displayTextName = DisplayText.name;

        if (displayTextName.Contains("Residential"))
            DisplayText.text = _game.ResidentialTax + "%";
        else if (displayTextName.Contains("Commercial"))
            DisplayText.text = _game.CommercialTax + "%";
        else if (displayTextName.Contains("Office"))
            DisplayText.text = _game.OfficeTax + "%";
    }

    public void UpdateGame(string taxType)
    {
        taxType = taxType.ToLower(); // Prevent case-sensitive errors
        int sliderValue = Mathf.RoundToInt(_slider.value);

        // Update values
        if (taxType == "residential")
        {
            _game.ResidentialTax = sliderValue;
            transform.FindChild("Residential Tax Percentage Display").GetComponent<Text>().text = sliderValue + "%";
        }
        else if (taxType == "commercial")
        {
            _game.CommercialTax = sliderValue;
            transform.FindChild("Commercial Tax Percentage Display").GetComponent<Text>().text = sliderValue + "%";
        }
        else if (taxType == "office")
        {
            _game.OfficeTax = sliderValue;
            transform.FindChild("Office Tax Percentage Display").GetComponent<Text>().text = sliderValue + "%";
        }
    }
}
