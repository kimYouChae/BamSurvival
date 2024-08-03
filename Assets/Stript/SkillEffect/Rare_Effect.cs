using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rare_PoisionBullet : SkillCard
{
    public override void F_SkillcardEffect()
    {
        Debug.Log(this.cardName);

        // 총알 폭발 시 사거리 안에 있는 unit에게 독 효과 
    }
}
public class Rare_RapidBarier : SkillCard
{
    public override void F_SkillcardEffect()
    {
        Debug.Log(this.cardName);

        // 쉴드 사용시간 10% 감소 (PlayerManager의 markerState 접근)
        for (int i = 0; i < PlayerManager.instance.F_MarkerListCount(); i++)
        {
            PlayerManager.instance.markers[i].markerState.markerShieldCoolTime
                += PlayerManager.instance.markers[i].markerState.markerShieldCoolTime * 0.1f;
        }
    }
}
public class Rare_IceBullet : SkillCard
{

    public override void F_SkillcardEffect()
    {
        Debug.Log(this.cardName);

        // 총알 폭발 시 사거리 안에 있는 unit에게 얼음효과 
    }
}