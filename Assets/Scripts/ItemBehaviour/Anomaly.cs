using System;
using UnityEngine;

public class Anomaly : Interactable
{
    protected enum STATE
    {
        Normal,
        Anomaly
    }

    [SerializeField] protected Sprite _normalState;
    [SerializeField] protected Sprite _anomalyState;
    [SerializeField] protected float _stress = 1.0f;
    protected STATE _gameState;

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

            if (_interactionsPressed == _interactionsNeeded)
            {
                GameEvent();
            }

            if (collider.GetComponent<Guest>() & collider.GetComponent<Guest>().CanStress)
            {
                StartCoroutine(collider.GetComponent<Guest>().IncrementStress(_stress));
            }
        }
        else return;
    }

    protected override void GameEvent()
    {
        _gameState = STATE.Normal;
        GetComponent<SpriteRenderer>().sprite = _normalState;
        _interactionsPressed = 0;
        FindAnyObjectByType<GameManager>().IncAnomaliesCorrected();
    }

    public void ChangeState()
    {
        try
        {
            OnChangeState();
        }
        catch(StackOverflowException)
        {
            return;
        }
    }

    protected virtual void OnChangeState()
    {
        _gameState = STATE.Anomaly;
    }
}

