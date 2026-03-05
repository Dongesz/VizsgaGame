using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProfileDataManager : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text emailText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private RawImage profileImage;

   

    public void LoadProfile()
    {
        Debug.Log("LoadProfile() hívva");
        StartCoroutine(LoadProfileCoroutine());
    }

    private IEnumerator LoadProfileCoroutine()
    {
        yield return ApiClient.GetMyUserResult(
            onSuccess: me =>
            {
                Debug.Log("GetMyUserResult onSuccess, url: " + me.profilePictureUrl);
                nameText.text = me.name;
                emailText.text = me.email;
                scoreText.text = me.totalScore.ToString();

                StartCoroutine(LoadImage(me.profilePictureUrl));
            },
            onError: err =>
            {
                Debug.LogError("Profil hiba: " + err);
            }
        );
    }

    private IEnumerator LoadImage(string imageUrl)
    {
        Debug.Log("LoadImage url: " + imageUrl);

        using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return req.SendWebRequest();

            Debug.Log("LoadImage result: " + req.result + " error: " + req.error);

            if (req.result == UnityWebRequest.Result.ConnectionError ||
                req.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Kép hiba: " + req.error);
                yield break;
            }

            Texture2D tex = DownloadHandlerTexture.GetContent(req);
            Debug.Log("Kép betöltve, méret: " + tex.width + "x" + tex.height);

            profileImage.texture = tex;
            profileImage.enabled = true;
        }
    }
}