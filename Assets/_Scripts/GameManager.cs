using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private int maxCollectable;

    [Header("UI")]
    [SerializeField] private GameObject victoryPan;

    private int _currentCollectable = 0;
    private bool _isWinnable;
    private Scene _currentScene;
    private static GameManager _instance;
    #endregion

    #region Properties
    public int CurrentCollectable
    {
        get { return _currentCollectable; }
        set { _currentCollectable = value; }
    }
    public static GameManager Instance => _instance;
    #endregion

    #region Built-in Methods
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentCollectable >= maxCollectable) 
        { 
            _isWinnable = true;
        }

        if (Input.GetKey(KeyCode.R)) 
        {
            SceneManager.LoadScene(_currentScene.name);
            Time.timeScale = 1;
        }
    }
    #endregion

    #region Custom Methods
    public void Victory()
    {
        if (_isWinnable && _currentCollectable == maxCollectable)
        {
            victoryPan.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    #endregion
}
