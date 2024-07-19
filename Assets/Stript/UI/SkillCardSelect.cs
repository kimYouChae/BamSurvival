using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardSelect : MonoBehaviour
{
    [Header("===CardSelectTool===")]
    [SerializeField]
    private int _currCardIndex;             // ���� ���� �� ī�� �ε���  
    [SerializeField]
    private GameObject _cardSelectUi;       // ī�� ����ui �θ� 
    [SerializeField]
<<<<<<< Updated upstream:Assets/Stript/UI/SkillCardSelect.cs
    private GameObject[] _cardSelectCardUi;     // ī�� ���� ui�� ��ųī�� 

    private void F_CardStacking() 
=======
    private GameObject[] _cardList;        // ī�� ���� ui�� ��ųī�� 
    [SerializeField]
    private TextMeshProUGUI[] _cardNameList;    // ī�� �̸� ����Ʈ
    [SerializeField]
    private Image[] _cardImageList;             // ī�� �̹��� ����Ʈ
    [SerializeField]
    private TextMeshProUGUI[] _cardToopTipList; // ī�� ���� ����Ʈ 

    [Header("===CardTierSprit===")]
    [SerializeField]
    private Sprite[] _cardTierSprite;           // tier�� ���� ��������Ʈ 

    [SerializeField]
    private List<Tuple<CardTier, SkillCard>> _finalSelectCard; // �������� ���� �� ī�� 

    private void Start()
>>>>>>> Stashed changes:Assets/Stript/UI/CardSelectUI.cs
    {
    }

    void Update()
    {
        //  ##TODO : �ӽ÷� L ������ 
        if (Input.GetKeyDown(KeyCode.L))
        {
            F_ShowCard();
        }
    }

    // ī�� ui On  
    private void F_ShowCard()
    {
        // ui on 
        _cardSelectUi.SetActive(true);

        // idx �ʱ�ȭ
        _currCardIndex = -1;

        // ���� ���õ� card List �޾ƿ��� 
        _finalSelectCard = SkillCardManager.instance.F_FinalSelectCard();

        // ī�� ǥ���ϱ� 
        for (int i = 0; i < _finalSelectCard.Count; i++) 
        {
            CardTier _currTier = _finalSelectCard[i].Item1;
            SkillCard _currCard = _finalSelectCard[i].Item2;

            // tier�� ���� ī�� �̹��� �ٲٱ� 
            _cardList[i].GetComponent<Image>().sprite 
                = _cardTierSprite[(int)_finalSelectCard[i].Item1];

            // ī���̸�
            _cardNameList[i].text = _currCard.cardName;

            // ī�� idx�� �´� sprite
            _cardImageList[i].sprite = ResourceManager.instance.skillCardSprites[_currCard.cardIndex];

            // ī�� ����
            _cardToopTipList[i].text = _currCard.cardToolTip;
        }

    }

<<<<<<< Updated upstream:Assets/Stript/UI/SkillCardSelect.cs
=======
    // card idx �޾ƿ���
    public void F_SetCardIndex(int v_idx) 
    {
        Debug.Log(v_idx);

        // ���� �ε��� �� ���� �ε����� �ٸ���?
        if(_currCardIndex != v_idx) 
        {
            //## TODO : ũ��ȹٲ� ���� ?

            // ���õ� ī�� ũ�� Ű��� 
            _cardList[v_idx].GetComponent<RectTransform>().sizeDelta = new Vector2(400,600);

            // ���� ī�� ũ�� �۰�
            if(_currCardIndex != -1)
                _cardList[_currCardIndex].GetComponent<RectTransform>().sizeDelta = new Vector2(300,500);

            // ���� idx ���� 
            _currCardIndex = v_idx;

        }
        else 
        {
            // ui on 
            _cardSelectUi.SetActive(false);
        
            // ##TODO : Skillcard�� �´� ȿ�� �߰�
        
        }


    }

>>>>>>> Stashed changes:Assets/Stript/UI/CardSelectUI.cs
}
