using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
    }

    public void UpdateGame(string taxType)
    {
        taxType = taxType.ToLower(); // Prevent case-sensitive errors
        Game game = GameObject.Find("Game").GetComponent<Game>();

        if (taxType == "residential")
            game.ResidentialTax = _slider.value;
        else if (taxType == "commercial")
            game.CommercialTax = _slider.value;
        else if (taxType == "office")
            game.OfficeTax = _slider.value;
    }
}
