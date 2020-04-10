using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class Carrier2 : MonoBehaviour
{
  public GameObject eventSystem;
  public IEnumerator SortIt(){
    string completeSet="{\\\"points\\\":[";
    for(int i=0;i<GLOBAL.addresses.Count;i++){
      completeSet+="{\\\"id\\\":"+i.ToString()+"\\,";
      completeSet+="\\\"coordinates\\\":{\\\"longitude\\\":"+GLOBAL.addresses[i].coordinate.x.ToString()+"\\,\\\"latitude\\\":"+GLOBAL.addresses[i].coordinate.y.ToString()+"}}";
      if(i<GLOBAL.addresses.Count-1)completeSet+="\\,";
    }
    completeSet+="]}";
    Debug.Log(completeSet);
    WWWForm form = new WWWForm();
    form.AddField("inputJSON",completeSet);
    JSONNode retData="";
    string retJson="";
    using (UnityWebRequest webRequest =UnityWebRequest.Post("localhost:3000/form",form))
    {
      yield return webRequest.SendWebRequest();
      retJson=webRequest.downloadHandler.text;
    }
    retData= JSON.Parse(retJson);
    Debug.Log(retData["resultKey"]);
    retJson="";
    form = new WWWForm();
    form.AddField("id",int.Parse(retData["resultKey"].ToString()));
    while(retJson.Length<=10){
      using (UnityWebRequest webRequest =UnityWebRequest.Post("localhost:3000/result",form))
      {
        yield return webRequest.SendWebRequest();
        retJson=webRequest.downloadHandler.text;
      }
    }
    Debug.Log(retJson);
  }
}
