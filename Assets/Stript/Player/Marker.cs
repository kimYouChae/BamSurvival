using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    /// <summary>
    /// marker Prefab�� ���ִ� ��ũ��Ʈ
    /// </summary>

    [SerializeField]
    private MarkerState _markerState;

    [SerializeField]
    private Slider _markerHpBar;

    // ������Ƽ
    public MarkerState markerState => _markerState;
    public Slider markerHpBar => _markerHpBar;

    private void Start()
    {
        StartCoroutine(IE_MarkerUseShield());
    }

    IEnumerator IE_MarkerUseShield() 
    {
        // update ȿ��
        while (true) 
        {
            // shield ��Ÿ�� ��ŭ ��ٸ���
            yield return new WaitForSeconds
                (PlayerManager.instance.markers[0].markerState.markerShieldCoolTime);

            // shield controller���� ����� �Լ� ���� 
            PlayerManager.instance.markerShieldController._markerShieldUse();

        }
    }

}
