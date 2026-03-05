using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiClient
{
    private const string BaseUrl = "https://dongesz.com/api";

    [Serializable]
    public class MeResultDto
    {
        public string name;
        public string email;
        public string bio;
        public string userType;
        public string profilePictureUrl;
        public int totalScore;
        public int totalXp;
        public string createdAt;
        public string updatedAt;
    }

    [Serializable]
    private class MeResponseWrapper
    {
        public string message;
        public bool success;
        public MeResultDto result;
    }

    public static IEnumerator GetMyUserResult(
        Action<MeResultDto> onSuccess,
        Action<string> onError = null)
    {
        if (AuthManager.Instance == null || !AuthManager.Instance.IsLoggedIn)
        {
            onError?.Invoke("Nincs AuthManager vagy nincs token (nem vagy belépve).");
            yield break;
        }

        string url = $"{BaseUrl}/main/Users/me/result";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Authorization", "Bearer " + AuthManager.Instance.Token);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(request.error);
            }
            else
            {
                string json = request.downloadHandler.text;
                Debug.Log("GetMyUserResult válasz: " + json);

                MeResponseWrapper data = JsonUtility.FromJson<MeResponseWrapper>(json);

                if (data != null && data.success && data.result != null)
                {
                    onSuccess?.Invoke(data.result);
                }
                else
                {
                    onError?.Invoke("Váratlan válasz vagy success = false.");
                }
            }
        }
    }
}