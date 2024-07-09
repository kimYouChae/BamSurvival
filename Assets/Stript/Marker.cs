using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _markerTransform;
    [SerializeField]
    private List<Quaternion> _markerRotation;

    public List<Vector3> markerTransform => _markerTransform;
    public List<Quaternion> markerRotation => _markerRotation;

    private void Awake()
    {
        // marker ���� 
        _markerTransform = new List<Vector3>();
        _markerRotation = new List<Quaternion>();
    }

    private void Start()
    {
        _markerTransform.Add(gameObject.transform.position);
        _markerRotation.Add(gameObject.transform.rotation);
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
