# BamSurvival
플랫폼 : 모바일
장르 : 뱀서라이크

## Player
+ 움직임
  + 1. Snake는 항상 앞으로 감 
    + Translate(Vector3.forward)
  + 2. 플레이어는 A,D를 사용해서 좌우로만 움직임을 조정 
    + 조이스틱 움직임 ? 한손조작
+ 움직임 최적화
  + PlayerManager.cs
    + FixedUpdate() 사용
    + List<Marker> , 현재 생성된 snake 몸통 가지고 있음
    + F_headMoveControl()
      + 사용자의 horizontal 값을 받아 이동  
    + F_bodyMoveSegment()
      + head를 제외한 (Marker List에서 0번째) , 1~마지막 까지 순회하면서, Marker[ index - 1 ]위치에 있는 Marker에 접근
      + Marker안의 리스트에서 맨 첫번째 위치값과 회전값을 본인에게 적용
      + Marker[index-1]의 List를 clear()
  + Marker.cs
    + 각 몸통에 붙어있는 cs
    + FixedUpdate() 사용
    + List<Vector>, List<Quaternion> 으로 매 프레임 위치,회전값 담기
   
## Enemy
+ CSV로 데이터 관리
+ enemy 필수 state
  + hp : int
  + moveSpeed : float
+ 플레이어를 무조건 추적
  + 어떤식으로 추적할것인가 ? A* ? -> 맵을 어떤식으로 하면 좋을지 생각해야할듯 장애물이 있다 or 없다

## Map
