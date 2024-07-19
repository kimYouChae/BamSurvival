using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillCard
{
    private CardTier _cardTier;
    private CardAbility _cardAbility;
    private string _cardName;
    private string _cardToolTip;

    // 프로퍼티
    public CardTier cardTier => _cardTier;
    public CardAbility cardAbility => _cardAbility; 
    public string cardName => _cardName;
    public string cardToolTip => _cardToolTip;

    // 생성자
    public SkillCard( string[] v_str ) 
    {
        // 1. card tier ( string to enum )
        this._cardTier = (CardTier)Enum.Parse( typeof(CardTier), v_str[0]);
        // 2. card ability (string to enum)
        this._cardAbility = (CardAbility)Enum.Parse(typeof(CardAbility), v_str[1]);
        // 3. card name
        this._cardName = v_str[2];
        // 4. card tool tip
        this._cardToolTip = v_str[3];
    }

}
