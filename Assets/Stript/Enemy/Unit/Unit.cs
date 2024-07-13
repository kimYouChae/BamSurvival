using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] protected float _unitTimeStamp;   // 공격 시 0으로 초기화                                                 
    [SerializeField] protected float _searchRadious;       // 플레이어 감지 범위 

    [Header("===FSM===")]
    public HeadMachine _UnitHeadMachine;
    public FSM[] _UnitStateArr;
    [SerializeField] public UNIT_STATE _curr_UNITS_TATE;           // 현재 enum
    [SerializeField] public UNIT_STATE _pre_UNITS_TATE;           // 이전 enum 


    [Header("===LayerMask===")]
    public LayerMask _hitWallLayerMask;

    // ##TODO 수정예정
    [Header("===Ect Object===")]
    [SerializeField] protected GameObject _dangerLine;
    [SerializeField] protected LineRenderer _dangerBounceLine;

    // 프로퍼티
    public float unitSpeed      => _unitSpeed;
    public float searchRadious  => _searchRadious;
    public float unitTimeStamp { get => _unitTimeStamp; set{ _unitTimeStamp = value;} }

    private void Start()
    {
        _hitWallLayerMask = LayerMask.GetMask("Wall");
    }

    // 상태 초기화
    // ##TODO CVS로 데이터 관리 시 수정필요함
    protected virtual void F_InitUnitUnitState() { }

    // attack 동작 재정의
    public virtual void F_UnitAttatk() { }

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
        _curr_UNITS_TATE = UNIT_STATE.Tracking;

        // Machine에 상태 넣기 
        _UnitHeadMachine.HM_SetState(_UnitStateArr[(int)_curr_UNITS_TATE]);
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
    public void F_ChekchUnitHp() 
    {
        // hp가 0이하면 true 
        if (_unitHp <= 0)
        {
            // Die로 상태변화
            F_ChangeState(UNIT_STATE.Die);
        }
    }

    public void F_UniTracking(Unit v_unit) 
    {
        // 1. 플레이어 추적
        v_unit.gameObject.transform.position
            = Vector3.MoveTowards(v_unit.gameObject.transform.position,
                PlayerManager.instance.headMarkerTransfrom.position, v_unit.unitSpeed * Time.deltaTime);

        // 2. 감지범위에 marker 가 검출되면
        Collider2D[] _coll = Physics2D.OverlapCircleAll
            (v_unit.gameObject.transform.position, v_unit.searchRadious, PlayerManager.instance.markerLayer);

        if (_coll.Length > 0)
        {
            // 상태전이
            v_unit.F_ChangeState( UNIT_STATE.Attack );
        }
       
    }

    // ## TODO : line 생성은 player가 할 예정 (아직 몬스터느 생각없음 ) , 임시로 몬스에서 생성 
    public void F_DangerMarkerShoot(Unit v_unit) 
    {
        Transform _unitTrs = v_unit.gameObject.transform;

        // 라인 생성 위치 
        Vector3 _linePosition = new Vector3(_unitTrs.position.x , _unitTrs.position.y , -0.1f);
        
        // Raycast
        RaycastHit2D _hit
            = Physics2D.Raycast(_unitTrs.position, _unitTrs.up * 30, 30f , _hitWallLayerMask);
        if (_hit.collider != null )
        {
            GameObject _dangerLineClone = Instantiate(_dangerLine, _linePosition, Quaternion.identity);
            _dangerLineClone.GetComponent<DamgerLine>().EndPosition = _hit.transform.position;
        }
        else
            Debug.LogError("없음 ");

    }

    // ## TODO : Line 그리는게 두개까지 밖에 안 됨, 위치값 다시 봐야할 듯 
    public void F_DangerLineBounce(Unit v_unit)
    {
        Transform _unitTrs = v_unit.gameObject.transform;

        // 라인생성 위치 
        Vector3 _linePosition = new Vector3(_unitTrs.position.x, _unitTrs.position.y, -0.1f);
        // 라인 방향 (방향벡터)
        Vector3 _lineDir = PlayerManager.instance.headMarkerTransfrom.position 
            - _unitTrs.position;

        LineRenderer _bounceLineInstance = Instantiate(_dangerBounceLine , _unitTrs.position , Quaternion.identity);

        _bounceLineInstance.positionCount = 1;                    // 연결할 점의 갯수 ?
        _bounceLineInstance.SetPosition(0, _unitTrs.position);    // 인덱스 0번에 있는 위치값 세팅   

        // N번 bounce
        for (int i = 1; i < 4; i++)
        {
            // Raycast
            RaycastHit2D _hit
                = Physics2D.Raycast(_linePosition, _lineDir * 100, 100f, _hitWallLayerMask);

            _bounceLineInstance.positionCount++;
            _bounceLineInstance.SetPosition(i, _hit.point);

            // 시작 위치 수정 
            _linePosition = _hit.point;
            // 라인 방향 수정 : 입사각 반사각 구하기 
            _lineDir = Vector3.Reflect( _lineDir , _hit.normal);
        }
    }

    public void F_StartCorutine(Unit v_unit) 
    {
        StopAllCoroutines();
        StartCoroutine(F_DangerLineAndShoot(v_unit));
    }

    IEnumerator F_DangerLineAndShoot( Unit _unit ) 
    {
        Debug.Log("코루틴 시작");
        // 데미지 들어가는 line renderer 그리기
        LineRenderer _temp = Instantiate(_dangerBounceLine, _unit.gameObject.transform);

        _temp.positionCount = 2;
        _temp.SetPosition(0, _unit.transform.position);     // 첫번째 위치 : unity
        _temp.SetPosition(1, PlayerManager.instance.headMarkerTransfrom.position);  // 두번째 위치 :  플레이어 위치  

        // width 줄어드는 애니메이션 실행
        _temp.GetComponent<Animator>().SetBool("isActive", true);

        // 애니메이션이 0.6초 
        yield return new WaitForSeconds(2.5f);
        Destroy(_temp.gameObject);

    }

    public void F_DrawLine() 
    {
        Debug.DrawRay(gameObject.transform.position , transform.up * 30f , Color.red);
    }
}
