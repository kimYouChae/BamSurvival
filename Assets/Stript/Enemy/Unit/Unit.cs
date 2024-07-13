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
    [SerializeField] protected float _unitTimeStamp;   // ���� �� 0���� �ʱ�ȭ                                                 
    [SerializeField] protected float _searchRadious;       // �÷��̾� ���� ���� 

    [Header("===FSM===")]
    public HeadMachine _UnitHeadMachine;
    public FSM[] _UnitStateArr;
    [SerializeField] public UNIT_STATE _curr_UNITS_TATE;           // ���� enum
    [SerializeField] public UNIT_STATE _pre_UNITS_TATE;           // ���� enum 


    [Header("===LayerMask===")]
    public LayerMask _hitWallLayerMask;

    // ##TODO ��������
    [Header("===Ect Object===")]
    [SerializeField] protected GameObject _dangerLine;
    [SerializeField] protected LineRenderer _dangerBounceLine;

    // ������Ƽ
    public float unitSpeed      => _unitSpeed;
    public float searchRadious  => _searchRadious;
    public float unitTimeStamp { get => _unitTimeStamp; set{ _unitTimeStamp = value;} }

    private void Start()
    {
        _hitWallLayerMask = LayerMask.GetMask("Wall");
    }

    // ���� �ʱ�ȭ
    // ##TODO CVS�� ������ ���� �� �����ʿ���
    protected virtual void F_InitUnitUnitState() { }

    // attack ���� ������
    public virtual void F_UnitAttatk() { }

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
        _curr_UNITS_TATE = UNIT_STATE.Tracking;

        // Machine�� ���� �ֱ� 
        _UnitHeadMachine.HM_SetState(_UnitStateArr[(int)_curr_UNITS_TATE]);
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
    public void F_ChekchUnitHp() 
    {
        // hp�� 0���ϸ� true 
        if (_unitHp <= 0)
        {
            // Die�� ���º�ȭ
            F_ChangeState(UNIT_STATE.Die);
        }
    }

    public void F_UniTracking(Unit v_unit) 
    {
        // 1. �÷��̾� ����
        v_unit.gameObject.transform.position
            = Vector3.MoveTowards(v_unit.gameObject.transform.position,
                PlayerManager.instance.headMarkerTransfrom.position, v_unit.unitSpeed * Time.deltaTime);

        // 2. ���������� marker �� ����Ǹ�
        Collider2D[] _coll = Physics2D.OverlapCircleAll
            (v_unit.gameObject.transform.position, v_unit.searchRadious, PlayerManager.instance.markerLayer);

        if (_coll.Length > 0)
        {
            // ��������
            v_unit.F_ChangeState( UNIT_STATE.Attack );
        }
       
    }

    // ## TODO : line ������ player�� �� ���� (���� ���ʹ� �������� ) , �ӽ÷� �󽺿��� ���� 
    public void F_DangerMarkerShoot(Unit v_unit) 
    {
        Transform _unitTrs = v_unit.gameObject.transform;

        // ���� ���� ��ġ 
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
            Debug.LogError("���� ");

    }

    // ## TODO : Line �׸��°� �ΰ����� �ۿ� �� ��, ��ġ�� �ٽ� ������ �� 
    public void F_DangerLineBounce(Unit v_unit)
    {
        Transform _unitTrs = v_unit.gameObject.transform;

        // ���λ��� ��ġ 
        Vector3 _linePosition = new Vector3(_unitTrs.position.x, _unitTrs.position.y, -0.1f);
        // ���� ���� (���⺤��)
        Vector3 _lineDir = PlayerManager.instance.headMarkerTransfrom.position 
            - _unitTrs.position;

        LineRenderer _bounceLineInstance = Instantiate(_dangerBounceLine , _unitTrs.position , Quaternion.identity);

        _bounceLineInstance.positionCount = 1;                    // ������ ���� ���� ?
        _bounceLineInstance.SetPosition(0, _unitTrs.position);    // �ε��� 0���� �ִ� ��ġ�� ����   

        // N�� bounce
        for (int i = 1; i < 4; i++)
        {
            // Raycast
            RaycastHit2D _hit
                = Physics2D.Raycast(_linePosition, _lineDir * 100, 100f, _hitWallLayerMask);

            _bounceLineInstance.positionCount++;
            _bounceLineInstance.SetPosition(i, _hit.point);

            // ���� ��ġ ���� 
            _linePosition = _hit.point;
            // ���� ���� ���� : �Ի簢 �ݻ簢 ���ϱ� 
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
        Debug.Log("�ڷ�ƾ ����");
        // ������ ���� line renderer �׸���
        LineRenderer _temp = Instantiate(_dangerBounceLine, _unit.gameObject.transform);

        _temp.positionCount = 2;
        _temp.SetPosition(0, _unit.transform.position);     // ù��° ��ġ : unity
        _temp.SetPosition(1, PlayerManager.instance.headMarkerTransfrom.position);  // �ι�° ��ġ :  �÷��̾� ��ġ  

        // width �پ��� �ִϸ��̼� ����
        _temp.GetComponent<Animator>().SetBool("isActive", true);

        // �ִϸ��̼��� 0.6�� 
        yield return new WaitForSeconds(2.5f);
        Destroy(_temp.gameObject);

    }

    public void F_DrawLine() 
    {
        Debug.DrawRay(gameObject.transform.position , transform.up * 30f , Color.red);
    }
}
