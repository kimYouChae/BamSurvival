using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    /// <summary>
    /// marker Prefab�� ���ִ� ��ũ��Ʈ
    /// </summary>

    [SerializeField]
    private MarkerState _markerState;

    [SerializeField]
    private Slider _markerHpBar;

    // ������Ƽ
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
