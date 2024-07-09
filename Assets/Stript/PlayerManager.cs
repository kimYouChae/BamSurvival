using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _markerPrefabs;   // ������ ����
    [SerializeField] List<Marker> _markers;             // Marker Ŭ���� ����Ʈ�� ����

    [SerializeField] private float _speed;                               // �Ӹ� �ӵ�
    [SerializeField] private Vector2 _joystickVec;                       // ���̽�ƽ�� vec 
    [SerializeField] private int _bodyCount;                             // ���� ������ ������ ���� (�Ӹ�����)
    [SerializeField] private float _distanceBetween;                     // ������� ���� ������
    [SerializeField] private bool _isReadToMove;                         // ������ �� �Ǹ� , ������ �غ� ��
    // �̱���
    public static PlayerManager instance;

    // ������Ƽ
    public Vector2 joystickVec { get => _joystickVec; set { _joystickVec = value; } }

    private void Awake()
    {
        instance = this;
        _bodyCount = 0;
        _joystickVec = Vector2.up;
    }

    void Start()
    {
        _speed = 3f;
        _distanceBetween = 0.1f;
        _isReadToMove = false;

        StartCoroutine(F_CreateSnake());
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

    // �Ӹ� , ���� ���� �ڷ�ƾ
    IEnumerator F_CreateSnake() 
    {
        for (int i = 0; i < _markerPrefabs.Count; i++)
        {
            // �Ӹ�, ���� ���� 
            GameObject _headInstance = Instantiate(_markerPrefabs[i], Vector3.zero, Quaternion.identity);

            // ����Ʈ�� �߰�
            _markers.Add(_headInstance.GetComponent<Marker>());

            // ���밹�� ++
            _bodyCount++;

            // �����ð� ��ٸ��� 
            yield return new WaitForSeconds(_distanceBetween);
        }

        // CamaraScript�� �ֱ� (ī�޶� ����ٴ� ��ü��)
        gameObject.GetComponent<CameraMovement>().F_SettlingPlayer(_markers[0].gameObject);

        // ������ �غ� �� 
        _isReadToMove = true;

        Debug.Log("�ڷ�ƾ�������� ");

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

    }

    // ���� ������ 
    private void F_SnakeBodyMovement() 
    {
        if(_markers.Count >= 1)
        {

            // �Ӹ� ������ 1�� marker ���� �̵� 
            for (int i = 1; i < _markers.Count; i++)
            {
                // �� ���� marker
                Marker _frontMarker = _markers[i - 1];

                // ���� ��ġ : ���� marker�� ��ġ�� �̵�
                _markers[i].gameObject.transform.Translate(_frontMarker.markerTransform[0] -_markers[i].transform.position );

                // �� ���� marker�� ȸ�������� look
                _markers[i].transform.rotation = Quaternion.LookRotation(_frontMarker.markerRotation[0].eulerAngles);

                _frontMarker.markerTransform.RemoveAt(0);
                _frontMarker.markerRotation.RemoveAt(0);


            }

        }

    }
}
