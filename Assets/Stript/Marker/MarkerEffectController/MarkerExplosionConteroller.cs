using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerExplosionConteroller : MonoBehaviour
{
    public delegate void del_BulletExplosion(GameObject obj );

    // deligate ����
    public del_BulletExplosion del_bulletExplosion;

    private void Start()
    {
        // ��������Ʈ�� �⺻ 
        del_bulletExplosion += F_BasicExplosionUse;    
    }

    // �浹 �� ����
    public void F_BulletExplosionStart(GameObject v_object) 
    {
        // ��������Ʈ ����
        del_bulletExplosion(v_object);
    }

    public void F_BasicExplosionUse(GameObject v_obj) 
    {
        // ��� : unit ������Ʈ
        if (v_obj.GetComponent<Unit>() == null)
            return;

        // unit�� hp ��� (bulletController�� bulletState�� damage ��ŭ) 
        v_obj.GetComponent<Unit>().
            F_GetDamage(PlayerManager.instance.markerBulletController.bulletSate.bulletDamage);
    }

    public void F_ApplyExplosionEffect(SkillCard v_card) 
    {
        // ##TODO : ȿ������ �ڵ� ¥�� 
        // ����ġ���̴� if���̴� �ӵ�Ἥ dictionary �߰� �� �Ű�����skillcard�� ���ؼ� ,,������..��¼��...
        // ó���̸� v_card�� ȿ�� �߰��ϰ� 
        // �ƴϸ� �� ��ũ��Ʈ�� �Լ��߰��ϰ� �װ� ��������Ʈ�� �ֱ� 

    }

}
