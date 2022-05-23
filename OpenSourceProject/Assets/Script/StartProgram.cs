using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

public class StartProgram : MonoBehaviour
{
    DynamoDBContext context;
    AmazonDynamoDBClient DBclient;
    CognitoAWSCredentials credentials;
    private void Awake()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        credentials = new CognitoAWSCredentials("ap-northeast-2:ddbf3c37-0b48-4981-8a6d-f10691ebf48a", RegionEndpoint.APNortheast2);
        DBclient = new AmazonDynamoDBClient(credentials, RegionEndpoint.APNortheast2);
        context = new DynamoDBContext(DBclient);

        // 아래 2개의 함수의 위치를 바꿔야함
        // UploadAllMapData();
        DownloadMapData();
    }
    [DynamoDBTable("PlayerData")]
    public class AWSDATA
    {
        [DynamoDBHashKey] // Hash key.
        public string id { get; set; }
        [DynamoDBProperty]
         public string data { get; set; }
    }
    public void UploadAllMapData() {
        // 파일 이름 받아오기
        List<Data> list = FindObjectOfType<DirectorySpawner>().fileList;
        foreach(Data data in list)
        {
            UploadMapData(data.FileName);
        }
    }
    private void UploadMapData(string fileName) //캐릭터 정보를 DB에 올리기
    {
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // 여러개의 맵 파일 -> 클래스로 묶어서 한번에 업로드 / 다운로드
        AWSDATA aWSDATA = new AWSDATA();
        aWSDATA.id = fileName;

        ///////////////////////////////////////////////////////////////////
        // 파일의 경로, 파일명을 하나로 합칠 때 사용
        // Application.streamingAssetsPath : 현재 유니티 프로젝트 - Assets - MapData 폴더 경로
        fileName = Path.Combine("Assets/Mapdata/", fileName);
        // "fileName" 파일에 있는 내용을 "dataAsJson" 변수에 문자열 형태로 저장
        string dataAsJson = File.ReadAllText(fileName);
        ////////////////////////////////////////////////////////////////////
        aWSDATA.data = dataAsJson;

        context.SaveAsync(aWSDATA, (result) =>
        {
            if (result.Exception == null)
                Debug.Log("Success!");
            else
                Debug.Log(result.Exception);
        });
    }

    private void DownloadMapData() //DB에서 캐릭터 정보 받기
    {
        AWSDATA aWSDATA = null;
        // 데이터 베이스에서 모든 키 (id)를 불러온다.
        // 해당 id를 통해 모든 파일을 덮어쓰기 or 생성한다.
        /*
        IEnumerable<AWSDATA> itemsWithWrongPrice = context.ScanAsync<AWSDATA>(
                    new ScanCondition("data", ScanOperator.LessThan, price),
                    new ScanCondition("ProductCategory", ScanOperator.Equal, "Book")
        */
      );
        foreach (AWSDATA a in /*데이터 들*/) {
            context.LoadAsync<AWSDATA>(a.id, (AmazonDynamoDBResult<AWSDATA> result) =>
            {
            // id가 abcd인 캐릭터 정보를 DB에서 받아옴
                if (result.Exception != null)
                {
                    Debug.LogException(result.Exception);
                    return;
                }
                aWSDATA = result.Result;
            }, null);
            // 동기화
            string fileName = aWSDATA.id;
            if (fileName.Contains(".json") == false)
            {
                fileName += ".json";
            }
            // 파일의 경로, 파일명을 하나로 합칠 때 사용
            // 현재 프로젝트 위치 기준으로 "MapData" 폴더
            fileName = Path.Combine("Assets/Mapdata/", fileName);

            // "fileName" 파일에 "toJson" 내용을 저장
            //File.Delete(fileName);
            File.WriteAllText(fileName, aWSDATA.data);
        }
    }
}