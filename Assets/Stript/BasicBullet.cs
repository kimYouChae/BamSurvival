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
    private Transform _bulletDestination;   // bullet 도착지
    [SerializeField]
    private BulletSate _bulletState;        // bulletState
    [SerializeField]
    private int _currBounceCount;           // 현재 this가 튕긴 횟수 
    [SerializeField]
    private Vector2 _bulletStartPosition;         // 입사-반사각 위한 시작 위치 
    [SerializeField]
    private bool _iscollisionToWall;            // 벽과 충돌했는지 ?
    [SerializeField]
    Vector2 lastVelocity;

    [Header("===Component===")]
    [SerializeField]private Rigidbody2D _bulletRidigBody;

    // 프로퍼티
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
            // bullet speed 만큼 도착지로 이동 
            /*
            gameObject.transform.position = Vector3.Lerp
                (gameObject.transform.position,
                _bulletDestination.position,
                Time.deltaTime * _bulletState.bulletSpeed);
            */

            Vector2 direction = (_bulletDestination.position - transform.position).normalized;  // 방향벡터의 정규화 (0~1사이의 수로 정규화됨)
            _bulletRidigBody.velocity = direction * _bulletState.bulletSpeed;                   // 방향벡터 * speed로 움직임

            lastVelocity = _bulletRidigBody.velocity;
            Debug.Log(this.name + "의 속도 : " + _bulletRidigBody.velocity);
            Debug.Log(this.name + "의 속도2 : " + lastVelocity);

            // 도착지 근처에 도착하면 
            if (Vector2.Distance(gameObject.transform.position, _bulletDestination.position) < 0.1f)
            {
                Destroy(gameObject, 0.1f);
            }
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // unit이랑 충돌시
        if (collision.gameObject.CompareTag("Unit"))
        {
            Debug.Log("Unit이랑 충돌함");
            // ##TODO : markerBulletExplosion의 함수실행 
            Destroy(gameObject, 0.1f);
        }

        // wall이랑 충돌 시 
        if (collision.gameObject.CompareTag("Wall")) 
        {
            _iscollisionToWall = true;

            Debug.LogError("벽이랑 충돌 ");
            
            // max랑 횟수가 같아지면 -> destory
            if (_currBounceCount == _bulletState.bulletBounceCount)
            {
                // ##TODO : markerBulletExplosion의 함수실행 
                Destroy(gameObject, 0.1f);
                return;
            }

            // ##TODO : 벽이 가만히 있을때는 잘 튕기는데 움직이면 안 튕 
            // 문제 : 충돌'직전' velocity를 가져와야 하는데 충돌'직후' velocity를 가져와서 출력하면 y값이 0이 출력되는거였듬
            // 해결 : 업데이트 문안에 충돌직전까지의 velocity를 저장해서 충돌시 사용함 (벽은 kinematic)

            //입사벡터
            Vector2 inDirection = lastVelocity;                             // 벡터(속도,방향 포함) , 리지드바디의 velocity 또한 속도와 방향을 포함
            Vector2 inNormal = collision.contacts[0].normal;                // 충돌체의 노말벡터
            Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);   // 반사각구하기

            _bulletRidigBody.velocity = newVelocity * _bulletState.bulletSpeed;     // 구한반사각으로 velocity 주가

            Debug.Log("입력벡터 + " + inDirection);
            Debug.Log("충돌체 노말벡터 + " + inNormal);
            Debug.Log("새로운속도 + " + newVelocity);

            // 튕김 횟수 +1
            _currBounceCount++;
        }
        
    }
}
