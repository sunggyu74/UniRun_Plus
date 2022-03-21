using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int count = 3; //생성할 발판 수

    //다음 배치까지의 시간 간격 최소값
    //다음 배치까지의 시간 간격 최대값
    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    //다음 배치까지의 시간 간격
    private float timeBetSpawn;
    //배치할 위치의 최소 y값
    //배치할 위치의 최대 y값
    public float yMin = -3.5f;
    public float yMax = 1.5f;
    //배치할 위치의 x값
    private float xPos = 20f; // 고정값 private 시간간격, x위치
    //생성한 발판들을 보관할 배열
    private GameObject[] platforms;
    //사용할 현재 순번의 발판 초기값 0
    private int currentIndex = 0;
    //초반에 생성한 발판을 화면 밖 위치에 지정
    private Vector2 poolPostion = new Vector2(0, -25);  // new키워드
    //마지막 배치 시점
    private float lastSpawnTime;


    // Start is called before the first frame update
    void Start()
    {
        //변수를 초기화 하고 사용할 발판을 미리 생성

        //count만큼의 공간을 가지는 새로운 발판 배열 생성
        platforms = new GameObject[count];
        //count만큼 루프하면서 발판 생성
        for (int i = 0; i < count; i++)
        {
            //platformPrefab을 원본으로 새 발판을
            //poolPosition 위치에 복제 생성
            //생성된 발판을 platforms 배열에 할당
            platforms[i] = Instantiate(platformPrefab, poolPostion, Quaternion.identity);
            //Quaternion.Euler(new Vector3(0, 0, 0)); //회전 적용 안 함 rotation (0,0,0)
            
        }
        //마지막 배치 시점 초기화
        lastSpawnTime = 0f;
        //다음번 배치까지의 시간 간격을 쵝화
        timeBetSpawn = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //순서를 돌아가며 주기적으로 발판을 배치

        //게임오버 상태에서는동작하지 않음
        if (GameManager.instance.isGameover) return;

        //마지막 배치 시점에서 timeBatSpawn이상 시간이 흘렀다면
        if (Time.time >= lastSpawnTime + timeBetSpawn) // 바로 실행
        {
            //기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time;
            //다음 배치 까지이 시간 간격을 timeBatSpawnMin,timeBatSpawnMax 
            //사이에서 랜덤 가져오기
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //배치할 위치의 높이를 yMin과 yMax 사이에서 랜덤 가져오기
            float yPos = Random.Range(yMin, yMax);

            //사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고
            //바로 즉시 다시 활성화
            //이때 발판의 Platform컴포넌트의 OnEnable 메서드가 실행됨
            platforms[currentIndex].SetActive(false);  //currentIndex 0 ~ 2
            platforms[currentIndex].SetActive(true);

            //현재 순번의 발판을 화면 오른쪽에 재배치
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            //vector값 입력은 new키워드
            //ScrollingObject 실행

            //순번 넘기기
            currentIndex++;

            //마지막 순번에 도달했다면
            if(currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
