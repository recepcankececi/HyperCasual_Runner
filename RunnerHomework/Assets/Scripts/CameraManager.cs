using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager manager;
    private Camera _camera;
    [SerializeField] Transform _player;
    [SerializeField] Transform _camPos;
    [SerializeField] Transform _introCam;
    [SerializeField] Transform _gameCam;
    [SerializeField] Transform _finishCam;
    private void Awake() 
    {
        manager = this;
    }
    private void Start() 
    {
        _camera = FindObjectOfType<Camera>();   
    }
    // Camera switches controlled by PlayerController with this instance.
    public void IntroCam()
    {
        CamFollow(2f);
        if(_camera.transform.parent != _introCam)
        {
            _camera.transform.SetParent(_introCam);
        }
        CamLerp(2f);
    }
    public void GameCam()
    {
        CamFollow(5f);
        if(_camera.transform.parent != _gameCam)
        {
            _camera.transform.SetParent(_gameCam);
        }
        CamLerp(2f);
    }
    public void FinishCam()
    {
        CamFollow(2f);
        if(_camera.transform.parent != _finishCam)
        {
            _camera.transform.SetParent(_finishCam);
        }
        CamLerp(2f);
    }
    // Changing camera positions smoothly with lerping.
    private void CamLerp(float value)
    {
        _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.zero, 
        Time.deltaTime *value);

        _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, Quaternion.identity,
        Time.deltaTime*value);
    }
    private void CamFollow(float value)
    {
        _camPos.position = Vector3.Lerp(_camPos.position, _player.position, Time.deltaTime*value);
    }
    


}
