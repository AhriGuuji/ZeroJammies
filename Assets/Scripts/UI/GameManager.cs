using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float _gameTimer;
    private Anomaly[] _anomalies;
    private List<Guest> _guests;
    [SerializeField]
    private float _firstEventTime = 5.0f;
    [SerializeField]
    private float _breakBetweenEvents;
    [SerializeField]
    private string _tab = "Tab";
    [SerializeField]
    private string _endGame = "GameOver";
    [SerializeField] TextMeshProUGUI timerText;
    private InputAction _pause;

    private void Awake()
    {
        _anomalies = FindObjectsByType<Anomaly>(FindObjectsSortMode.None);
        _guests = new(FindObjectsByType<Guest>(FindObjectsSortMode.None));
        _gameTimer = 0;
        _pause = InputSystem.actions.FindAction(_tab);

        InvokeRepeating("StartAnomaly", _firstEventTime, _breakBetweenEvents);
    }

    private void Update()
    {
        _gameTimer += Time.deltaTime;
        timerText.text = $"{_gameTimer}";

        if (_guests.Count <= 0)
        {
            SceneManager.LoadScene(_endGame);
        }

        /*
        if (_pause.WasPressedThisFrame())
            Time.timeScale = 0;

        if (Time.timeScale == 0)
            if (_pause.WasPressedThisFrame())
                Time.timeScale = 1;*/
    }
    
    private void StartAnomaly()
    {
        _anomalies[Random.Range(0, _anomalies.Length)].ChangeState();
    }

    public void GuestRunAway(Guest guest)
    {
        _guests.Remove(guest);
    }
}
