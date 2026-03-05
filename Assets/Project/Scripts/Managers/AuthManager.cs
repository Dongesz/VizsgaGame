using CastL.Managers;
using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }

    [Header("UI - Login")]
    [SerializeField] private TMP_InputField UsernameTxt;
    [SerializeField] private TMP_InputField PasswordTxt;
    [SerializeField] private GameObject loginPanel;

    [Header("UI - Profile")]
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private ProfileDataManager profileDataManager;

    [Header("SFX")]
    [SerializeField] private AudioClip loginSuccessSfx;
    [SerializeField] private AudioClip loginFailedSfx;

    public string Token { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public bool IsLoggedIn => !string.IsNullOrEmpty(Token);

    [Serializable]
    private class LoginResultDto
    {
        public LoginUserDto result;
        public string token;
        public bool success;
    }

    [Serializable]
    private class LoginUserDto
    {
        public string userName;
        public string email;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnLoginButtonClicked()
    {
        StartCoroutine(LoginRequest());
    }

    private IEnumerator LoginRequest()
    {
        string url = "https://dongesz.com/api/auth/Auth/login";

        string userName = UsernameTxt.text;
        string password = PasswordTxt.text;

        string jsonBody = $"{{\"userName\":\"{userName}\",\"password\":\"{password}\"}}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // HIBÁS HTTP / HÁLÓZAT
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Login hiba: " + request.error);

            if (AudioManager.Instance != null && loginFailedSfx != null)
            {
                AudioManager.Instance.PlaySfx(loginFailedSfx);
            }

            yield break;
        }

        // SIKERES HTTP VÁLASZ
        string responseText = request.downloadHandler.text;
        Debug.Log("Login válasz: " + responseText);
        UsernameTxt.text = "";
        PasswordTxt.text = "";
        LoginResultDto data = JsonUtility.FromJson<LoginResultDto>(responseText);
        if (data != null && data.success)
        {
            Token = data.token;
            UserName = data.result?.userName;
            Email = data.result?.email;

            // siker SFX
            if (AudioManager.Instance != null && loginSuccessSfx != null)
            {
                AudioManager.Instance.PlaySfx(loginSuccessSfx);
            }

            // panelek váltása
            if (loginPanel != null) loginPanel.SetActive(false);
            if (profilePanel != null) profilePanel.SetActive(true);

            if (profileDataManager != null)
            {
                profileDataManager.LoadProfile();
            }
        }
        else
        {
            Debug.LogWarning("Login nem sikerült, success = false.");

            if (AudioManager.Instance != null && loginFailedSfx != null)
            {
                AudioManager.Instance.PlaySfx(loginFailedSfx);
            }
        }
    }

    public void Logout()
    {
        // auth adatok nullázása
        Token = null;
        UserName = null;
        Email = null;

        // panelek váltása
        if (profilePanel != null) profilePanel.SetActive(false);
        if (loginPanel != null) loginPanel.SetActive(true);

        Debug.Log("Logout megtörtént.");
    }
}