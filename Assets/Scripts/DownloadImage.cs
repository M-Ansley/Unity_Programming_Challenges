using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class DownloadImage : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private string _imageURL;

    [SerializeField] private TextMeshProUGUI _promptText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetTexture());
        }
    }

    private IEnumerator GetTexture()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(_imageURL);

        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError(request.error);
            _promptText.color = Color.red;
            _promptText.text = "An error occurred";
        }
        else
        {
            Debug.Log("Successfully downloaded image");
            var texture = DownloadHandlerTexture.GetContent(request);
            _rawImage.texture = texture;
            _promptText.color = Color.green;
            _promptText.text = "Success!";
        }
    }
}
