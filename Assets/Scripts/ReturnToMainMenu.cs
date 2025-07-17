using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string menuSceneName = "Menu";

    private InputAction menuButtonAction;

    void OnEnable()
    {
        var actionMap = inputActions.FindActionMap("MenuButton", true);
        menuButtonAction = actionMap.FindAction("MenuButton2", true);
        Debug.Log(actionMap);
        menuButtonAction.Enable();
        menuButtonAction.performed += OnMenuPressed;
    }

    void OnDisable()
    {
        if (menuButtonAction != null)
            menuButtonAction.performed -= OnMenuPressed;
    }

    private void OnMenuPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Menu key/button pressed!");
        SceneManager.LoadScene(menuSceneName);
    }
}

