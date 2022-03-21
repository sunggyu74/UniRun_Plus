using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int count = 3; //������ ���� ��

    //���� ��ġ������ �ð� ���� �ּҰ�
    //���� ��ġ������ �ð� ���� �ִ밪
    public float timeBetSpawnMin = 1.25f;
    public float timeBetSpawnMax = 2.25f;
    //���� ��ġ������ �ð� ����
    private float timeBetSpawn;
    //��ġ�� ��ġ�� �ּ� y��
    //��ġ�� ��ġ�� �ִ� y��
    public float yMin = -3.5f;
    public float yMax = 1.5f;
    //��ġ�� ��ġ�� x��
    private float xPos = 20f; // ������ private �ð�����, x��ġ
    //������ ���ǵ��� ������ �迭
    private GameObject[] platforms;
    //����� ���� ������ ���� �ʱⰪ 0
    private int currentIndex = 0;
    //�ʹݿ� ������ ������ ȭ�� �� ��ġ�� ����
    private Vector2 poolPostion = new Vector2(0, -25);  // newŰ����
    //������ ��ġ ����
    private float lastSpawnTime;


    // Start is called before the first frame update
    void Start()
    {
        //������ �ʱ�ȭ �ϰ� ����� ������ �̸� ����

        //count��ŭ�� ������ ������ ���ο� ���� �迭 ����
        platforms = new GameObject[count];
        //count��ŭ �����ϸ鼭 ���� ����
        for (int i = 0; i < count; i++)
        {
            //platformPrefab�� �������� �� ������
            //poolPosition ��ġ�� ���� ����
            //������ ������ platforms �迭�� �Ҵ�
            platforms[i] = Instantiate(platformPrefab, poolPostion, Quaternion.identity);
            //Quaternion.Euler(new Vector3(0, 0, 0)); //ȸ�� ���� �� �� rotation (0,0,0)
            
        }
        //������ ��ġ ���� �ʱ�ȭ
        lastSpawnTime = 0f;
        //������ ��ġ������ �ð� ������ ��ȭ
        timeBetSpawn = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        //������ ���ư��� �ֱ������� ������ ��ġ

        //���ӿ��� ���¿����µ������� ����
        if (GameManager.instance.isGameover) return;

        //������ ��ġ �������� timeBatSpawn�̻� �ð��� �귶�ٸ�
        if (Time.time >= lastSpawnTime + timeBetSpawn) // �ٷ� ����
        {
            //��ϵ� ������ ��ġ ������ ���� �������� ����
            lastSpawnTime = Time.time;
            //���� ��ġ ������ �ð� ������ timeBatSpawnMin,timeBatSpawnMax 
            //���̿��� ���� ��������
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //��ġ�� ��ġ�� ���̸� yMin�� yMax ���̿��� ���� ��������
            float yPos = Random.Range(yMin, yMax);

            //����� ���� ������ ���� ���� ������Ʈ�� ��Ȱ��ȭ�ϰ�
            //�ٷ� ��� �ٽ� Ȱ��ȭ
            //�̶� ������ Platform������Ʈ�� OnEnable �޼��尡 �����
            platforms[currentIndex].SetActive(false);  //currentIndex 0 ~ 2
            platforms[currentIndex].SetActive(true);

            //���� ������ ������ ȭ�� �����ʿ� ���ġ
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            //vector�� �Է��� newŰ����
            //ScrollingObject ����

            //���� �ѱ��
            currentIndex++;

            //������ ������ �����ߴٸ�
            if(currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
