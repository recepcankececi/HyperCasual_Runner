using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // PlayerController takes user input and controls the player.
    private Rigidbody playerRb;
    private BoxCollider _collider;
    private SkinnedMeshRenderer _renderer;
    [SerializeField] Animator playerAnim;
    [SerializeField] Transform playerRoot;
    [SerializeField] Transform _finish;
    [SerializeField] ParticleSystem shadowParticle;
    private bool _isSwiping;
    private bool _isRunning;
    private Vector2 _startingTouch;
    private Vector3 _targetPosition = Vector3.zero;
    private int _currentLane;
    private int _startingLane = 1;
    private float _progress;
    [HideInInspector] public bool _isFinished;

    [SerializeField] int laneOffset;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private int _gold;
    private void Awake() 
    {
        _isRunning = false;
        _isFinished = false;
        _currentLane = _startingLane;
    }
    private void Start() 
    {
       _gold = PlayerPrefs.GetInt("gold", 0);
       playerRb = GetComponentInChildren<Rigidbody>();
       _collider = GetComponentInChildren<BoxCollider>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        UIManager.manager.ScoreUpdate(_gold);
    }
    
    private void Update() 
    {
        switch (GameManager.manager.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
            Prepare();
                break;
            case GameManager.GameState.MainGame:
            CameraManager.manager.GameCam();
            PlayerMove();
            SwipeInput();
                break;
            case GameManager.GameState.FinishGame:
            if(_isFinished)
            {
                CameraManager.manager.FinishCam();
            }
                break;
        }
    }
    private void Prepare()
    {
        if(Input.touchCount > 0)
        {
            GameManager.manager.ToMainGame();
            UIManager.manager.HideIntro();           
        }
        CameraManager.manager.IntroCam();
    }
    private void PlayerMove()
    {
        playerRoot.Translate(Vector3.forward * Time.deltaTime * speed);
        _isRunning = true;
        playerAnim.SetBool("run", true);
        _progress = playerRoot.position.z / _finish.position.z;
        UIManager.manager.ProgressBar(_progress);
    }
    public void FinishMove()
    {
        playerAnim.SetBool("run", false);
        playerAnim.SetTrigger("dance");
    }
    public void DefeatMove()
    {
        playerAnim.SetBool("run", false);
        playerAnim.SetTrigger("fall");
    }
    private void SwipeInput()
    {
        if(Input.touchCount == 1)
        {
            if(_isSwiping)
			{
				Vector2 diff = Input.GetTouch(0).position - _startingTouch;

				// Put difference in Screen ratio, but using only width, so the ratio is the same on both
                // axes (otherwise we would have to swipe more vertically...)
				diff = new Vector2(diff.x/Screen.width, diff.y/Screen.width);

				if(diff.magnitude > 0.05f) //we set the swip distance to trigger movement to 5% of the screen width
				{
					if(diff.x < 0)
					{
						ChangeLane(-1);
					}
					else
					{
						ChangeLane(1);
					}						
					_isSwiping = false;
				}
            }
            if(Input.GetTouch(0).phase == TouchPhase.Began)
			{
				_startingTouch = Input.GetTouch(0).position;
				_isSwiping = true;
			}
			else if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				_isSwiping = false;
			}
        }
    }
    private void ChangeLane(int direction)
    {
        if (!_isRunning)
			return;

        int targetLane = _currentLane + direction;

        if (targetLane < 0 || targetLane > 2)
            // Ignore, we are on the borders.
            return;
        
        StartCoroutine(Transparency());
        shadowParticle.transform.position = playerRoot.transform.position + Vector3.forward;
        shadowParticle.Play();
        
        _currentLane = targetLane;
        _targetPosition = new Vector3((_currentLane - 1) * laneOffset, playerRoot.position.y, playerRoot.position.z);
        playerRoot.position = _targetPosition;
    }
    IEnumerator Transparency()
    {
        Color tempColor = _renderer.material.color;
        tempColor.a = 0f;
        _renderer.material.color = tempColor;
        float timer = 0f;
        while(true)
        {
            timer += Time.deltaTime/2;
            if(timer >= 1f)
            {
                break;
            }
            tempColor.a = Mathf.Lerp(0f, 1f, timer);
            yield return new WaitForEndOfFrame();
            _renderer.material.color = tempColor;
        }
    }
    public void GoldIncrease()
    {
        _gold += 10;
        PlayerPrefs.SetInt("gold", _gold);
        UIManager.manager.ScoreUpdate(_gold);
    }
    
}
