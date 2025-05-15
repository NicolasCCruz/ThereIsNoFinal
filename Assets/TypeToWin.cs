using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class TypeToWin : MonoBehaviour
{
    public TextMeshProUGUI titleText;     // Full title text: "THERE IS NO FINAL PROJECT"
    public TextMeshProUGUI typedText;     // Shows what the player is typing

    private string fullTitle = "THERE IS NO FINAL PROJECT";
    private string targetPhrase = "finish project";
    private string typed = "";
    private char[] displayedTitle;

    void Start()
    {
        displayedTitle = fullTitle.ToCharArray();
        titleText.text = fullTitle;
        typedText.text = "";
    }

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (!char.IsLetter(c) && c != ' ')
                continue;

            char lowerC = char.ToLower(c);

            if (typed.Length < targetPhrase.Length && lowerC == targetPhrase[typed.Length])
            {
                typed += lowerC;
                typedText.text = typed;

                char toReplace = char.ToUpper(lowerC); // Match uppercase in title
                for (int i = 0; i < displayedTitle.Length; i++)
                {
                    if (displayedTitle[i] == toReplace)
                    {
                        displayedTitle[i] = ' ';
                        break;
                    }
                }

                titleText.text = new string(displayedTitle);

                if (typed == targetPhrase)
                {
                    SceneManager.LoadScene("EndScreen"); // Replace with your actual scene name

                }
            }
        }
    }
}
