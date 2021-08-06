using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class DownloadText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _textURL;

    [System.Serializable] // lets us map JSON directly to it
    public class Fact
    {
        public string fact;
        public int length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetText());
        }
    }

    private IEnumerator GetText()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_textURL))
        {
            yield return request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Successfully downloaded text");
                
                var text = request.downloadHandler.text;

                Fact catFact = JsonUtility.FromJson<Fact>(text);
                _text.text = catFact.fact;
            } 
        }
    }



    // CF JSON to C# Class Conversion tool: https://json2csharp.com/
    // https://catfact.ninja/

}
