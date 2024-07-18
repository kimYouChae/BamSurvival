using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectUI : MonoBehaviour
{
    [Header("===CardSelectTool===")]
    [SerializeField]
    private GameObject _cardSelectUi;       // 카드 선택ui 부모 
    [SerializeField]
    private GameObject[] _cardSelectCardUi;     // 카드 선택 ui의 스킬카드 
    [SerializeField]
    private int _currCardIndex;

    private void F_CardStacking()
    {
        // ##TODO : 카드 슬라이딩 
    }

    // card idx 받아오기
    public void F_SetCardIndex(int v_idx) 
    {
        _currCardIndex = v_idx;

        Debug.Log(_currCardIndex);
    }
}
