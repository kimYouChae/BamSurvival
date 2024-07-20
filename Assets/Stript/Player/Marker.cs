using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    /// <summary>
    /// marker Prefab에 들어가있는 스크립트
    /// </summary>

    [SerializeField]
    private MarkerState _markerState;

    [SerializeField]
    private Slider _markerHpBar;

    // 프로퍼티
    public MarkerState markerState => _markerState;
    public Slider markerHpBar => _markerHpBar;

    private void Start()
    {
        StartCoroutine(IE_MarkerUseShield());
    }

    IEnumerator IE_MarkerUseShield() 
    {
        // update 효과
        while (true) 
        {
            // shield 쿨타임 만큼 기다리기
            yield return new WaitForSeconds
                (PlayerManager.instance.markers[0].markerState.markerShieldCoolTime);

            // shield controller에서 저장된 함수 실행 
            PlayerManager.instance.markerShieldController._markerShieldUse();

        }
    }

}
