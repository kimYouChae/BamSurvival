using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerShieldController : MonoBehaviour
{
    /// <summary>
    ///  deligate로 현재 실행할 shield동작, 즉 함수를 저장해놓고
    ///  각 marekr가 실행한다면 ?? ( marker은 playerManager을 통해서 여기에 접근 )
    /// </summary>

    [Header("===basic Shield Object===")]
    [SerializeField]
    private GameObject _basicShieldObject;

    public delegate void del_MarkerShield(Transform v_parent);

    // deligate 선언
    public del_MarkerShield del_markerShieldUse;

    private void Start()
    {
        // 델리게이트에 기본 쉴드 사용 추가 
        del_markerShieldUse += F_BasicShieldUse;
    }

    private void F_BasicShieldUse(Transform v_parent) 
    {
        GameObject _ins = Instantiate(_basicShieldObject, v_parent);
        _ins.transform.localPosition = Vector3.zero;

        Destroy(_ins, 1f);
    }
}
