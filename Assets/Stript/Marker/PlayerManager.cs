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
    [SerializeField] private float _markerShieldCoolTime;       // marker 쉴드 쿨타임 
    [SerializeField] private float _markerShootCoolTime;        // 총알 발사 쿨타임 
    [SerializeField] private float _markerSearchRadious;        // unit 탐색 범위

    // 프로퍼티
    public int markerHp => _markerHp;
    public int markerMaxHp => _markerMaxHp;
    public float markerMoveSpeed => _markerMoveSpeed;
    public float markerShieldCoolTime => _markerShieldCoolTime;
    public float markerShootCoolTime => _markerShootCoolTime;
    public float markerSearchRadious => _markerSearchRadious;


    // 생성자 
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
    [SerializeField] private MarkerMovement _markerMovement;                    // marker 움직임
    [SerializeField] private MarkerShieldController _markerShieldController;    // 쉴드 컨트롤러
    [SerializeField] private MarkerBulletController _markerBulletController;    // 총알 컨트롤러
    [SerializeField] private MarkerExplosionConteroller _markerExplosionConteroller;    // 총알 폭발시 컨트롤러

    [Header("===Marker===")]
    [SerializeField] List<Marker> _markers;             // Marker 클래스 리스트에 저장
    [SerializeField] List<Slider> _markerHpBar;         // Marker의 hp바 

    [Header("===Layer===")]
    [SerializeField] private LayerMask _markerLayer;             // marker의 layer int 

    [Header("===Transform===")]
    [SerializeField] private Transform _markerHeadTrasform;      // marker head의 transform

    // 프로퍼티
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
        // state 초기화 
        F_InitMarkerState();

        _markerLayer = LayerMask.GetMask("Marker");

        _markerHeadTrasform = _markers[0].transform;

    }

    // marker State 초기화 
    private void F_InitMarkerState() 
    {
        // ## TODO : player에 따라 수정해야함 ( 추후 시작 player 종류가 많아지면 )

        for(int i = 0; i < _markers.Count; i++) 
        {
            _markers[i].markerState.F_SetMarkerState(10, 10, 1f, 5f, 5f, 5f);
        }
    }

    // skillcard의 효과 적용
    public void F_ApplyCardEffect(SkillCard v_Card ) 
    {
        v_Card.F_SkillcardEffect();
    }
}
