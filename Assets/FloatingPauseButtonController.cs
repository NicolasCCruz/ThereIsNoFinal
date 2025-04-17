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

        Debug.Log("FloatingPauseButtonController started. Waiting for activation.");
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
        Debug.Log("Pause button behavior activated after Resume.");
    }

    void OnPauseClicked()
    {
        clickCount++;

        if (!locked)
        {
            Debug.Log("Pause button clicked. Click count: " + clickCount);

            if (clickCount >= 3)
            {
                locked = true;
                SnapPauseButtonToTopRight();
                pauseMenu.ResumeFromFloatingPause();
                Debug.Log("Pause button locked. Triggering pause menu resume.");
                return;
            }
        }
        else
        {
            Debug.Log("Locked pause button clicked. Triggering pause menu again.");
            pauseMenu.ResumeFromFloatingPause();
        }
    }

    void MovePauseButton()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        Vector2 clampedPosition = new Vector2(x, y);
        pauseButtonTransform.anchoredPosition = clampedPosition;

        Debug.Log("Pause button position set to: " + clampedPosition.ToString("F2"));
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

        Debug.Log("Pause button snapped to top right at: " + clampedTopRight.ToString("F2"));
    }
}
