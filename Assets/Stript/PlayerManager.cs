using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _markerPrefabs;   // ������ ����
    [SerializeField] List<Marker> _markers;             // Marker Ŭ���� ����Ʈ�� ����

    [SerializeField] 
    private float _speed;                               // �Ӹ� �ӵ�
    private Vector2 _joystickVec;                       // ���̽�ƽ�� vec 
    private int _bodyCount;                             // ���� ������ ������ ���� (�Ӹ�����)

    // �̱���
    public static PlayerManager instance;

    // ������Ƽ
    public Vector2 joystickVec { get => _joystickVec; set { _joystickVec = value; } }

    private void Awake()
    {
        instance = this;
        _bodyCount = 0;
    }

    void Start()
    {
        // �Ӹ� �ʱ� ���� 
        F_HeadInit();

        _speed = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // �Ӹ� ������ 
        F_HeadMoveControl();

        // ���� ����
        F_CreateSnakeBody();

        // ���� ������ 
        F_SnakeBodyMovement();
    }

    // �Ӹ� �ʱ� ����
    private void F_HeadInit() 
    {
        // �Ӹ�����
        GameObject _headInstance = Instantiate(_markerPrefabs[0], Vector3.zero , Quaternion.identity);
        // ����Ʈ�� �߰�
        _markers.Add(_headInstance.GetComponent<Marker>());

        // CamaraScript�� �ֱ�
        gameObject.GetComponent<CameraMovement>().F_SettlingPlayer(_markers[0].gameObject);

        // ���밹�� ++
        _bodyCount++;

    }


    // �Ӹ� ������ ��Ʈ��
    private void F_HeadMoveControl() 
    {
        // ������ ����
        if (_markers.Count > 0) 
        {
            // ���̽�ƽ vector ����
            // y�� ���̽�ƽ�� ���� ���� �� (0���� Ŭ ��) ,  �Ʒ��� ���� �� (0���� ���� ��) �� ����
            Vector2 _joyVec = new Vector2(_joystickVec.x , _joystickVec.y > 0 ? 1f : -1f );

            _markers[0].gameObject.transform.Translate
                (_joyVec * _speed * Time.deltaTime);

        }
    }

    // Ư��Ű Input �� ���� ����
    // ##TODO : ���߿� Ư�� ������ ȹ�� �� ���� ����� �����ؾ��� 
    private void F_CreateSnakeBody() 
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            // ������ ��ġ , ȸ���� : ���� �ٷ� �տ� �ִ� marker�� ��ġ�� ȸ���� ����  
            GameObject _body = Instantiate(_markerPrefabs[1] 
                , _markers[_bodyCount - 1].markerTransform[0] , _markers[_bodyCount - 1].markerRotation[1]);
            _markers.Add(_body.GetComponent<Marker>());

            // �� �տ� �ִ� marker�� clear
            _markers[ _bodyCount - 1].F_clearDataList();

        }
    }

    // ���� ������ 
    private void F_SnakeBodyMovement() 
    {
        // �Ӹ� ������ 1�� marker ���� �̵� 
        for (int i = 1; i < _markers.Count; i++) 
        {
            GameObject _currMarker = _markers[i].gameObject;

            // �� ���� marker ��ġ Vector�� �̵� 
            _currMarker.transform.Translate(  _markers[i - 1].markerTransform[0] * Time.deltaTime);

            // �� ���� marker�� ȸ�������� look
            _currMarker.transform.rotation = Quaternion.LookRotation( _markers[i - 1].markerRotation[0].eulerAngles );

            // ����Ʈ ���� 
            _markers[i - 1].F_clearDataList();


        }
    }
}
