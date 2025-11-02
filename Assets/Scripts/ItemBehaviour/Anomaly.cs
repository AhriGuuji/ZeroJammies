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
    private STATE _gameState;
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

            if (_interactionsPressed == _interactionsNeeded)
            {
                GameEvent();
                _interactionsPressed++;
            }
        }
    }

    protected override void GameEvent()
    {
        _gameState = STATE.Normal;
        GetComponent<SpriteRenderer>().sprite = _normalState;
        _interactionsPressed = 0;
    }
}

