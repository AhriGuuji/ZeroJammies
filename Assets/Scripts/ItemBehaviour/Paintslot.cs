using UnityEngine;

public class Paintslot : Anomaly
{
    private Painting _paiting;

    [SerializeField]
    private GameObject[] _paints;

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

            if (collider.GetComponent<Painting>())
            {
                GameEvent();
            }
        }
    }

    protected override void OnChangeState()
    {
        base.OnChangeState();

        foreach (GameObject paint in _paints)
        {
            Instantiate(paint);
        }
    }
}
