using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class ApiClient : MonoBehaviour
{
    [SerializeField] private string apiUrl = "http://10.101.204.161:1880";

    IEnumerator SendRequest(string endpoint, string method, string jsonBody = "")
    {
        UnityWebRequest request;
        string url = apiUrl + endpoint;

        if (method == "GET")
        {
            request = UnityWebRequest.Get(url);
        }
        else
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding(true).GetBytes(jsonBody);

            request = UnityWebRequest.Post(url, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            byte[] responseData = request.downloadHandler.data;
            string responseText = Encoding.UTF8.GetString(responseData); 
            Debug.Log("API response: " + responseText);

            // Process the response data as needed
        }
        else
        {
            Debug.Log("API request failed with error: " + request.error);
        }
    }

    IEnumerator GetAllStatus()
    {
        yield return StartCoroutine(SendRequest("/status_all", "GET"));
    }

    IEnumerator GetStatus(int id)
    {
        string endpoint = "/status?id=" + id.ToString();
        yield return StartCoroutine(SendRequest(endpoint, "GET"));
    }

    IEnumerator GetSensorData(int id, string key)
    {
        string endpoint = "/sensor?id=" + id.ToString() + "&key=" + key;
        yield return StartCoroutine(SendRequest(endpoint, "GET"));
    }

    public void TriggerGetAllStatus()
    {
        StartCoroutine(GetAllStatus());
    }

    public void TriggerGetStatus(int id)
    {
        StartCoroutine(GetStatus(id));
    }

    public void TriggerGetSensorData(int id, string key)
    {
        StartCoroutine(GetSensorData(id, key));
    }

    public void TriggerPostMessage(int id, string payload)
    {
        string endpoint = "/test";
        string jsonBody = "{\"msg\":{\"id\":" + id.ToString() + ",\"payload\":\"" + payload + "\"}}";

        StartCoroutine(SendRequest(endpoint, "POST", jsonBody));
    }
}
