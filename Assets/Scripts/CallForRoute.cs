using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class CallForRoute : MonoBehaviour
{
  public GameObject eventSystem;
  public string[] addressTargets;
  public Vector2[] geoTargets;
  public List<Vector2> roadTargets;
  public string preServiceJsonCall;
  public string postServiceJsonCall;
  public void routeCall(){

    string deliveries="\"services\": [";
    for(int i=0; i<GLOBAL.addresses.Count;i++){
      deliveries+="{\"id\": \"Stop #"+i.ToString()+"\",\"name\": \"N/A\",\"address\": {\"location_id\": \""+i.ToString()+"\",\"lon\":"+GLOBAL.addresses[i].coordinate.y.ToString()+",\"lat\": "+GLOBAL.addresses[i].coordinate.x.ToString()+"},\"size\": [1],\"time_windows\": [{\"earliest\": 0,\"latest\": 922337203685477}]}";
      if(i<GLOBAL.addresses.Count-1){deliveries+=",";}
    }
    deliveries+="],";
    StartCoroutine(PostRouteWorkRequest(preServiceJsonCall,deliveries,postServiceJsonCall,"32c66e96-5fad-45ba-a50d-b82ad1db4abb"));
    //StartCoroutine(GetNearestRoad(geoTargets,"AIzaSyDCynUbT6aUPBQX2TnnR0zyv4v2bmfuthc"));
  }
  IEnumerator GetNearestRoad(Vector2[] coordinates,string apiKey){
    string pointsString="";
    for(int i=0;i<GLOBAL.addresses.Count;i++){
      pointsString+=GLOBAL.addresses[i].coordinate.x.ToString()+","+GLOBAL.addresses[i].coordinate.y.ToString();
      if(i<GLOBAL.addresses.Count-1){
        pointsString+="|";
      }
    }
    string thisUrl="https://roads.googleapis.com/v1/nearestRoads?points="+pointsString+"&key="+apiKey;
    string retJson;
    using (UnityWebRequest webRequest =UnityWebRequest.Get(thisUrl))
    {
      yield return webRequest.SendWebRequest();
      retJson=webRequest.downloadHandler.text;
    }
    JSONNode data = JSON.Parse(retJson);
    Debug.Log(data);
    roadTargets=new List<Vector2>();
    for(int i=0;i<GLOBAL.addresses.Count;i++){
      roadTargets.Add(new Vector2(float.Parse(data["snappedPoints"][i*2]["location"]["latitude"]),float.Parse(data["snappedPoints"][i*2]["location"]["longitude"])));
    }

  }
  private string job_id;
  IEnumerator PostRouteWorkRequest(string preService,string jsonServices,string postService,string apiKey)
  {
    string retJson;
    byte[] data = System.Text.Encoding.UTF8.GetBytes(preService+jsonServices+postService);
    UploadHandlerRaw upHandler = new UploadHandlerRaw(data);
    upHandler.contentType = "application/json";
    using (UnityWebRequest webRequest =UnityWebRequest.Post("https://graphhopper.com/api/1/vrp/optimize?key="+apiKey,UnityWebRequest.kHttpVerbPOST))
    {
      webRequest.uploadHandler=upHandler;
      yield return webRequest.SendWebRequest();
      retJson=webRequest.downloadHandler.text;
    }
    JSONNode retData = JSON.Parse(retJson);
    job_id=retData["job_id"];
    StartCoroutine(GetWorkRet("32c66e96-5fad-45ba-a50d-b82ad1db4abb"));
  }
  IEnumerator GetWorkRet(string apiKey){
    bool finished=false;
    string retJson="";
    JSONNode retData=JSON.Parse(retJson);
    while(!finished){
      using (UnityWebRequest webRequest =UnityWebRequest.Get("https://graphhopper.com/api/1/vrp/solution/"+job_id+"?key="+apiKey))
      {
        yield return webRequest.SendWebRequest();
        retJson=webRequest.downloadHandler.text;
      }
      retData= JSON.Parse(retJson);
      Debug.Log(retData["status"]);
      if(retData["status"]=="finished"){
        finished=true;
      }
      yield return new WaitForSeconds(0.5f);
    }

    Debug.Log(retJson);
    List<GLOBAL.Address> newAddresses=new List<GLOBAL.Address>();
    for(int i=1;i<GLOBAL.addresses.Count+1;i++){
      newAddresses.Add(GLOBAL.addresses[int.Parse(retData["solution"]["routes"][0]["activities"][i]["location_id"])]);

    }
    GLOBAL.addresses=newAddresses;
    InstantiateAddresses instantiateAddresses=eventSystem.GetComponent(typeof(InstantiateAddresses)) as InstantiateAddresses;
    instantiateAddresses.updateAddresses();
  }
}
