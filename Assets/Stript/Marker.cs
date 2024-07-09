using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Marker : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _markerTransform;
    [SerializeField]
    private List<Quaternion> _merkerRotation;

    public List<Vector3> markerTransform => _markerTransform;
    public List<Quaternion> markerRotation => _merkerRotation;

    private void Awake()
    {
        // marker ���� 
        _markerTransform = new List<Vector3>();
        _merkerRotation = new List<Quaternion>();
    }

    private void FixedUpdate()
    {
        // �� ������ ��ġ���� ȸ������ ��Ƴ��� 
        markerTransform.Add(this.transform.position);
        markerRotation.Add(this.transform.rotation);
    }

    // MarkerData�� ����Ʈ �ʱ�ȭ
    public void F_clearDataList() 
    {
        // �����
        markerTransform.Clear();
        markerRotation.Clear();

        // ������� �ٷ� �� ������ ���� ��ġ,ȸ���� �ֱ� 
        markerTransform.Add(this.transform.position);
        markerRotation.Add(this.transform.rotation);
    }

}
