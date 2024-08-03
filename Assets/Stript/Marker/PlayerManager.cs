using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MarkerState
{
    [SerializeField] private int _markerHp;                     // marker Hp
    [SerializeField] private int _markerMaxHp;                  // marker max hp
    [SerializeField] private float _markerMoveSpeed;            // marker speed
    [SerializeField] private float _markerShieldCoolTime;       // marker ���� ��Ÿ�� 
    [SerializeField] private float _markerShootCoolTime;        // �Ѿ� �߻� ��Ÿ�� 
    [SerializeField] private float _markerSearchRadious;        // unit Ž�� ����

    // ������Ƽ
    public int markerHp => _markerHp;
    public int markerMaxHp => _markerMaxHp;
    public float markerMoveSpeed => _markerMoveSpeed;
    public float markerShieldCoolTime => _markerShieldCoolTime;
    public float markerShootCoolTime => _markerShootCoolTime;
    public float markerSearchRadious => _markerSearchRadious;


    // ������ 
    public void F_SetMarkerState(int v_hp, int v_maxHp , float v_speed, float v_sCoolTime, float v_bCoolTime, float v_search)
    {
        this._markerHp = v_hp;
        this._markerMaxHp = v_maxHp;
        this._markerMoveSpeed = v_speed;
        this._markerShieldCoolTime = v_sCoolTime;
        this._markerShootCoolTime = v_bCoolTime;
        this._markerSearchRadious = v_search;
    }
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("===Script===")]
    [SerializeField] private MarkerMovement _markerMovement;                    // marker ������
    [SerializeField] private MarkerShieldController _markerShieldController;    // ���� ��Ʈ�ѷ�
    [SerializeField] private MarkerBulletController _markerBulletController;    // �Ѿ� ��Ʈ�ѷ�
    [SerializeField] private MarkerExplosionConteroller _markerExplosionConteroller;    // �Ѿ� ���߽� ��Ʈ�ѷ�

    [Header("===Marker===")]
    [SerializeField] List<Marker> _markers;             // Marker Ŭ���� ����Ʈ�� ����
    [SerializeField] List<Slider> _markerHpBar;         // Marker�� hp�� 

    [Header("===Layer===")]
    [SerializeField] private LayerMask _markerLayer;             // marker�� layer int 

    [Header("===Transform===")]
    [SerializeField] private Transform _markerHeadTrasform;      // marker head�� transform

    // ������Ƽ
    public MarkerMovement markerMovement => _markerMovement;
    public MarkerShieldController markerShieldController => _markerShieldController;
    public MarkerBulletController markerBulletController => _markerBulletController;
    public MarkerExplosionConteroller markerExplosionConteroller => _markerExplosionConteroller;
    public LayerMask markerLayer => _markerLayer;   
    public Transform markerHeadTrasform => _markerHeadTrasform;
    public List<Marker> markers => _markers;
    public List<Slider> markerHpBar => _markerHpBar;
   

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // state �ʱ�ȭ 
        F_InitMarkerState();

        _markerLayer = LayerMask.GetMask("Marker");

        _markerHeadTrasform = _markers[0].transform;

    }

    // marker State �ʱ�ȭ 
    private void F_InitMarkerState() 
    {
        // ## TODO : player�� ���� �����ؾ��� ( ���� ���� player ������ �������� )

        for(int i = 0; i < _markers.Count; i++) 
        {
            _markers[i].markerState.F_SetMarkerState(10, 10, 1f, 5f, 5f, 5f);
        }
    }

    // skillcard�� ȿ�� ����
    public void F_ApplyCardEffect(SkillCard v_Card ) 
    {
        v_Card.F_SkillcardEffect();
    }
}
