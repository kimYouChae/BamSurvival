using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("===Ratio / 0에서 1 사이 ===")]
    [SerializeField] private float _basicRatio;
    [SerializeField] private float _commonRatio;
    [SerializeField] private float _rareRatio;
    [SerializeField] private float _epicRatio;
    [SerializeField] private float _legendaryRatio;

    // 프로퍼티
    public float BasicRatio => _basicRatio;
    public float CommonRatio => _commonRatio;
    public float RareRatio => _rareRatio;
    public float EpicRatio => _epicRatio;
    public float LegaryRatio => _legendaryRatio;

    private void Awake()
    {
        instance = this;
    }
}
