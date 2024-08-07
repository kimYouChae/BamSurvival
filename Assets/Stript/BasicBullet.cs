using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BasicBullet : MonoBehaviour
{
    [Header("===Bullet Field===")]
    [SerializeField]
    private Transform _bulletDestination;   // bullet ������
    [SerializeField]
    private BulletSate _bulletState;        // bulletState
    [SerializeField]
    private int _currBounceCount;           // ���� this�� ƨ�� Ƚ�� 
    [SerializeField]
    private Vector2 _bulletStartPosition;         // �Ի�-�ݻ簢 ���� ���� ��ġ 
    [SerializeField]
    private bool _iscollisionToWall;            // ���� �浹�ߴ��� ?
    [SerializeField]
    Vector2 lastVelocity;

    [Header("===Component===")]
    [SerializeField]private Rigidbody2D _bulletRidigBody;

    // ������Ƽ
    public Transform bulletDestination { get => _bulletDestination; set { _bulletDestination = value; } }
    public BulletSate bulletState { set { _bulletState = value; } }
    public Vector2 bulletStartPosition { set { _bulletStartPosition = value; } }

    void Start()
    {
        _bulletRidigBody = gameObject.GetComponent<Rigidbody2D>();
        _currBounceCount = 0;
        _iscollisionToWall = false;
    }

    void Update()
    {
        if (_bulletDestination != null && !_iscollisionToWall)
        {
            // bullet speed ��ŭ �������� �̵� 
            /*
            gameObject.transform.position = Vector3.Lerp
                (gameObject.transform.position,
                _bulletDestination.position,
                Time.deltaTime * _bulletState.bulletSpeed);
            */

            Vector2 direction = (_bulletDestination.position - transform.position).normalized;  // ���⺤���� ����ȭ (0~1������ ���� ����ȭ��)
            _bulletRidigBody.velocity = direction * _bulletState.bulletSpeed;                   // ���⺤�� * speed�� ������

            lastVelocity = _bulletRidigBody.velocity;
            Debug.Log(this.name + "�� �ӵ� : " + _bulletRidigBody.velocity);
            Debug.Log(this.name + "�� �ӵ�2 : " + lastVelocity);

            // ������ ��ó�� �����ϸ� 
            if (Vector2.Distance(gameObject.transform.position, _bulletDestination.position) < 0.1f)
            {
                Destroy(gameObject, 0.1f);
            }
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // unit�̶� �浹��
        if (collision.gameObject.CompareTag("Unit"))
        {
            Debug.Log("Unit�̶� �浹��");
            // ##TODO : markerBulletExplosion�� �Լ����� 
            Destroy(gameObject, 0.1f);
        }

        // wall�̶� �浹 �� 
        if (collision.gameObject.CompareTag("Wall")) 
        {
            _iscollisionToWall = true;

            Debug.LogError("���̶� �浹 ");
            
            // max�� Ƚ���� �������� -> destory
            if (_currBounceCount == _bulletState.bulletBounceCount)
            {
                // ##TODO : markerBulletExplosion�� �Լ����� 
                Destroy(gameObject, 0.1f);
                return;
            }

            // ##TODO : ���� ������ �������� �� ƨ��µ� �����̸� �� ƨ 
            // ���� : �浹'����' velocity�� �����;� �ϴµ� �浹'����' velocity�� �����ͼ� ����ϸ� y���� 0�� ��µǴ°ſ���
            // �ذ� : ������Ʈ ���ȿ� �浹���������� velocity�� �����ؼ� �浹�� ����� (���� kinematic)

            //�Ի纤��
            Vector2 inDirection = lastVelocity;                             // ����(�ӵ�,���� ����) , ������ٵ��� velocity ���� �ӵ��� ������ ����
            Vector2 inNormal = collision.contacts[0].normal;                // �浹ü�� �븻����
            Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);   // �ݻ簢���ϱ�

            _bulletRidigBody.velocity = newVelocity * _bulletState.bulletSpeed;     // ���ѹݻ簢���� velocity �ְ�

            Debug.Log("�Էº��� + " + inDirection);
            Debug.Log("�浹ü �븻���� + " + inNormal);
            Debug.Log("���ο�ӵ� + " + newVelocity);

            // ƨ�� Ƚ�� +1
            _currBounceCount++;
        }
        
    }
}
