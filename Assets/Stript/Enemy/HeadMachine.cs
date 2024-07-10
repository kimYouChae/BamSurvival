using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMachine
{
    // machine이 돌아갈 주체
    private Unit _unit;

    private FSM _currState;     // 현재 상태
    private FSM _preState;     // 이전 상태  

    // 생성자
    public HeadMachine(Unit v_unit) 
    {
        this._unit = v_unit;
    }

    // 현재 상태 세팅
    public void HM_SetState(FSM v_fsm) 
    {
        this._currState = v_fsm;
    }

    // 상태 진입
    public void HM_StateEnter() 
    {
        // currState의 Enter 실행
        if(_currState != null )
            _currState.FSM_Enter();
    }

    public void HM_StateExcute() 
    {
        // _currState의 Excute 실행 
        if(_currState != null )
            _currState.FSM_Excute();
    }

    // 상태 변경
    public void HM_ChangeState(FSM v_ChageState) 
    {
        if (_currState != null)
        {
            // 바뀌기 전 end 동작 실행
            _currState.FSM_Excute();

            // 이전상태 = 현재상태 
            _preState = _currState;

            // 현재상태 = 새로들어온 상태
            _currState = v_ChageState;

            // 새로들어온 상태의 enter 동작 실행
            _currState.FSM_Enter();
        }
    }
   
}
