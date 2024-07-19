using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;   // regex 사용
using UnityEngine;

public class SkillCardDatabase : MonoBehaviour
{
    // csv 파싱 정규식 
    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    [Header("===Container===")]
    private Dictionary<CardTier, List<SkillCard>> _tierBySkillCard;  // 티어별 스킬카드
    private List<SkillCard> _tempSkillCard;

    // 프로퍼티
    public Dictionary<CardTier, List<SkillCard>> tierBySkillCard => _tierBySkillCard;

    private void Awake()
    {
        // cvs로 데이터 가져오기 
        F_InitSkillCard();

        // Dictionary 초기화
        F_InitDictionary();
    }

    private void F_InitSkillCard() 
    {
        // 초기화
        _tempSkillCard = new List<SkillCard>();

        // cvs 파일을 텍스트 파일로 가져오기 
        TextAsset textAsset = Resources.Load("SkillCard") as TextAsset;
        // 행 별로 자르기 
        string[] lines = Regex.Split( textAsset.text , LINE_SPLIT_RE);
        // 첫번째 행 자르기 
        string[] header = Regex.Split(lines[0] , SPLIT_RE);

        for (int i = 1; i < lines.Length; i++) 
        {
            // 행을 단어별로 자르기 
            string[] values = Regex.Split(lines[i], SPLIT_RE);

            // Skillcard 생성 
            SkillCard _card = new SkillCard(values);

            // 임시 리스트에 추가 
            _tempSkillCard.Add( _card );
        }

    }

    private void F_InitDictionary() 
    {
        // dictionary에 분류해서 넣기 
        _tierBySkillCard = new Dictionary<CardTier, List<SkillCard>>();

        foreach (SkillCard card in _tempSkillCard) 
        {
            CardTier _myTier = card.cardTier;
            
            // tier에 해당하는 리스트가 없으면 => 리스트 초기화 
            if(!_tierBySkillCard.ContainsKey(_myTier))
                _tierBySkillCard[_myTier] = new List<SkillCard>();

            // tier에 따른 리스트에 값 넣기 
            _tierBySkillCard[ _myTier ].Add( card );    
        }
    }

}
