using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class Bug_Basic :  Unit 
{
    private void Start()
    {
        // 스탯 초기화 
        F_InitUnitUnitState();
        
        // FSM 세팅 
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
        Debug.Log(this.gameObject.name + " 의 attack 함수 실행 ");

        // 공격 안끝남 
        _unitFinishedAttak = false;

        float _temp = 0;
        _temp += Time.deltaTime;

        if(_temp < _unitAttackTime)
        {
            Debug.Log(this.gameObject.name +"가 공격중 !! ");
        }
        else if(_temp >= _unitAttackTime )
        {
            Debug.Log(this.gameObject.name + " 공격 끝 ");

            // 공격 끝남
            _unitFinishedAttak = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, 1f);
    }
}
