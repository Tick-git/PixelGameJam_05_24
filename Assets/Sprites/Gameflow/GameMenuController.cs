using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] EventChannel _onGameStartEvent;

    VisualElement _root;

    Button _continueButton;
    Button _startButton;
    Button _exitButton;
    Button _muteButton;
    private bool _gameRunning;

    GameManagerBehavior _gameManager;

    private void Awake()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        _gameRunning = false;

        _continueButton = _root.Q<Button>("ContinueButton");
        _continueButton.clicked += ChangeGameMenuDisplayStatus;
        _continueButton.style.display = DisplayStyle.None;

        _startButton = _root.Q<Button>("StartButton");
        _startButton.clicked += OnStartButtonClicked;

        _muteButton = _root.Q<Button>("MuteButton");
        _muteButton.clicked += OnMuteButtonClicked;

        _exitButton = _root.Q<Button>("ExitButton");
        _exitButton.clicked += OnExitButtonClicked;

        _audioMixer.GetFloat("MasterVolume", out float volume);

        if(volume < 0)
        {
            _muteButton.text = "Unmute";
        }
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (!_gameRunning) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeGameMenuDisplayStatus();
        }
    }

    private void ChangeGameMenuDisplayStatus()
    {
        if (_root.style.display == DisplayStyle.None)
        {
            Time.timeScale = 0;
            _root.style.display = DisplayStyle.Flex;
        }
        else
        {
            Time.timeScale = 1;
            _root.style.display = DisplayStyle.None;
        }
    }

    private void OnMuteButtonClicked()
    {
        _audioMixer.GetFloat("MasterVolume", out float volume);

        if(volume >= 0)
        {
            _audioMixer.SetFloat("MasterVolume", -80);
            _muteButton.text = "Unmute";
        } 
        else
        {
            _audioMixer.SetFloat("MasterVolume", 0);
            _muteButton.text = "Mute";
        }

    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void OnStartButtonClicked()
    {
        if(!_gameRunning)
        {
            ChangeGameMenuDisplayStatus();
            _onGameStartEvent.Invoke(new Empty());
            _gameRunning = true;
            _startButton.text = "Restart";
            _continueButton.style.display = DisplayStyle.Flex;
        } 
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }      
    }
}
