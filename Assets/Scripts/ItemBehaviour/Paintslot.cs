using UnityEngine;

public class Paintslot : Anomaly
{
    private Painting _paiting;

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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _paiting = collision.GetComponent<Painting>();

        if (_paiting)
            GameEvent();
    }
}
