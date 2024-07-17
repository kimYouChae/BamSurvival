using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Attack : FSM
{
    private Unit _unit;

    public Unit_Attack(Unit v_unit)
    {
        this._unit = v_unit;
    }

    public override void FSM_Enter()
    {
        Debug.Log("Attack Enter");
        _unit._curr_UNITS_TATE = UNIT_STATE.Attack;

        // 0. basic Line 생성  
        //_unit.F_DangerMarkerShoot(_unit);
        // 1. bouncec Line 생성 
        //_unit.F_DangerLineBounce(_unit);
        // 2. line Renderer에 애니메이션 넣어서 실행하기 
        _unit.F_StartCorutine(_unit);

        // 공격 시 0으로 초기화 
        _unit.unitTimeStamp = 0f;
    }

    public override void FSM_Excute()
    {
        // Unit hp 검사, 0 이하 시 Die로 상태변화
        _unit.F_ChekchUnitHp();
        
        // 각 Unit 마다 다른 Attack 동작 
        _unit.F_UnitAttatk();
    }

    public override void FSM_Exit()
    {
        Debug.Log("Attack exit");
        _unit._pre_UNITS_TATE = UNIT_STATE.Attack;
    }


}
