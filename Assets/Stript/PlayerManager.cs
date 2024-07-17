using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<Marker> _markers;             // Marker Ŭ���� ����Ʈ�� ����
    [SerializeField] List<Slider> _markerHpBar;         // Marker�� hp�� 

    [Header("===snake State===")]
    [SerializeField] private float _speed;                               // �Ӹ� �ӵ�
    [SerializeField] private bool _isReadToMove;                         // ������ �غ� ��
    [SerializeField] private LayerMask _markerLayer;                     // marker�� layer int 

    [Header("===snake Move===")]
    private Vector2 _joystickVec;                       // ���̽�ƽ�� vec 
    private List<Transform> _markerNowTransform;        // marker ������ ���� ����Ʈ 

    // �̱���
    public static PlayerManager instance;

    // ������Ƽ
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
            // �Ӹ� ������ 
            F_HeadMoveControl();

            // ���� ������ 
            F_SnakeBodyMovement();
        }
        
    }

    // �Ӹ� ������ ��Ʈ��
    private void F_HeadMoveControl() 
    {
        // ���̽�ƽ vector ����
        // y�� ���̽�ƽ�� ���� ���� �� (0���� Ŭ ��) ,  �Ʒ��� ���� �� (0���� ���� ��) �� ����
        Vector2 _joyVec = new Vector2(_joystickVec.x, _joystickVec.y > 0 ? 1f : -1f);

        // head �����̱� 
        _markers[0].gameObject.transform.Translate
            (_joyVec * _speed * Time.deltaTime);

    }

    private void F_SnakeBodyMovement() 
    {
        // �迭 �ʱ�ȭ 
        _markerNowTransform.Clear();

        // ���� �Ӹ� + ���� ��ġ ��Ƶα�
        for (int i = 0; i < _markers.Count; i++) 
        {
            _markerNowTransform.Add(_markers[i].transform);
        }

        // �̵� , �Ӹ�����
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
