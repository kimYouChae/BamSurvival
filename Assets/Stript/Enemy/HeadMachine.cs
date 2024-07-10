using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMachine
{
    // machine�� ���ư� ��ü
    private Unit _unit;

    private FSM _currState;     // ���� ����
    private FSM _preState;     // ���� ����  

    // ������
    public HeadMachine(Unit v_unit) 
    {
        this._unit = v_unit;
    }

    // ���� ���� ����
    public void HM_SetState(FSM v_fsm) 
    {
        this._currState = v_fsm;
    }

    // ���� ����
    public void HM_StateEnter() 
    {
        // currState�� Enter ����
        if(_currState != null )
            _currState.FSM_Enter();
    }

    public void HM_StateExcute() 
    {
        // _currState�� Excute ���� 
        if(_currState != null )
            _currState.FSM_Excute();
    }

    // ���� ����
    public void HM_ChangeState(FSM v_ChageState) 
    {
        if (_currState != null)
        {
            // �ٲ�� �� end ���� ����
            _currState.FSM_Excute();

            // �������� = ������� 
            _preState = _currState;

            // ������� = ���ε��� ����
            _currState = v_ChageState;

            // ���ε��� ������ enter ���� ����
            _currState.FSM_Enter();
        }
    }
   
}
