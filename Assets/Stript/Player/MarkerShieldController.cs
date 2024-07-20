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

    // shield deligate 
    public delegate void del_MarkerShield();

    // deligate 선언
    public del_MarkerShield _markerShieldUse;

    private void Start()
    {
        // 델리게이트에 기본 쉴드 사용 추가 
        _markerShieldUse += F_BasicShieldUse;
    }

    private void F_BasicShieldUse() 
    {
        Debug.Log("기본 쉴드 사용");

        // ## TODO : 기본 쉴드 함수 제작, 크기 커지는건 애니메이션으로 해도 될듯 ? 
    }

}
