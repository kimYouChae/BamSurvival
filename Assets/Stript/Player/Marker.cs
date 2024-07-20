using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    /// <summary>
    /// marker Prefab에 들어가있는 스크립트
    /// </summary>

    [SerializeField]
    private MarkerState _markerState;

    [SerializeField]
    private Slider _markerHpBar;

    // 프로퍼티
    public MarkerState markerState => _markerState;
    public Slider markerHpBar => _markerHpBar;

    private void Awake()
    {
    }

    private void Start()
    {
    }


    private void FixedUpdate()
    {

    }


}
