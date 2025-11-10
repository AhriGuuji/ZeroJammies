using System.Linq;
using UnityEngine;

public class Bookshelf : Anomaly
{
    [SerializeField]
    private GameObject[] _books;

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

            if (collider.GetComponent<Book>().name == _books[0].name)
                if (collider.GetComponent<Book>().name == _books[1].name)
                    if (collider.GetComponent<Book>().name == _books[2].name)
                        GameEvent();
                    else return;
                else return;
            else return;
        }
    }

    protected override void OnChangeState()
    {
        base.OnChangeState();

        foreach (GameObject book in _books)
        {
            Instantiate(book);
        }
    }
}
