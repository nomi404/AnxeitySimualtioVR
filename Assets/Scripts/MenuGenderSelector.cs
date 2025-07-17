using UnityEngine;
using UnityEngine.UI;

public class GenderToggleSelector : MonoBehaviour
{
    public Toggle maleToggle;
    public Toggle femaleToggle;

    void Start()
    {
        // Remove any old/broken listeners
        maleToggle.onValueChanged.RemoveAllListeners();
        femaleToggle.onValueChanged.RemoveAllListeners();

        // Rebind listeners to current singleton instance
        maleToggle.onValueChanged.AddListener((isOn) => {
            if (isOn) PlayerSelection.Instance.SelectMale();
        });

        femaleToggle.onValueChanged.AddListener((isOn) => {
            if (isOn) PlayerSelection.Instance.SelectFemale();
        });

        // Sync toggle states with selected gender when menu scene loads
        var selectedGender = PlayerSelection.Instance.selectedGender;
        maleToggle.isOn = (selectedGender == PlayerSelection.Gender.Male);
        femaleToggle.isOn = (selectedGender == PlayerSelection.Gender.Female);
    }
}
