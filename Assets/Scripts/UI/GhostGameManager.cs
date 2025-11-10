using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GhostGameManager : MonoBehaviour
{
    private List<Guest> _guests;

    [SerializeField]
    private string _endGame = "Win Screen";
    private InputAction _pause;

    private void Awake()
    {
        _guests = new(FindObjectsByType<Guest>(FindObjectsSortMode.None));
    }

    private void Update()
    {
    

        if (_guests.Count <= 0)
        {
            SceneManager.LoadScene(_endGame);
        }

    }
    

    public void GuestRunAway(Guest guest)
    {
        _guests.Remove(guest);
    }
}
