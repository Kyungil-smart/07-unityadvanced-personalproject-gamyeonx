using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MiniBunnySpawn : MonoBehaviour
{
    public static MiniBunnySpawn Instance { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private HitFlash _hitFlash;

    [Header("Score")]
    [SerializeField] private int _score;
    [SerializeField] private TMP_Text _scoreText;

    [Header("Spawn")]
    [SerializeField] private GameObject _miniBunny;
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _spawnInterval2 = 11f;
    [SerializeField] private float _spawnInterval3 = 7f;

    private const float Y = 5.55f;
    private const float MinX = 1.28f;
    private const float MaxX = 8.1f;

    private float _timer;
    private float _timer2;
    private float _timer3;

    private bool _isDead = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0f;
            Spawn();
        }

        _timer2 += Time.deltaTime;

        if (_timer2 >= _spawnInterval2)
        {
            _timer2 = 0f;
            SpawnOtherPlayer();
        }

        _timer3 += Time.deltaTime;

        if (_timer3 >= _spawnInterval3)
        {
            _timer3 = 0f;
            SpawnToPlayer();
        }

    }
    private void SpawnToPlayer()
    {
        float playerX = Mathf.Clamp(transform.position.x, MinX, MaxX);
        Vector3 pos = new Vector3(playerX, Y, 0f);

        Instantiate(_miniBunny, pos, Quaternion.identity);
    }

    private void SpawnOtherPlayer()
    {
        float mid = (MinX + MaxX) * 0.5f;
        float playerX = transform.position.x;

        float spawnX;

        if (playerX < mid)
            spawnX = Random.Range(mid, MaxX);  
        else
            spawnX = Random.Range(MinX, mid);  

        Vector3 pos = new Vector3(spawnX, Y, 0f);
        Instantiate(_miniBunny, pos, Quaternion.identity);
    }

    private void Spawn()
    {
        float randomX = Random.Range(MinX, MaxX);
        Vector3 pos = new Vector3(randomX, Y, 0f);

        Instantiate(_miniBunny, pos, Quaternion.identity);
    }

    public void AddScore(int value)
    {
        if (_isDead) return;

        int prevScore = _score;
        _score += value;

        if (prevScore / 350 < _score / 350)
        {
            _spawnInterval -= 0.05f;
            _spawnInterval2 -= 0.05f;
            _spawnInterval3 -= 0.05f;

            if (_spawnInterval < 0.05f)
                _spawnInterval = 0.05f;
            if (_spawnInterval2 < 0.05f)
                _spawnInterval2 = 0.05f;
            if (_spawnInterval3 < 0.05f)
                _spawnInterval3 = 0.05f;
        }

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = "점수 : " + _score.ToString();
    }

    public void Death()
    {
        if (_isDead) return;

        _isDead = true;

        _audioSource.PlayOneShot(_deathClip);
        _hitFlash?.hitFlash();

        Destroy(gameObject, 0.1f);
    }
}
