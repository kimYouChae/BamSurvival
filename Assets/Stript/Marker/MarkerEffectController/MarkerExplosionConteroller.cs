using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerExplosionConteroller : MonoBehaviour
{
    public delegate void del_BulletExplosion(GameObject obj );

    // deligate 선언
    public del_BulletExplosion del_bulletExplosion;

    private void Start()
    {
        // 델리게이트에 기본 
        del_bulletExplosion += F_BasicExplosionUse;    
    }

    // 충돌 시 시작
    public void F_BulletExplosionStart(GameObject v_object) 
    {
        // 델리게이트 실행
        del_bulletExplosion(v_object);
    }

    public void F_BasicExplosionUse(GameObject v_obj) 
    {
        // 대상 : unit 오브젝트
        if (v_obj.GetComponent<Unit>() == null)
            return;

        // unit의 hp 깎기 (bulletController의 bulletState의 damage 만큼) 
        v_obj.GetComponent<Unit>().
            F_GetDamage(PlayerManager.instance.markerBulletController.bulletSate.bulletDamage);
    }

    public void F_ApplyExplosionEffect(SkillCard v_card) 
    {
        // ##TODO : 효과적용 코드 짜기 
        // 스위치문이던 if문이던 머든써서 dictionary 추가 후 매개변수skillcard랑 비교해서 ,,갯수가..어쩌고...
        // 처음이면 v_card의 효과 추가하고 
        // 아니면 이 스크립트에 함수추가하고 그거 델리게이트에 넣기 

    }

}
