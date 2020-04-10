using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class OCR : MonoBehaviour
{
    // Start is called before the first frame update
    public void callForImageSearch()
    {
        StartCoroutine(searchIt());
    }
    public IEnumerator searchIt(){
      string retJson="";
      WWWForm form = new WWWForm();
      form.AddField("inputJSON","{\\\"id\\\":\\\"i1a\\\"}");
      using (UnityWebRequest webRequest =UnityWebRequest.Post("localhost:3000/ocr_post",form))
      {
        yield return webRequest.SendWebRequest();
        retJson=webRequest.downloadHandler.text;
      }
      Debug.Log(retJson);
      string retData= JSON.Parse(retJson);
    }
}
