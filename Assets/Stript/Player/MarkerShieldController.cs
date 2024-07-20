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
        // ���� ��� �ڷ�ƾ ���� 
        StartCoroutine(F_MarkerShield());
    }

    IEnumerator F_MarkerShield() 
    {

        // update�� ȿ���ֱ� ���ؼ� 
        while (true) 
        {
            // head�� coolTime��ŭ ��ٸ��� 
            yield return new WaitForSeconds
                (PlayerManager.instance.markers[0].markerState.markerShieldCoolTime);

            for (int i = 0; i < PlayerManager.instance.markers.Count; i++) 
            {
                // ���� ����
                GameObject _instanceShield = Instantiate( _basicShield , PlayerManager.instance.markers[0].transform);

                // ## TODO : ���� ����� �Ѱ����� ��������, marker���� �������� �����غ����ҵ� ?
                
            }

        }
    }
}
