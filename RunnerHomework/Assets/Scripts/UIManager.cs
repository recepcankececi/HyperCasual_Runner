using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // UI Manager controls all UI elements of the game.
    public static UIManager manager;
    [SerializeField] GameObject _introPanel;
    [SerializeField] GameObject _nextLevelPanel;
    [SerializeField] GameObject _retryPanel;
    [SerializeField] Slider _progressBar;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _currentLevelText;
    [SerializeField] TextMeshProUGUI _nextLevelText;

    private void Awake() 
    {
        manager = this;    
    }
    private void Start() 
    {
        LevelText();    
    }
    public void ProgressBar(float value)
    {
        _progressBar.value = value;
    }
    
    public void LevelText()
    {
        _currentLevelText.text = "" + (LevelManager.manager.Index + 1);
        _nextLevelText.text = "" + (LevelManager.manager.Index + 2);
    }
    public void ScoreUpdate(int score)
    {
        _scoreText.text = "" + score;
    }
    public void HideIntro()
    {
        _introPanel.SetActive(false);
    }
    public void ShowNextLevelPanel()
    {
        _nextLevelPanel.SetActive(true);
    }
    public void LoadNextLevel()
    {
        LevelManager.manager.LoadLevel();
    }
    public void RestartLevel()
    {
        LevelManager.manager.RestartLevel();
    }
    public void RetryMethod()
    {
        StartCoroutine(Retry());
    }
    IEnumerator Retry()
    {
        yield return new WaitForSeconds(1f);
        _retryPanel.SetActive(true);
    }
}
