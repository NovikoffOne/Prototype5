using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _minSpeed = 13f;
    [SerializeField] private float _maxSpeed = 16f;
    [SerializeField] private int _pointValue;
    [SerializeField] private ParticleSystem _exepsionPartical;

    private GameManager _gameManager;
    private Rigidbody _targetRigidbody;
    private float _maxTorque = 10f;
    private float _xRange = 4f;
    private float _ySpawnPosition = -3f;

    void Start()
    {
        _targetRigidbody = GetComponent<Rigidbody>();

        _targetRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRigidbody.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = StartPosition();

        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void OnMouseDown()
    {
        if (_gameManager.IsActiveGame)
        {
            Destroy(gameObject);

            Instantiate(_exepsionPartical, transform.position, _exepsionPartical.transform.rotation);

            _gameManager.UpdateScore(_pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bomb"))
        {
            _gameManager.TakeDamage();
        }
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }

    private Vector3 StartPosition()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPosition);
    }
}
