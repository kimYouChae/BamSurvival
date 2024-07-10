using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNIT_STATE 
{ 
    Idle,
    Tracking,
    Attack,
    Die
}

public class Unit : MonoBehaviour
{
    /// <summary>
    /// Unit���� �θ� 
    /// 
    /// 1. hp
    /// 2. �̵��ӵ�
    /// 3. abstract ����
    /// 
    /// </summary>

    [Header("===Uint State===")]
    [SerializeField] protected int   _unitHp;           // hp
    [SerializeField] protected float _unitSpeed;        // speed 
    [SerializeField] protected float _unitAttackTime;   // ���� ���ӽð�
    [SerializeField] protected bool  _unitFinishedAttak; // ���� ��                                                        
    [SerializeField] protected float _searchRadious;       // �÷��̾� ���� ���� 

    [Header("===FSM===")]
    public HeadMachine _UnitHeadMachine;
    public FSM[] _UnitStateArr;
    [SerializeField] private FSM _currState;          // ���� ���� 
    [SerializeField] private FSM _preState;           // ���� ���� 

    // ������Ƽ
    public float unitSpeed      => _unitSpeed;
    public float searchRadious  => _searchRadious;

    // ���� �ʱ�ȭ
    // ##TODO CVS�� ������ ���� �� �����ʿ���
    protected virtual void F_InitUnitUnitState() { }

    // attack ���� ������
    protected virtual void F_UnitAttatk() { }

    // FSM ���� 
    protected void F_InitUnitState( Unit v_standard ) 
    {
        // ���ӽ� ���� ( �ڽĿ��� �Լ����� , �ڽ� ������ headmachine�� �� )
        _UnitHeadMachine = new HeadMachine(v_standard);

        // FSM array ����
        _UnitStateArr = new FSM[System.Enum.GetValues(typeof(UNIT_STATE)).Length];

        _UnitStateArr[(int)UNIT_STATE.Idle]         = new Unit_Idle(v_standard);
        _UnitStateArr[(int)UNIT_STATE.Tracking]     = new Unit_Tracking(v_standard);
        _UnitStateArr[(int)UNIT_STATE.Attack]       = new Unit_Attack(v_standard);
        _UnitStateArr[(int)UNIT_STATE.Die]          = new Unit_Die(v_standard);

        // ������� ���� 
        _currState = _UnitStateArr[(int)UNIT_STATE.Tracking];

        // Machine�� ���� �ֱ� 
        _UnitHeadMachine.HM_SetState(_currState);

    }

    // ���� ���� ���� ( 1ȸ , Start���� ���� )
    protected void F_CurrStateEnter() 
    {
        // head Machine�� enter
        _UnitHeadMachine.HM_StateEnter();
    }
 
    // ���� ���� ���� ( update���� ���� )
    protected void F_CurrStateExcute() 
    {
        // head Machine�� excute 
        _UnitHeadMachine.HM_StateExcute();
    }

    public void F_ChangeState( UNIT_STATE v_state ) 
    {
        // UNIT_STATE�� �´� FSM���� ���º�ȭ 
        // head Machine�� Change 
        _UnitHeadMachine.HM_ChangeState(_UnitStateArr[(int)v_state]);
    }

    // Unit hp �˻� 
    public bool F_ChekchUnitHp() 
    {
        // hp�� 0���ϸ� true 
        if (_unitHp <= 0)
            return true;

        return false;
    }

}
