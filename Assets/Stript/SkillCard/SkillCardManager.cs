using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardTier // ī�� Ƽ��
{
    Legendary,
    Epic,
    Rare,
    Common,
    Basic
}

public enum CardAbility // ī�� �ɷ�ġ 
{
    Shield,         // ������
    PlayerState,    // �÷��̾� ���� ��
    Bullet          // �Ѿ� ���� �� 
}

public class SkillCardManager : MonoBehaviour
{
    public static SkillCardManager instance;

    [Header("===Script====")]
    [SerializeField]
    private SkillCardDatabase _skillDatabase;

    // ������Ƽ
    public SkillCardDatabase SkillCardDatabase => _skillDatabase;

    private void Awake()
    {
        instance = this;
    }
}
