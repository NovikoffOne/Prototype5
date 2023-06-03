using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] private int _difficulty;

    private Button _button;
    private GameManager _gameManager;

    private void Start()
    {
        _button = GetComponent<Button>();

        _gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        _button.onClick.AddListener(SetDifficulty);
    }

    private void SetDifficulty()
    {
        _gameManager.StartGame(_difficulty);

        //_gameManager.PlayAudio();
    }
}
