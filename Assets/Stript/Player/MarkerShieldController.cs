using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerShieldController : MonoBehaviour
{
    [SerializeField]
    private GameObject _basicShield;

    private void Start()
    {
        // 쉴드 사용 코루틴 실행 
        StartCoroutine(F_MarkerShield());
    }

    IEnumerator F_MarkerShield() 
    {

        // update문 효과주기 위해서 
        while (true) 
        {
            // head의 coolTime만큼 기다리기 
            yield return new WaitForSeconds
                (PlayerManager.instance.markers[0].markerState.markerShieldCoolTime);

            for (int i = 0; i < PlayerManager.instance.markers.Count; i++) 
            {
                // 쉴드 생성
                GameObject _instanceShield = Instantiate( _basicShield , PlayerManager.instance.markers[0].transform);

                // ## TODO : 쉴드 사용을 한곳에서 관리할지, marker별로 관리할지 생각해봐야할듯 ?
                
            }

        }
    }
}
