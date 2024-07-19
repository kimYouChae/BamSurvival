using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;   // regex ���
using UnityEngine;

public class SkillCardDatabase : MonoBehaviour
{
    // csv �Ľ� ���Խ� 
    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    [Header("===Container===")]
    private Dictionary<CardTier, List<SkillCard>> _tierBySkillCard;  // Ƽ� ��ųī��
    private List<SkillCard> _tempSkillCard;

    // ������Ƽ
    public Dictionary<CardTier, List<SkillCard>> tierBySkillCard => _tierBySkillCard;

    private void Awake()
    {
        // cvs�� ������ �������� 
        F_InitSkillCard();

        // Dictionary �ʱ�ȭ
        F_InitDictionary();
    }

    private void F_InitSkillCard() 
    {
        // �ʱ�ȭ
        _tempSkillCard = new List<SkillCard>();

        // cvs ������ �ؽ�Ʈ ���Ϸ� �������� 
        TextAsset textAsset = Resources.Load("SkillCard") as TextAsset;
        // �� ���� �ڸ��� 
        string[] lines = Regex.Split( textAsset.text , LINE_SPLIT_RE);
        // ù��° �� �ڸ��� 
        string[] header = Regex.Split(lines[0] , SPLIT_RE);

        for (int i = 1; i < lines.Length; i++) 
        {
            // ���� �ܾ�� �ڸ��� 
            string[] values = Regex.Split(lines[i], SPLIT_RE);

            // Skillcard ���� 
            SkillCard _card = new SkillCard(values);

            // �ӽ� ����Ʈ�� �߰� 
            _tempSkillCard.Add( _card );
        }

    }

    private void F_InitDictionary() 
    {
        // dictionary�� �з��ؼ� �ֱ� 
        _tierBySkillCard = new Dictionary<CardTier, List<SkillCard>>();

        foreach (SkillCard card in _tempSkillCard) 
        {
            CardTier _myTier = card.cardTier;
            
            // tier�� �ش��ϴ� ����Ʈ�� ������ => ����Ʈ �ʱ�ȭ 
            if(!_tierBySkillCard.ContainsKey(_myTier))
                _tierBySkillCard[_myTier] = new List<SkillCard>();

            // tier�� ���� ����Ʈ�� �� �ֱ� 
            _tierBySkillCard[ _myTier ].Add( card );    
        }
    }

}
