using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DownloadImage : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private string _imageURL;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetTexture());
        }
    }

    IEnumerator GetTexture()
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(_imageURL))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Successfully downloaded image");
                var texture = DownloadHandlerTexture.GetContent(request);
                _rawImage.texture = texture;
            }
        }
    }
}
