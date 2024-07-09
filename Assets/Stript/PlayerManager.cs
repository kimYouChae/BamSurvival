using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _markerPrefabs;   // 프리팹 저장
    [SerializeField] List<Marker> _markers;             // Marker 클래스 리스트에 저장
    [SerializeField] List<Slider> _markerHpBar;         // Marker의 hp바 

    [SerializeField] private float _speed;                               // 머리 속도
    [SerializeField] private Vector2 _joystickVec;                       // 조이스틱의 vec 
    [SerializeField] private int _bodyCount;                             // 현재 생성한 몸통의 개수 (머리포함)
    [SerializeField] private float _distanceBetween;                     // 몸통사이 생성 딜레이
    [SerializeField] private bool _isReadToMove;                         // 생성이 다 되면 , 움직일 준비가 된
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
        _speed = 3f;
        _bodyCount = 0;
        _joystickVec = Vector2.up;
        _distanceBetween = 0.1f;
        _isReadToMove = false;

        _markerHpBar = new List<Slider>();

        StartCoroutine(F_CreateSnake());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (_isReadToMove)
        {
            // 머리 움직임 
            F_HeadMoveControl();

            // 몸통 움직임 
            F_SnakeBodyMovement();
        }

    }

    // 머리 , 몸통 생성 코루틴
    IEnumerator F_CreateSnake() 
    {
        for (int i = 0; i < _markerPrefabs.Count; i++)
        {
            // 머리, 몸통 생성 
            GameObject _segments = Instantiate(_markerPrefabs[i], Vector3.zero, Quaternion.identity);

            // 리스트에 추가
            _markers.Add(_segments.GetComponent<Marker>());

            // 몸통갯수 ++
            _bodyCount++;

            // Hp바 초기화ㅣ
            F_ChangeHpValue(_segments.GetComponent<Marker>(), 1f);

            // 일정시간 기다리기 
            yield return new WaitForSeconds(_distanceBetween);
        }

        // CamaraScript에 넣기 (카메라가 따라다닐 주체로)
        gameObject.GetComponent<CameraMovement>().F_SettlingPlayer(_markers[0].gameObject);

        // 움직일 준비 완 
        _isReadToMove = true;

        Debug.Log("코루틴끝날예정 ");

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

    }

    // 몸통 움직임 
    private void F_SnakeBodyMovement() 
    {
        if(_markers.Count >= 1)
        {

            // 머리 제외한 1번 marker 부터 이동 
            for (int i = 1; i < _markers.Count; i++)
            {
                // 내 이전 marker
                Marker _frontMarker = _markers[i - 1];

                // 현재 위치 : 이전 marker의 위치로 이동
                _markers[i].gameObject.transform.Translate(_frontMarker.markerTransform[0] -_markers[i].transform.position );

                // 내 앞의 marker의 회전값으로 look
                _markers[i].transform.rotation = Quaternion.LookRotation(_frontMarker.markerRotation[0].eulerAngles);

                _frontMarker.markerTransform.RemoveAt(0);
                _frontMarker.markerRotation.RemoveAt(0);


            }

        }

    }

    // float만큼 hp바의 value 바꾸기 
    private void F_ChangeHpValue(Marker v_marker , float v_value) 
    {
        // hpbar(Slider)의 value를 수정 
        v_marker.markerHpBar.value = v_value;
    }
}
