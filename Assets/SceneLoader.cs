using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Button startButton;
    public GameObject popupPanel;
    public Button yesButton;
    public Button noButton;
    public Button[] allButtons;
    public Graphic[] popupGraphics;
    public SpriteRenderer[] popupSprites;
    public Graphic quitButtonGraphic;
    public string sceneToLoad;
    public float fadeSpeed = 2f;

    void Start()
    {
        SetUIAlpha(0f);
        SetSpriteAlpha(popupSprites, 0f);
        SetUIAlpha(quitButtonGraphic, 0f);

        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        EnableButtons(true);

        startButton.onClick.AddListener(() =>
        {
            EnableButtons(false);
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(FadeInPopupAndQuit());
        });

        yesButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneToLoad);
        });

        noButton.onClick.AddListener(() =>
        {
            StopAllCoroutines();
            StartCoroutine(FadeOutPopupExceptQuit());
        });
    }

    void EnableButtons(bool enable)
    {
        foreach (var btn in allButtons)
        {
            if (btn != null)
                btn.interactable = enable;
        }

        yesButton.interactable = !enable;
        noButton.interactable = !enable;
    }

    void SetUIAlpha(float alpha)
    {
        foreach (var g in popupGraphics)
        {
            if (g != null)
            {
                var color = g.color;
                color.a = alpha;
                g.color = color;
            }
        }
    }

    void SetUIAlpha(Graphic g, float alpha)
    {
        if (g != null)
        {
            var color = g.color;
            color.a = alpha;
            g.color = color;
        }
    }

    void SetSpriteAlpha(SpriteRenderer[] srs, float alpha)
    {
        foreach (var sr in srs)
        {
            if (sr != null)
            {
                var color = sr.color;
                color.a = alpha;
                sr.color = color;
            }
        }
    }

    System.Collections.IEnumerator FadeInPopupAndQuit()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            SetUIAlpha(Mathf.Clamp01(alpha));
            SetSpriteAlpha(popupSprites, Mathf.Clamp01(alpha));
            SetUIAlpha(quitButtonGraphic, Mathf.Clamp01(alpha));
            yield return null;
        }
    }

    System.Collections.IEnumerator FadeOutPopupExceptQuit()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            SetUIAlpha(Mathf.Clamp01(alpha));
            SetSpriteAlpha(popupSprites, Mathf.Clamp01(alpha));
            yield return null;
        }

        SetUIAlpha(quitButtonGraphic, 1f);

        EnableButtons(true); // Re-enable everything
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
    }
}
