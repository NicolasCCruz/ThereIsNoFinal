using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingPauseButtonController : MonoBehaviour
{
    public Button pauseButton;
    public RectTransform pauseButtonTransform;
    public TMP_Text pauseButtonText;
    public PauseMenuController pauseMenu;

    public float moveInterval = 1.5f;
    public float fadeSpeed = 2f;
    public Vector2 finalPosition = new Vector2(429f, 234f); // Anchored final corner position

    private int clickCount = 0;
    private bool locked = false;
    private float moveTimer = 0f;
    private bool fadingOut = true;
    private bool activated = false;

    public static bool canType = false;

    // Clamping bounds
    private float minX = -422f;
    private float maxX = 429f;
    private float minY = -234f;
    private float maxY = 234f;

    void Start()
    {
        pauseButton.onClick.AddListener(OnPauseClicked);

        // Hide text at start
        Color c = pauseButtonText.color;
        c.a = 0f;
        pauseButtonText.color = c;

    }

    void Update()
    {
        if (!activated || locked) return;

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveInterval)
        {
            MovePauseButton();
            moveTimer = 0f;
        }

        FlashPauseButtonText();
    }

    public void ActivatePauseButton()
    {
        activated = true;
    }

    void OnPauseClicked()
    {
        clickCount++;

        if (!locked)
        {
            if (clickCount >= 3)
                {
                    locked = true;
                    canType = true; // <-- Allow typing now
                    SnapPauseButtonToTopRight();
                    pauseMenu.ResumeFromFloatingPause();
                    return;
                }
        }
        else
        {
            pauseMenu.ResumeFromFloatingPause();
        }
    }

    void MovePauseButton()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        Vector2 clampedPosition = new Vector2(x, y);
        pauseButtonTransform.anchoredPosition = clampedPosition;

    }

    void FlashPauseButtonText()
    {
        Color c = pauseButtonText.color;
        float delta = fadeSpeed * Time.deltaTime;

        if (fadingOut)
        {
            c.a -= delta;
            if (c.a <= 0f)
            {
                c.a = 0f;
                fadingOut = false;
            }
        }
        else
        {
            c.a += delta;
            if (c.a >= 1f)
            {
                c.a = 1f;
                fadingOut = true;
            }
        }

        pauseButtonText.color = c;
    }

    void SnapPauseButtonToTopRight()
    {
        Vector2 clampedTopRight = new Vector2(maxX, maxY);
        pauseButtonTransform.anchoredPosition = clampedTopRight;

        Color c = pauseButtonText.color;
        c.a = 1f;
        pauseButtonText.color = c;

    }
}
