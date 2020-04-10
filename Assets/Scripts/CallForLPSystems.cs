using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class CallForLPSystems : MonoBehaviour
{
  public GameObject inputURL;
  public GameObject inputForm1;
  // Start is called before the first frame update
  private class LocalFormField{
    private string privateFieldName;
    public string name{
      get{return privateFieldName;}
      set{privateFieldName=value;}
    }
    private string privateFieldValue;
    public string value{
      get{return privateFieldValue;}
      set{privateFieldValue=value;}
    }
    public LocalFormField(string inputName,string inputValue){
      privateFieldName=inputName;
      privateFieldValue=inputValue;
    }
  }
    public void onAPIRecall()
    {
      InputField inputFieldURL=inputURL.GetComponent(typeof(InputField)) as InputField;
      FormField formContainer=inputForm1.GetComponent(typeof(FormField)) as FormField;
      InputField inputFieldName=formContainer.nameField.GetComponent(typeof(InputField)) as InputField;
      InputField inputFieldValue=formContainer.valueField.GetComponent(typeof(InputField)) as InputField;
      List<LocalFormField> localFormField=new List<LocalFormField>();
      localFormField.Add(new LocalFormField(inputFieldName.text,inputFieldValue.text));
      Debug.Log(inputFieldURL.text);
      StartCoroutine(checkAPI(inputFieldURL.text,localFormField.ToArray()));
    }

    IEnumerator checkAPI(string url,LocalFormField[] fields){
      string retJson="";
      WWWForm form = new WWWForm();
      for(int i=0;i<fields.Length;i++){
        form.AddField(fields[i].name,fields[i].value);
      }
      JSONNode retData=JSON.Parse(retJson);
      byte[] data = System.Text.Encoding.UTF8.GetBytes("{JSONInput:\"what what\"}");
      UploadHandlerRaw upHandler = new UploadHandlerRaw(data);
      using (UnityWebRequest webRequest =UnityWebRequest.Post(url,form))
      {
        yield return webRequest.SendWebRequest();
        retJson=webRequest.downloadHandler.text;
      }
      retData= JSON.Parse(retJson);
      Debug.Log(retData);
    }
}
