using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class AddressSearch : MonoBehaviour
{
  public GameObject addressField;
  public GameObject eventSystem;
  public string apiKey;
  public void searchFor(){
    InputField inputField=addressField.GetComponent(typeof(InputField)) as InputField;
    if(inputField.text!=""){
      StartCoroutine(checkTerm(inputField.text));
      inputField.text="";
    }
  }
  IEnumerator checkTerm(string addressSearch)
  {
    //string thisUrl="https://maps.googleapis.com/maps/api/geocode/json?address="+UnityWebRequest.EscapeURL(addressSearch)+"&key="+apiKey;
    Debug.Log(UnityWebRequest.EscapeURL(addressSearch));
    string thisUrl="https://www.geocode.farm/v3/json/forward/?addr="+addressSearch+"&country=sv&lang=en&count=1";
    string retJson;
    using (UnityWebRequest webRequest =UnityWebRequest.Post(thisUrl,UnityWebRequest.kHttpVerbGET))
    {
      yield return webRequest.SendWebRequest();
      retJson=webRequest.downloadHandler.text;
    }
    JSONNode data = JSON.Parse(retJson);
    Debug.Log(data);
    string status=data["geocoding_results"]["STATUS"]["status"];
    Debug.Log(status);
    if(status=="SUCCESS"){
      string div=" | ";
      string format=data["geocoding_results"]["RESULTS"][0]["ADDRESS"]["street_name"]+" "+data["geocoding_results"]["RESULTS"][0]["ADDRESS"]["street_number"]+div+data["geocoding_results"]["RESULTS"][0]["ADDRESS"]["postal_code"]+div+data["geocoding_results"]["RESULTS"][0]["ADDRESS"]["locality"]+div+data["geocoding_results"]["RESULTS"][0]["ADDRESS"]["admin_1"]+div+data["geocoding_results"]["RESULTS"][0]["ADDRESS"]["country"];
      float latitude=float.Parse(data["geocoding_results"]["RESULTS"][0]["COORDINATES"]["latitude"]);
      float longitude=float.Parse(data["geocoding_results"]["RESULTS"][0]["COORDINATES"]["longitude"]);
      string name=format;
      Vector2 coordinate=new Vector2(latitude,longitude);
      GLOBAL.addresses.Add(new GLOBAL.Address(coordinate,name));
      InstantiateAddresses instantiateAddresses=eventSystem.GetComponent(typeof(InstantiateAddresses)) as InstantiateAddresses;
      instantiateAddresses.updateAddresses();
    }
  }
}
