using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _markerPrefabs;   // 프리팹 저장
    [SerializeField] List<Marker> _markers;             // Marker 클래스 리스트에 저장

    [SerializeField] 
    private float _speed;
    private Vector2 _joystickVec;

    // 싱글톤
    public static PlayerManager instance;

    // 프로퍼티
    public Vector2 joystickVec { get => _joystickVec; set { _joystickVec = value; } }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // 머리 초기 생성 
        F_HeadInit();

        _speed = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 머리 움직임 
        F_HeadMoveControl();
    }

    // 머리 초기 생성
    private void F_HeadInit() 
    {
        // 머리생성
        GameObject _headInstance = Instantiate(_markerPrefabs[0], Vector3.zero , Quaternion.identity);
        // 리스트에 추가
        _markers.Add(_headInstance.GetComponent<Marker>());

        // CamaraScript에 넣기
        gameObject.GetComponent<CameraMovement>().F_SettlingPlayer(_markers[0].gameObject);

    }


    // 머리 움직임 컨트롤
    private void F_HeadMoveControl() 
    {
        // 앞으로 직진
        if (_markers.Count > 0) 
        {
            // 조이스틱 vector 수정
            Vector2 _joyVec = new Vector2(_joystickVec.x , _joystickVec.y > 0 ? 1f : -1f );

            _markers[0].gameObject.transform.Translate
                (_joyVec * _speed * Time.deltaTime);

        }
    }
}
