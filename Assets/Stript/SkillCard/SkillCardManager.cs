using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTier // 카드 티어
{
    Legendary,
    Epic,
    Rare,
    Common,
    Basic
}

public enum CardAbility // 카드 능력치 
{
    Shield,         // 쉴드형
    PlayerState,    // 플레이어 스탯 형
    Bullet          // 총알 스탯 형 
}

public class SkillCardManager : MonoBehaviour
{
    public static SkillCardManager instance;

    [Header("===Script====")]
    [SerializeField]
    private SkillCardDatabase _skillDatabase;

    // 프로퍼티
    public SkillCardDatabase SkillCardDatabase => _skillDatabase;

    private void Awake()
    {
        instance = this;
    }
}
