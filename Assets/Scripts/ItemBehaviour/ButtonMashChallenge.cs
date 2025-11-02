using UnityEngine;

public class ButtonMashChallenge : Interactable
{
    [SerializeField] private int requiredPresses = 10;
    [SerializeField] private float timeLimit = 3f;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private MemoryTimer memoryTimer;

    private int currentPresses = 0;
    private float timer = 0f;
    private bool timerRunning = false;

    private void Start()
    {
        if (memoryTimer == null)
        {
            memoryTimer = FindAnyObjectByType<MemoryTimer>();
            if (memoryTimer == null)
                Debug.LogError("No MemoryTimer found in the scene!");
        }
    }

    protected override void Update()
    {

        if (_interact == null)
        {
            Debug.LogError("_interact is null! Did you assign your InputActionReference in Interactable?");
            return;
        }
        
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactRange, _playerLayer);
        if (!collider) return;

        if (collider.GetComponent<PlayerMovement>())
        {
            if (_interact.WasPressedThisFrame())
            {
                if (!timerRunning)
                {
                    timerRunning = true;
                    timer = timeLimit;
                }

                currentPresses++;
                Debug.Log($"Pressed {currentPresses}/{requiredPresses}");

                if (currentPresses >= requiredPresses)
                {
                    Debug.Log("Success! You mashed fast enough!");
                    GameEvent();
                    ResetChallenge();
                }
            }
        }

        if (timerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                Debug.Log("Time’s up! Restarting challenge...");
                ResetChallenge();
            }
        }
    }

    private void ResetChallenge()
    {
        currentPresses = 0;
        timerRunning = false;
        timer = 0f;
    }

    protected override void GameEvent()
    {
        Debug.Log("Xerz");

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = true;
            sr.sprite = _sprite;
        }

        if (memoryTimer != null)
        {
            memoryTimer.resetTimer();
        }
        else
        {
            Debug.LogWarning("⚠️ memoryTimer is not assigned! Cannot reset timer.");
        }
    }
}
