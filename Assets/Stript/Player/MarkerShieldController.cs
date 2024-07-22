using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerShieldController : MonoBehaviour
{
    /// <summary>
    ///  쉴드 실행 deligate 관리
    ///  (+) 각 marker에서 deligate 실행 
    /// </summary>

    [Header("===basic Shield Object===")]
    [SerializeField]
    private GameObject _basicShieldObject;

    // shield deligate 
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
        Debug.Log("기본 쉴드 사용");

        GameObject _ins = Instantiate(_basicShieldObject, v_parent);
        _ins.transform.localPosition = Vector3.zero;

        Destroy(_ins, 1f);
    }

}
