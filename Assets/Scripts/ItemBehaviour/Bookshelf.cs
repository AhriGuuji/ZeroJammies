using UnityEngine;

public class Bookshelf : Anomaly
{
    [SerializeField]
    private GameObject[] _books;
    protected override void OnChangeState()
    {
        base.ChangeState();

        foreach (GameObject book in _books)
        {
            Instantiate(book);
        }
    }
}
