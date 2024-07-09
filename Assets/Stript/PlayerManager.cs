using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _markerPrefabs;   // 프리팹 저장
    [SerializeField] List<Marker> _markers;             // Marker 클래스 리스트에 저장

    [SerializeField] 
    private float _speed;                               // 머리 속도
    private Vector2 _joystickVec;                       // 조이스틱의 vec 
    private int _bodyCount;                             // 현재 생성한 몸통의 개수 (머리포함)

    // 싱글톤
    public static PlayerManager instance;

    // 프로퍼티
    public Vector2 joystickVec { get => _joystickVec; set { _joystickVec = value; } }

    private void Awake()
    {
        instance = this;
        _bodyCount = 0;
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

        // 몸통 생성
        F_CreateSnakeBody();

        // 몸통 움직임 
        F_SnakeBodyMovement();
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

        // 몸통갯수 ++
        _bodyCount++;

    }


    // 머리 움직임 컨트롤
    private void F_HeadMoveControl() 
    {
        // 앞으로 직진
        if (_markers.Count > 0) 
        {
            // 조이스틱 vector 수정
            // y는 조이스틱이 위를 향할 때 (0보다 클 때) ,  아래를 향할 때 (0보다 작을 때) 로 나뉨
            Vector2 _joyVec = new Vector2(_joystickVec.x , _joystickVec.y > 0 ? 1f : -1f );

            _markers[0].gameObject.transform.Translate
                (_joyVec * _speed * Time.deltaTime);

        }
    }

    // 특정키 Input 시 몸통 생김
    // ##TODO : 나중에 특정 아이템 획득 시 몸통 생기게 수정해야함 
    private void F_CreateSnakeBody() 
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            // 생성시 위치 , 회전값 : 나의 바로 앞에 있는 marker의 위치와 회전값 가짐  
            GameObject _body = Instantiate(_markerPrefabs[1] 
                , _markers[_bodyCount - 1].markerTransform[0] , _markers[_bodyCount - 1].markerRotation[1]);
            _markers.Add(_body.GetComponent<Marker>());

            // 내 앞에 있는 marker을 clear
            _markers[ _bodyCount - 1].F_clearDataList();

        }
    }

    // 몸통 움직임 
    private void F_SnakeBodyMovement() 
    {
        // 머리 제외한 1번 marker 부터 이동 
        for (int i = 1; i < _markers.Count; i++) 
        {
            GameObject _currMarker = _markers[i].gameObject;

            // 내 앞의 marker 위치 Vector로 이동 
            _currMarker.transform.Translate(  _markers[i - 1].markerTransform[0] * Time.deltaTime);

            // 내 앞의 marker의 회전값으로 look
            _currMarker.transform.rotation = Quaternion.LookRotation( _markers[i - 1].markerRotation[0].eulerAngles );

            // 리스트 비우기 
            _markers[i - 1].F_clearDataList();


        }
    }
}
