using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class FinishProjectTrigger : MonoBehaviour
{
    public TMP_InputField inputField;     // Drag your TMP InputField here
    public string triggerPhrase = "finish project";
    public string endSceneName = "EndScreen"; // Replace with your real end scene name

    void Start()
    {
        // Ensure input is always active and ready
        inputField.interactable = true;
        inputField.text = "";
        inputField.onValueChanged.AddListener(OnTextChanged);

        // Auto-focus on the field at start
        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
        inputField.ActivateInputField();

        Debug.Log("Input field is now active and ready for typing.");
    }

    void OnTextChanged(string text)
    {
        if (text.Trim().ToLower() == triggerPhrase.ToLower())
        {
            Debug.Log("Finish phrase detected. Loading end scene...");
            SceneManager.LoadScene(endSceneName);
        }
    }
}
