using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bullet state
[System.Serializable]
public class BulletSate
{
    [SerializeField] int _bulletCount;    // �ѹ��� �����ϴ� �Ѿ� ����
    [SerializeField] float _bulletSpeed;    // �Ѿ� �ӵ�
    [SerializeField] float _bulletDamage;   // �Ѿ� ������ 
    [SerializeField] float _bulletSize;     // �Ѿ� ũ�� 
    [SerializeField] int _bulletBounceCount;  // �Ѿ� ƨ��� Ƚ�� 

    // ������Ƽ 
    public int bulletCount { get => _bulletCount; set { _bulletCount = value; } }
    public float bulletSpeed { get => _bulletSpeed; set { _bulletSpeed = value; } }
    public float bulletDamage { get => _bulletDamage; set { _bulletDamage = value; } }
    public float bulletSize { get => _bulletSize; set { _bulletSize = value; } }
    public int bulletBounceCount { get => _bulletBounceCount; set { _bulletBounceCount = value; } }

    // ������
    public BulletSate(int v_cnt, float v_speed, float v_damage, float v_size, int _cnt)
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
        _bulletSate = new BulletSate(1, 3f, 1f, 1f, 1);

    }

    public void F_BasicBulletShoot(Transform v_muzzleTrs)  // �ѱ� ��ġ 
    {
        Debug.Log("�⺻ �Ѿ� �߻�");

        // unit collider Ž��
        Transform _destination;

        //##TODO : ���⼭ ������ null 
        // unit�� �ݶ��̴� �˻� 
        Collider2D[] _coll = Physics2D.OverlapCircleAll
            (v_muzzleTrs.position, PlayerManager.instance.markers[0].markerState.markerSearchRadious, UnitManager.Instance.unitLayer);
        
        // ����Ȱ� ������ ����
        if (_coll.Length <= 0) 
            return;

        // �Ѿ� �߻� ���� ��ŭ 
        for (int i = 0; i < _bulletSate.bulletCount; i++)
        {
            // ����Ȱ� ���� ������ ����
            _destination = _coll[0].transform;

            // bullet ���� 
            GameObject _bullet = Instantiate(_basicBulletObject, v_muzzleTrs.position, Quaternion.identity);

            // bullet�� ������ �����ֱ�
            _bullet.GetComponent<BasicBullet>().bulletDestination = _destination;
            

        }

    }

    public void F_ApplyBulletEffect(SkillCard v_card) 
    {
        // ##TODO : ȿ������ �ڵ� ¥�� 
    }
}