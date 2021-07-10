using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Level Manager is only script that work outside the level prefabs. We control and manage levels with it.
    public static LevelManager manager;
    [SerializeField] List<GameObject> levels;
    [SerializeField] GameObject loadingPanel;
    [SerializeField] Animator loadingAnim;
    private GameObject currentLevel;

    private int index;

    private void Awake() 
    {
        manager = this;    
    }
    private void Start() 
    {
        index = 0;
        StartCoroutine(LevelLoader(index));
    }
    public int Index 
    { 
        get
        {
            return index;
        } 
        set
        {

        } 
    }
    // Everytime we load a level, level index increase 1.
    public void LoadLevel()
    {
        index++;
        StartCoroutine(LevelLoader(index));
    }
    // We control level transitions with this coroutine.
    IEnumerator LevelLoader(int index)
    {
        loadingPanel.SetActive(true);
        loadingAnim.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1f);
        if(currentLevel)
        {
            Destroy(currentLevel);
        }
        currentLevel = Instantiate(levels[index], Vector3.zero, Quaternion.identity);
        loadingAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        loadingPanel.SetActive(false);
    }
    public void RestartLevel()
    {
        StartCoroutine(LevelLoader(index));
    }

}
