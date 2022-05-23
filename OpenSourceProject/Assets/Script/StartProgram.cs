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

        // �Ʒ� 2���� �Լ��� ��ġ�� �ٲ����
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
        // ���� �̸� �޾ƿ���
        List<Data> list = FindObjectOfType<DirectorySpawner>().fileList;
        foreach(Data data in list)
        {
            UploadMapData(data.FileName);
        }
    }
    private void UploadMapData(string fileName) //ĳ���� ������ DB�� �ø���
    {
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // �������� �� ���� -> Ŭ������ ��� �ѹ��� ���ε� / �ٿ�ε�
        AWSDATA aWSDATA = new AWSDATA();
        aWSDATA.id = fileName;

        ///////////////////////////////////////////////////////////////////
        // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
        // Application.streamingAssetsPath : ���� ����Ƽ ������Ʈ - Assets - MapData ���� ���
        fileName = Path.Combine("Assets/Mapdata/", fileName);
        // "fileName" ���Ͽ� �ִ� ������ "dataAsJson" ������ ���ڿ� ���·� ����
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

    private void DownloadMapData() //DB���� ĳ���� ���� �ޱ�
    {
        AWSDATA aWSDATA = null;
        // ������ ���̽����� ��� Ű (id)�� �ҷ��´�.
        // �ش� id�� ���� ��� ������ ����� or �����Ѵ�.
        /*
        IEnumerable<AWSDATA> itemsWithWrongPrice = context.ScanAsync<AWSDATA>(
                    new ScanCondition("data", ScanOperator.LessThan, price),
                    new ScanCondition("ProductCategory", ScanOperator.Equal, "Book")
        */
      );
        foreach (AWSDATA a in /*������ ��*/) {
            context.LoadAsync<AWSDATA>(a.id, (AmazonDynamoDBResult<AWSDATA> result) =>
            {
            // id�� abcd�� ĳ���� ������ DB���� �޾ƿ�
                if (result.Exception != null)
                {
                    Debug.LogException(result.Exception);
                    return;
                }
                aWSDATA = result.Result;
            }, null);
            // ����ȭ
            string fileName = aWSDATA.id;
            if (fileName.Contains(".json") == false)
            {
                fileName += ".json";
            }
            // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
            // ���� ������Ʈ ��ġ �������� "MapData" ����
            fileName = Path.Combine("Assets/Mapdata/", fileName);

            // "fileName" ���Ͽ� "toJson" ������ ����
            //File.Delete(fileName);
            File.WriteAllText(fileName, aWSDATA.data);
        }
    }
}