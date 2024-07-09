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
        // marker 생성 
        _markerTransform = new List<Vector3>();
        _merkerRotation = new List<Quaternion>();
    }

    private void FixedUpdate()
    {
        // 매 프레임 위치값과 회전값을 담아놓음 
        markerTransform.Add(this.transform.position);
        markerRotation.Add(this.transform.rotation);
    }

    // MarkerData의 리스트 초기화
    public void F_clearDataList() 
    {
        // 지우기
        markerTransform.Clear();
        markerRotation.Clear();

        // 지운다음 바로 뒷 몸통이 따라갈 위치,회전값 넣기 
        markerTransform.Add(this.transform.position);
        markerRotation.Add(this.transform.rotation);
    }

}
