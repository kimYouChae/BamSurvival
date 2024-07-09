using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _markerPrefabs;   // ������ ����
    [SerializeField] List<Marker> _markers;             // Marker Ŭ���� ����Ʈ�� ����

    [SerializeField] 
    private float _speed;
    private Vector2 _joystickVec;

    // �̱���
    public static PlayerManager instance;

    // ������Ƽ
    public Vector2 joystickVec { get => _joystickVec; set { _joystickVec = value; } }

    private void Awake()
    {
        instance = this;
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

    }


    // �Ӹ� ������ ��Ʈ��
    private void F_HeadMoveControl() 
    {
        // ������ ����
        if (_markers.Count > 0) 
        {
            // ���̽�ƽ vector ����
            Vector2 _joyVec = new Vector2(_joystickVec.x , _joystickVec.y > 0 ? 1f : -1f );

            _markers[0].gameObject.transform.Translate
                (_joyVec * _speed * Time.deltaTime);

        }
    }
}
