using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField]
    private Transform _bulletDestination;

    // ������Ƽ
    public Transform bulletDestination { get => _bulletDestination; set { _bulletDestination = value; } }

    void Start()
    {
        
    }

    void Update()
    {
        if (_bulletDestination != null)
        {
            // bullet speed ��ŭ �������� �̵� 
            gameObject.transform.position = Vector3.Lerp
                (gameObject.transform.position,
                _bulletDestination.position,
                Time.deltaTime * PlayerManager.instance.markerBulletController.bulletSate.bulletSpeed);

            // ������ ��ó�� �����ϸ� 
            if (Vector2.Distance(gameObject.transform.position, _bulletDestination.position) < 0.1f) 
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            Debug.Log("Unit�̶� �浹��");
            Destroy(gameObject, 0.1f);
        }
    }
}
