using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MarkerState
{
    [SerializeField] private int _markerHp;              // marker Hp
    [SerializeField] private float _markerMoveSpeed;       // marker speed
    [SerializeField] private int _markerShieldCoolTime;  // marker 쉴드 쿨타임 

    // 프로퍼티
    public int markerHp => _markerHp;
    public float markerMoveSpeed => _markerMoveSpeed; 
    public int markerShieldCoolTime => _markerShieldCoolTime;

    // 생성자 
    public void F_SetMarkerState( int v_hp , float v_speed , int v_sCoolTime) 
    {
        this._markerHp = v_hp;
        this._markerMoveSpeed = v_speed;
        this._markerShieldCoolTime = v_sCoolTime;
    }
    
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("===Script===")]
    [SerializeField] private MarkerMovement _markerMovement;

    [Header("===Marker===")]
    [SerializeField] List<Marker> _markers;             // Marker 클래스 리스트에 저장
    [SerializeField] List<Slider> _markerHpBar;         // Marker의 hp바 

    [Header("===Layer===")]
    [SerializeField] private LayerMask _markerLayer;             // marker의 layer int 

    [Header("===Transform===")]
    [SerializeField] private Transform _markerHeadTrasform;      // marker head의 transform

    // 프로퍼티
    public MarkerMovement markerMovement => _markerMovement;
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
            _markers[i].markerState.F_SetMarkerState(10 , 3f , 5);
        }
    }
}
