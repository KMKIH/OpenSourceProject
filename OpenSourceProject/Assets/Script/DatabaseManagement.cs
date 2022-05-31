
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections;

public class DatabaseManagement : MonoBehaviour
{
    DynamoDBContext context;
    AmazonDynamoDBClient DBclient;
    CognitoAWSCredentials credentials;

    [SerializeField] GameObject uploadingImage;
    [SerializeField] GameObject downloadingImage;
    [SerializeField] GameObject succesImage;
    [SerializeField] GameObject failImage;
    [SerializeField] GameObject canNotTouch;

    float limitTime = 10f;
    static int maxMapFileNum = 10;
    private void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        credentials = new CognitoAWSCredentials("ap-northeast-2:ddbf3c37-0b48-4981-8a6d-f10691ebf48a", RegionEndpoint.APNortheast2);
        DBclient = new AmazonDynamoDBClient(credentials, RegionEndpoint.APNortheast2);
        context = new DynamoDBContext(DBclient);

        // 아래 2개의 함수의 위치를 바꿔야함
    }
    [DynamoDBTable("PlayerData")]
    public class AWSDATA
    {
        [DynamoDBHashKey] // Hash key.
        public string id { get; set; }
        [DynamoDBProperty]
        public string data { get; set; }
    }

    int idx = 0;
    int countList;
    int maxList;
    public void UploadAllMapData()
    {
        // 파일 이름 받아오기
        List<Data> list = FindObjectOfType<DirectorySpawner>().fileList;
        string fileName_ = "fileName";
        idx = 0;
        countList = 0;
        maxList = list.Count;
        uploadingImage.SetActive(true);
        canNotTouch.SetActive(true);
        foreach (Data data in list)
        {
            UploadMapData(data.FileName, fileName_);
        }
        StartCoroutine(WaitCompleteUpload());
    }
    private void UploadMapData(string fileName, string fileName_) //캐릭터 정보를 DB에 올리기
    {
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // 여러개의 맵 파일 -> 클래스로 묶어서 한번에 업로드 / 다운로드
        AWSDATA aWSDATA = new AWSDATA();
        // aWSDATA.id = fileName;
        fileName_ += idx++;
        aWSDATA.id = fileName_;

        ///////////////////////////////////////////////////////////////////
        // 파일의 경로, 파일명을 하나로 합칠 때 사용
        // Application.streamingAssetsPath : 현재 유니티 프로젝트 - Assets - MapData 폴더 경로
        fileName = Path.Combine(Application.persistentDataPath, fileName);
        // "fileName" 파일에 있는 내용을 "dataAsJson" 변수에 문자열 형태로 저장
        string dataAsJson = File.ReadAllText(fileName);
        ////////////////////////////////////////////////////////////////////
        aWSDATA.data = dataAsJson;

        context.SaveAsync(aWSDATA, (result) =>
        {
            if (result.Exception == null)
            {
                countList++;
            }
            else
            {
                countList++;
            }
        });
    }

    IEnumerator WaitCompleteUpload()
    {
        float runTime = 0f;
        while (true)
        {
            runTime += Time.deltaTime;
            if(runTime > limitTime)
            {
                failImage.SetActive(true);
                uploadingImage.SetActive(false);
                canNotTouch.SetActive(false);
                yield break;
            }
                
            yield return null;
            if (countList == maxList)
            {
                succesImage.SetActive(true);
                uploadingImage.SetActive(false);
                canNotTouch.SetActive(false);
                yield break;
            }
        }
    }
    int countDownload = 0;
    public void DownloadMapData() //DB에서 캐릭터 정보 받기
    {
        // 데이터 베이스에서 모든 키 (id)를 불러온다.
        // 해당 id를 통해 모든 파일을 덮어쓰기 or 생성한다.
        string fileName_ = "fileName";
        AWSDATA temp = null;


        countDownload = 0;
        downloadingImage.SetActive(true);
        canNotTouch.SetActive(true);


        for (int i = 0; i < maxMapFileNum; i++)
        {
            context.LoadAsync<AWSDATA>(fileName_ + i, (AmazonDynamoDBResult<AWSDATA> result) =>
            {
                // id가 abcd인 캐릭터 정보를 DB에서 받아옴
                if (result.Exception != null)
                {
                    Debug.LogException(result.Exception);
                    return;
                }
                temp = result.Result;
                if (temp == null)
                {
                    countDownload++;
                }
                else
                {
                    // 동기화
                    string fileName = JsonConvert.DeserializeObject<MapData>(temp.data).fileName;
                    if (fileName.Contains(".json") == false)
                    {
                        fileName += ".json";
                    }
                    // 파일의 경로, 파일명을 하나로 합칠 때 사용
                    // 현재 프로젝트 위치 기준으로 "MapData" 폴더
                    fileName = Path.Combine(Application.persistentDataPath, fileName);

                    // "fileName" 파일에 "toJson" 내용을 저장
                    //File.Delete(fileName);
                    File.WriteAllText(fileName, temp.data);
                    FindObjectOfType<DirectoryController>().UpdateDirectoryAuto();
                    countDownload++;
                }
            }, null);
        }
        StartCoroutine(WaitCompleteDownload());
    }
    IEnumerator WaitCompleteDownload()
    {
        float runTime = 0f;
        while (true)
        {
            runTime += Time.deltaTime;
            if (runTime > limitTime)
            {
                failImage.SetActive(true);
                downloadingImage.SetActive(false);
                canNotTouch.SetActive(false);
                yield break;
            }
            yield return null;
            if (countDownload == maxMapFileNum)
            {
                succesImage.SetActive(true);
                downloadingImage.SetActive(false);
                canNotTouch.SetActive(false);
                yield break;
            }
        }
    }
}