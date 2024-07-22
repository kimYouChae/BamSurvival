using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerShieldController : MonoBehaviour
{
    /// <summary>
    ///  ���� ���� deligate ����
    ///  (+) �� marker���� deligate ���� 
    /// </summary>

    [Header("===basic Shield Object===")]
    [SerializeField]
    private GameObject _basicShieldObject;

    // shield deligate 
    public delegate void del_MarkerShield(Transform v_parent);

    // deligate ����
    public del_MarkerShield del_markerShieldUse;

    private void Start()
    {
        // ��������Ʈ�� �⺻ ���� ��� �߰� 
        del_markerShieldUse += F_BasicShieldUse;
    }

    private void F_BasicShieldUse(Transform v_parent)
    {
        Debug.Log("�⺻ ���� ���");

        GameObject _ins = Instantiate(_basicShieldObject, v_parent);
        _ins.transform.localPosition = Vector3.zero;

        Destroy(_ins, 1f);
    }

}
