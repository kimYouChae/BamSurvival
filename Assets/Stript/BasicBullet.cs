using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [Header("Bullet Field")]
    [SerializeField]
    private Transform _bulletDestination;   // bullet 도착지
    private int _currBounceCount;           // 쵱긴 총알 

    // 프로퍼티
    public Transform bulletDestination { get => _bulletDestination; set { _bulletDestination = value; } }

    void Start()
    {

    }

    void Update()
    {
        if (_bulletDestination != null)
        {
            // bullet speed 만큼 도착지로 이동 
            gameObject.transform.position = Vector3.Lerp
                (gameObject.transform.position,
                _bulletDestination.position,
                Time.deltaTime * PlayerManager.instance.markerBulletController.bulletSate.bulletSpeed);

            // 도착지 근처에 도착하면 
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
            Debug.Log("Unit이랑 충돌함");
            Destroy(gameObject, 0.1f);
        }
    }
}
