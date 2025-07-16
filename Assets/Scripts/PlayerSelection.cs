// PlayerSelection.cs
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    public static PlayerSelection Instance;

    public enum Gender { Male, Female }
    public Gender selectedGender = Gender.Male;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectMale() => selectedGender = Gender.Male;
    public void SelectFemale() => selectedGender = Gender.Female;
}
