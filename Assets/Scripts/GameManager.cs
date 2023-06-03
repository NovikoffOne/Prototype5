using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(AudioSource))]

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _livesText;

    [SerializeField] private List<Target> _targets;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Button _restartButton;
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _pausePanel;

    public bool IsActiveGame;

    private AudioSource _audioSource;
    private bool _isPause;
    private int _score;
    private float _volume;
    private float _spawnRate = 1;
    private int _lives;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _volume = _audioSource.volume;

        _volumeSlider.value = _volume;

        _isPause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPause)
        {
            _isPause = true;

            Time.timeScale = 0f;

            IsActiveGame = false;

            _pausePanel.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _isPause)
        {
            _isPause = false;

            Time.timeScale = 1f;

            IsActiveGame = true;

            _pausePanel.SetActive(false);
        }
    }

    public void StartGame(int difficulty)
    {
        _spawnRate /= difficulty;

        IsActiveGame = true;

        _score = 0;

        _lives = 3;

        StartCoroutine(SpawnRandom());

        UpdateHealth();

        UpdateScore(_score);

        _titleScreen.SetActive(false);

        AudioControlls();

        _audioSource.Play();
    }

    public void UpdateScore(int score)
    {
        _score += score;

        if (_score < 0)
        {
            GameOver();
        }

        _scoreText.text = "Score : " + _score;
    }

    public void TakeDamage()
    {
        _lives -= 1;

        UpdateHealth();
    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);

        _audioSource.Stop();

        IsActiveGame = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        AudioControlls();
    }

    private IEnumerator SpawnRandom()
    {
        while (IsActiveGame)
        {
            yield return new WaitForSeconds(_spawnRate);

            int index = Random.Range(0, _targets.Count);

            Instantiate(_targets[index]);
        }
    }

    private void UpdateHealth()
    {
        if (IsActiveGame)
        {
            _livesText.text = "Lives : " + _lives;
        }
        
        if (_lives <= 0)
        {
            GameOver();
        }
    }

    private void AudioControlls()
    {
        _volume = _volumeSlider.value;

        _audioSource.volume = _volume;
    }
}
