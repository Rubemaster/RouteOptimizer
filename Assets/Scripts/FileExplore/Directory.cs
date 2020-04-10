using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Directory : MonoBehaviour
{
  public GameObject pathText;
  public GameObject fileUnitPrefab;
  public GameObject directoryPanel;
  public string thisPath;

  private RectTransform thisRectTransform;
  private GameObject thisObject;
  private FileInfoControl thisFileInfoControl;
    // Start is called before the first frame update
    private void addFileInfoAtY(float yPosition,string title,bool clickable){
      thisObject=Instantiate(fileUnitPrefab,directoryPanel.transform);
      thisRectTransform=thisObject.GetComponent(typeof(RectTransform)) as RectTransform;
      thisRectTransform.anchoredPosition=new Vector2(thisRectTransform.anchoredPosition.x,yPosition);
      thisFileInfoControl=thisObject.GetComponent(typeof(FileInfoControl)) as FileInfoControl;
      thisFileInfoControl.updateTitle(title);
      if(!clickable){
        thisFileInfoControl.noOpen();
      }
    }
    public void reloadDirectory(string path)
    {
      Text title=pathText.GetComponent(typeof(Text)) as Text;
      title.text=path;
      GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag ("FileInfo");
      for(int i = 0 ; i < gameObjects.Length ; i ++)Destroy(gameObjects[i]);
      thisPath=path;
        DirectoryInfo info=new DirectoryInfo(path);
        FileInfo[] fileInfo =info.GetFiles();
        DirectoryInfo[] directoryInfo =info.GetDirectories();
        float yPosition=0;
        for(int i=0;i<directoryInfo.Length;i++){
          Debug.Log(directoryInfo[i]);
          addFileInfoAtY(yPosition,directoryInfo[i].Name,true);
          yPosition-=30;
        }
        for(int i=0;i<fileInfo.Length;i++){
          Debug.Log(fileInfo[i].Extension);
          bool isImage=false;
          if(fileInfo[i].Extension==".jpg"||fileInfo[i].Extension==".png"||fileInfo[i].Extension==".bmp")isImage=true;
          addFileInfoAtY(yPosition,fileInfo[i].Name,isImage);
          yPosition-=30;
        }
        RectTransform directoryRectTransform= directoryPanel.GetComponent(typeof(RectTransform)) as RectTransform;
        directoryRectTransform.sizeDelta=new Vector2(directoryRectTransform.sizeDelta.x,yPosition*-1);
    }
    public void goDeep(string subPath){
      reloadDirectory(thisPath+="/"+subPath);
    }
    public void goBack(){
      if(thisPath.Length>7){
      int isAt=thisPath.Length-1;
      while(thisPath[isAt]!='/')isAt--;
      string newPath="";
      for(int i=0;i<isAt;i++){
        newPath+=thisPath[i];
      }
      reloadDirectory(newPath);
    }
    }
    void Start(){
      reloadDirectory("./../..");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
