using UnityEngine;

enum STATE
{
    Normal,
    Anomaly
}

public class Anomaly : Interactable
{
    [SerializeField] private Sprite _normalState;
    [SerializeField] private Sprite _anomalyState;
    [SerializeField] private float _stress = 1.0f;
    private STATE _gameState;

    protected override void Awake()
    {
        base.Awake();

        GetComponent<SpriteRenderer>().sprite = _normalState;
    }

    protected override void Update()
    {
        if (_gameState == STATE.Anomaly)
        {
            GetComponent<SpriteRenderer>().sprite = _anomalyState;

            Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactRange, _playerLayer);
            if (!collider) return;

            if (collider.GetComponent<PlayerMovement>())
            {
                if (_interact.WasPressedThisFrame())
                {
                    _interactionsPressed++;
                }
            }

            if (collider.GetComponent<Guest>() & collider.GetComponent<Guest>().CanStress)
            {
                StartCoroutine(collider.GetComponent<Guest>().IncrementStress(_stress));
            }

            if (_interactionsPressed == _interactionsNeeded)
            {
                GameEvent();
            }
        }
    }

    protected override void GameEvent()
    {
        _gameState = STATE.Normal;
        GetComponent<SpriteRenderer>().sprite = _normalState;
        _interactionsPressed = 0;
    }

    public void ChangeState()
    {
        _gameState = STATE.Anomaly;
    }
}

