
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

        // �Ʒ� 2���� �Լ��� ��ġ�� �ٲ����
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
        // ���� �̸� �޾ƿ���
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
    private void UploadMapData(string fileName, string fileName_) //ĳ���� ������ DB�� �ø���
    {
        if (fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }

        // �������� �� ���� -> Ŭ������ ��� �ѹ��� ���ε� / �ٿ�ε�
        AWSDATA aWSDATA = new AWSDATA();
        // aWSDATA.id = fileName;
        fileName_ += idx++;
        aWSDATA.id = fileName_;

        ///////////////////////////////////////////////////////////////////
        // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
        // Application.streamingAssetsPath : ���� ����Ƽ ������Ʈ - Assets - MapData ���� ���
        fileName = Path.Combine(Application.persistentDataPath, fileName);
        // "fileName" ���Ͽ� �ִ� ������ "dataAsJson" ������ ���ڿ� ���·� ����
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
    public void DownloadMapData() //DB���� ĳ���� ���� �ޱ�
    {
        // ������ ���̽����� ��� Ű (id)�� �ҷ��´�.
        // �ش� id�� ���� ��� ������ ����� or �����Ѵ�.
        string fileName_ = "fileName";
        AWSDATA temp = null;


        countDownload = 0;
        downloadingImage.SetActive(true);
        canNotTouch.SetActive(true);


        for (int i = 0; i < maxMapFileNum; i++)
        {
            context.LoadAsync<AWSDATA>(fileName_ + i, (AmazonDynamoDBResult<AWSDATA> result) =>
            {
                // id�� abcd�� ĳ���� ������ DB���� �޾ƿ�
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
                    // ����ȭ
                    string fileName = JsonConvert.DeserializeObject<MapData>(temp.data).fileName;
                    if (fileName.Contains(".json") == false)
                    {
                        fileName += ".json";
                    }
                    // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
                    // ���� ������Ʈ ��ġ �������� "MapData" ����
                    fileName = Path.Combine(Application.persistentDataPath, fileName);

                    // "fileName" ���Ͽ� "toJson" ������ ����
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