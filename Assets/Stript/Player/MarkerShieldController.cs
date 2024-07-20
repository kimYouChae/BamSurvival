using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class MarkerShieldController : MonoBehaviour
{
    /// <summary>
    ///  deligate�� ���� ������ shield����, �� �Լ��� �����س���
    ///  �� marekr�� �����Ѵٸ� ?? ( marker�� playerManager�� ���ؼ� ���⿡ ���� )
    /// </summary>

    [Header("===basic Shield Object===")]
    [SerializeField]
    private GameObject _basicShieldObject;

    // shield deligate 
    public delegate void del_MarkerShield();

    // deligate ����
    public del_MarkerShield _markerShieldUse;

    private void Start()
    {
        // ��������Ʈ�� �⺻ ���� ��� �߰� 
        _markerShieldUse += F_BasicShieldUse;
    }

    private void F_BasicShieldUse() 
    {
        Debug.Log("�⺻ ���� ���");

        // ## TODO : �⺻ ���� �Լ� ����, ũ�� Ŀ���°� �ִϸ��̼����� �ص� �ɵ� ? 
    }

}
