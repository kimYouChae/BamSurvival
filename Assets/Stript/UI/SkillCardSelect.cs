using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCardSelect : MonoBehaviour
{
    [Header("===CardSelectTool===")]
    [SerializeField]
    private GameObject _cardSelectUi;       // 카드 선택ui 부모 
    [SerializeField]
    private GameObject[] _cardSelectCardUi;     // 카드 선택 ui의 스킬카드 

    private void F_CardStacking() 
    {
        // ##TODO : 카드 슬라이딩 
    }

}
