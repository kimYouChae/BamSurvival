using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class Bug_Basic :  Unit 
{
    private void Start()
    {
        // ���� �ʱ�ȭ 
        F_InitUnitUnitState();
        
        // FSM ���� 
        F_InitUnitState(this);

        // FSM enter 
        F_CurrStateEnter();
    }

    private void Update()
    {
        // FSM excute 
        //F_CurrStateExcute(); 
    }

    protected override void F_InitUnitUnitState()
    {
        this._unitHp = 10;
        this._unitSpeed = 3f;
        this._unitAttackTime = 1f;
        this._searchRadious = 2f;
    }

    protected override void F_UnitAttatk()
    {
        Debug.Log(this.gameObject.name + " �� attack �Լ� ���� ");

        // ���� �ȳ��� 
        _unitFinishedAttak = false;

        float _temp = 0;
        _temp += Time.deltaTime;

        if(_temp < _unitAttackTime)
        {
            Debug.Log(this.gameObject.name +"�� ������ !! ");
        }
        else if(_temp >= _unitAttackTime )
        {
            Debug.Log(this.gameObject.name + " ���� �� ");

            // ���� ����
            _unitFinishedAttak = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, 1f);
    }
}
