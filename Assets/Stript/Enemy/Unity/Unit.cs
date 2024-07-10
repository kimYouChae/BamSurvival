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
    /// Unit들의 부모 
    /// 
    /// 1. hp
    /// 2. 이동속도
    /// 3. abstract 공격
    /// 
    /// </summary>

    [Header("===Uint State===")]
    [SerializeField] protected int   _unitHp;           // hp
    [SerializeField] protected float _unitSpeed;        // speed 
    [SerializeField] protected float _unitAttackTime;   // 공격 지속시간
    [SerializeField] protected bool  _unitFinishedAttak; // 공격 끝                                                        
    [SerializeField] protected float _searchRadious;       // 플레이어 감지 범위 

    [Header("===FSM===")]
    public HeadMachine _UnitHeadMachine;
    public FSM[] _UnitStateArr;
    [SerializeField] private FSM _currState;          // 현재 상태 
    [SerializeField] private FSM _preState;           // 이전 상태 

    // 프로퍼티
    public float unitSpeed      => _unitSpeed;
    public float searchRadious  => _searchRadious;

    // 상태 초기화
    // ##TODO CVS로 데이터 관리 시 수정필요함
    protected virtual void F_InitUnitUnitState() { }

    // attack 동작 재정의
    protected virtual void F_UnitAttatk() { }

    // FSM 세팅 
    protected void F_InitUnitState( Unit v_standard ) 
    {
        // 헤드머신 생성 ( 자식에서 함수실행 , 자식 본인이 headmachine에 들어감 )
        _UnitHeadMachine = new HeadMachine(v_standard);

        // FSM array 생성
        _UnitStateArr = new FSM[System.Enum.GetValues(typeof(UNIT_STATE)).Length];

        _UnitStateArr[(int)UNIT_STATE.Idle]         = new Unit_Idle(v_standard);
        _UnitStateArr[(int)UNIT_STATE.Tracking]     = new Unit_Tracking(v_standard);
        _UnitStateArr[(int)UNIT_STATE.Attack]       = new Unit_Attack(v_standard);
        _UnitStateArr[(int)UNIT_STATE.Die]          = new Unit_Die(v_standard);

        // 현재상태 지정 
        _currState = _UnitStateArr[(int)UNIT_STATE.Tracking];

        // Machine에 상태 넣기 
        _UnitHeadMachine.HM_SetState(_currState);

    }

    // 현재 상태 진입 ( 1회 , Start에서 실행 )
    protected void F_CurrStateEnter() 
    {
        // head Machine의 enter
        _UnitHeadMachine.HM_StateEnter();
    }
 
    // 현재 상태 실행 ( update에서 실행 )
    protected void F_CurrStateExcute() 
    {
        // head Machine의 excute 
        _UnitHeadMachine.HM_StateExcute();
    }

    public void F_ChangeState( UNIT_STATE v_state ) 
    {
        // UNIT_STATE에 맞는 FSM으로 상태변화 
        // head Machine의 Change 
        _UnitHeadMachine.HM_ChangeState(_UnitStateArr[(int)v_state]);
    }

    // Unit hp 검사 
    public bool F_ChekchUnitHp() 
    {
        // hp가 0이하면 true 
        if (_unitHp <= 0)
            return true;

        return false;
    }

}
