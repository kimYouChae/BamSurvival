using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelectUI : MonoBehaviour
{
    [Header("===CardSelectTool===")]
    [SerializeField]
    private GameObject _cardSelectUi;       // ī�� ����ui �θ� 
    [SerializeField]
    private GameObject[] _cardSelectCardUi;     // ī�� ���� ui�� ��ųī�� 
    [SerializeField]
    private int _currCardIndex;

    private void F_CardStacking()
    {
        // ##TODO : ī�� �����̵� 
    }

    // card idx �޾ƿ���
    public void F_SetCardIndex(int v_idx) 
    {
        _currCardIndex = v_idx;

        Debug.Log(_currCardIndex);
    }
}
