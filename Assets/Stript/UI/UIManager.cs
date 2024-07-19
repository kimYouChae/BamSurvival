using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField]
    private CardSelectUI _cardSelectUI;

    // 프로퍼티
    public CardSelectUI cardSelectUi => _cardSelectUI;

    private void Awake()
    {
        Instance = this;
    }

}
