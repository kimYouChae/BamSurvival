using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("===Script===")]
    [SerializeField] private MarkerMovement _markerMovement;

    [Header("===Layer===")]
    [SerializeField] private LayerMask _markerLayer;             // markerÀÇ layer int 

    [Header("===Transform===")]
    [SerializeField] private Transform _markerHeadTrasform;      // marker headÀÇ transform

    // ½Ì±ÛÅæ
    public MarkerMovement markerMovement => _markerMovement;
    public LayerMask markerLayer => _markerLayer;   
    public Transform markerHeadTrasform => _markerHeadTrasform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _markerLayer = LayerMask.GetMask("Marker");

        _markerHeadTrasform = _markerMovement.markerHead;

    }


}
