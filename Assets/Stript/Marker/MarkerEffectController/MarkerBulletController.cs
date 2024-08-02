using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bullet state
[System.Serializable]
public class BulletSate
{
    [SerializeField] int _bulletCount;    // �ѹ��� �����ϴ� �Ѿ� ����
    [SerializeField] float _bulletSpeed;    // �Ѿ� �ӵ�
    [SerializeField] int _bulletDamage;   // �Ѿ� ������ 
    [SerializeField] int _bulletSize;     // �Ѿ� ũ�� 
    [SerializeField] int _bulletBounceCount;  // �Ѿ� ƨ��� Ƚ�� 

    // ������Ƽ 
    public int bulletCount => _bulletCount;
    public float bulletSpeed => _bulletSpeed;
    public int bulletDamage => _bulletDamage;
    public int bulletSize => _bulletSize;
    public int bulletBounceCount => _bulletBounceCount;

    // ������
    public BulletSate(int v_cnt, float v_speed, int v_damage, int v_size, int _cnt)
    {
        this._bulletCount = v_cnt;
        this._bulletSpeed = v_speed;
        this._bulletDamage = v_damage;
        this._bulletSize = v_size;
        this._bulletBounceCount = _cnt;
    }
}

public class MarkerBulletController : MonoBehaviour
{
    /// <summary>
    ///  �Ѿ��� ��������Ʈ ��� �� �ص� �� �� 
    ///  
    /// ** Ư�� �Ѿ��� ī�带 ȹ�� �� �� , Ư��Ÿ���̸�? 
    /// �Ѿ˿� ��ũ��ƮaddComponent�ؼ� �浹�� �󸮰�...���ְ�...��� �ϸ� ���� ������ ?
    /// => ���߿� pooling ����� �� ������ �̸� �������� ����ϸ� �ɵ� 
    /// 
    /// </summary>

    [Header("===Bullet Sate===")]
    [SerializeField] private BulletSate _bulletSate;

    [Header("===basic Bullet Object===")]
    [SerializeField]
    private GameObject _basicBulletObject;

    // ������Ƽ
    public BulletSate bulletSate => _bulletSate;

    private void Start()
    {
        // �Ѿ� state �ʱ�ȭ 
        _bulletSate = new BulletSate(1, 3f, 1, 1, 1);

    }

    public void F_BasicBulletShoot(Transform v_muzzleTrs)  // �ѱ� ��ġ 
    {
        Debug.Log("�⺻ �Ѿ� �߻�");

        // unit collider Ž��
        Transform _destination;

        // �Ѿ� �߻� ���� ��ŭ 
        for (int i = 0; i < _bulletSate.bulletCount; i++)
        {
            //##TODO : ���⼭ ������ null 

            /*
            // unit�� �ݶ��̴� �˻� 
            Collider2D[] _coll = Physics2D.OverlapCircleAll
                (v_muzzleTrs.position, PlayerManager.instance.markers[0].markerState.markerSearchRadious, UnitManager.Instance.unitLayer);

            // ����Ȱ� ������ continuew
            if (_coll.Length <= 0)
                continue;

            // ����Ȱ� ���� ������ ����
            _destination = _coll[0].transform;

            // bullet ���� 
            GameObject _bullet = Instantiate(_basicBulletObject, v_muzzleTrs.position, Quaternion.identity);

            // bullet�� ������ �����ֱ�
            _bullet.GetComponent<BasicBullet>().bulletDestination = _destination;
            */

        }

    }

    public void F_ApplyBulletEffect() 
    {
        // ##TODO : ȿ������ �ڵ� ¥�� 
    }
}