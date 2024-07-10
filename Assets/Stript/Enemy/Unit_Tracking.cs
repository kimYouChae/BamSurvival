using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Unit_Tracking : FSM
{
    private Unit _unit;

    public Unit_Tracking(Unit v_unit)
    {
        this._unit = v_unit;
    }

    public override void FSM_Enter()
    {
        Debug.Log("Tracking Enter");
        _unit._curr_UNITS_TATE = UNIT_STATE.Tracking;
    }   

    public override void FSM_Excute()
    {
        // Die 전이 
        if(_unit.F_ChekchUnitHp())
        {
            //_unit.F_ChangeState(UNIT_STATE.Die);
        }

        // 1. 플레이어 추적
        _unit.gameObject.transform.position
            = Vector3.MoveTowards(_unit.gameObject.transform.position, 
                PlayerManager.instance.headMarkerTransfrom.position , _unit.unitSpeed * Time.deltaTime);

        // 2. 감지범위에 marker 가 검출되면
        
        Collider2D[] _coll = Physics2D.OverlapCircleAll
            (_unit.gameObject.transform.position, _unit.searchRadious , PlayerManager.instance.markerLayer );

        if ( _coll.Length >= 0 )
        {
            // 상태전이
            // _unit.F_ChangeState( UNIT_STATE.Attack );
        }
        
    }

    public override void FSM_Exit()
    {
        Debug.Log("Tracking Exit");
        _unit._pre_UNITS_TATE = UNIT_STATE.Tracking;
    }
}
