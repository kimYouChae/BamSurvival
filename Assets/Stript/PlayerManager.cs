using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<Marker> _markers;             // Marker 클래스 리스트에 저장
    [SerializeField] List<Slider> _markerHpBar;         // Marker의 hp바 

    [Header("===snake State===")]
    [SerializeField] private float _speed;                               // 머리 속도
    [SerializeField] private bool _isReadToMove;                         // 움직일 준비가 된
    [SerializeField] private LayerMask _markerLayer;                     // marker의 layer int 

    [Header("===snake Move===")]
    private Vector2 _joystickVec;                       // 조이스틱의 vec 
    private List<Transform> _markerNowTransform;        // marker 움직임 위한 리스트 

    // 싱글톤
    public static PlayerManager instance;

    // 프로퍼티
    public Vector2 joystickVec { get => _joystickVec; set { _joystickVec = value; } }
    public LayerMask markerLayer => _markerLayer;
    public Transform headMarkerTransfrom => _markers[0].transform;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _speed = 3f;
        _joystickVec = Vector2.up;
        _isReadToMove = true;
        _markerLayer = LayerMask.GetMask("Marker");

        _markerNowTransform = new List<Transform>();


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

    // 머리 움직임 컨트롤
    private void F_HeadMoveControl() 
    {
        // 조이스틱 vector 수정
        // y는 조이스틱이 위를 향할 때 (0보다 클 때) ,  아래를 향할 때 (0보다 작을 때) 로 나뉨
        Vector2 _joyVec = new Vector2(_joystickVec.x, _joystickVec.y > 0 ? 1f : -1f);

        // head 움직이기 
        _markers[0].gameObject.transform.Translate
            (_joyVec * _speed * Time.deltaTime);

    }

    private void F_SnakeBodyMovement() 
    {
        // 배열 초기화 
        _markerNowTransform.Clear();

        // 현재 머리 + 몸통 위치 담아두기
        for (int i = 0; i < _markers.Count; i++) 
        {
            _markerNowTransform.Add(_markers[i].transform);
        }

        // 이동 , 머리제외
        for (int i = 1; i < _markers.Count; i++) 
        {
            Transform _nowMarker = _markers[i].transform;
            _markers[i].transform.position = Vector3.Lerp(
                _markers[i].transform.position,
                _markerNowTransform[i - 1].transform.position,
                _speed * Time.deltaTime);
        }
    }




}
