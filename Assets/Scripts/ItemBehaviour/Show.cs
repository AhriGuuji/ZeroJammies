using System.Collections;
using UnityEngine;

public class Show : MonoBehaviour
{
    [SerializeField]
    private float _showTime;

    public IEnumerator ShowMe()
    {
        if (GetComponent<SpriteRenderer>().enabled)
        {
            yield break;
        }
    
        WaitForSeconds wfs = new(_showTime);

        GetComponent<SpriteRenderer>().enabled = true;

        yield return wfs;

        GetComponent<SpriteRenderer>().enabled = false;
    }
}
