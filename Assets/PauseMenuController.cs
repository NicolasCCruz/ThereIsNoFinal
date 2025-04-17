using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public Button resumeButton;
    public Button[] pauseButtons;
    public Graphic[] uiGraphics;
    public SpriteRenderer[] spriteGraphics;
    public GameObject floatingPauseButton; // assign the PauseButton GameObject here
    public float fadeSpeed = 2f;

    private bool isPaused = false;

    void Start()
    {
        SetUIAlpha(0f);
        SetSpriteAlpha(0f);
        SetButtonsInteractable(false);

        if (floatingPauseButton != null)
            floatingPauseButton.SetActive(false); // Hide pause button at start

        isPaused = true;
        StartCoroutine(FadeInPauseMenu());

        resumeButton.onClick.AddListener(() =>
        {
            if (isPaused)
            {
                isPaused = false;
                StartCoroutine(FadeOutPauseMenu());
            }
        });
    }

    void SetButtonsInteractable(bool state)
    {
        foreach (var button in pauseButtons)
        {
            if (button != null)
                button.interactable = state;
        }
    }

    void SetUIAlpha(float alpha)
    {
        foreach (var g in uiGraphics)
        {
            if (g != null)
            {
                var color = g.color;
                color.a = alpha;
                g.color = color;
            }
        }
    }

    void SetSpriteAlpha(float alpha)
    {
        foreach (var sr in spriteGraphics)
        {
            if (sr != null)
            {
                var color = sr.color;
                color.a = alpha;
                sr.color = color;
            }
        }
    }

    public void ResumeFromFloatingPause()
    {
        if (!isPaused)
        {
            isPaused = true;
            StartCoroutine(FadeInPauseMenu());
        }
    }

    System.Collections.IEnumerator FadeInPauseMenu()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetUIAlpha(Mathf.Clamp01(alpha));
            SetSpriteAlpha(Mathf.Clamp01(alpha));
            yield return null;
        }

        SetButtonsInteractable(true);
    }

    System.Collections.IEnumerator FadeOutPauseMenu()
    {
        SetButtonsInteractable(false);

        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetUIAlpha(Mathf.Clamp01(alpha));
            SetSpriteAlpha(Mathf.Clamp01(alpha));
            yield return null;
        }

        if (floatingPauseButton != null)
        {
            floatingPauseButton.SetActive(true); // activate the floating pause button
        }
        floatingPauseButton.SetActive(true);
        floatingPauseButton.GetComponent<FloatingPauseButtonController>().ActivatePauseButton();

    }
}
