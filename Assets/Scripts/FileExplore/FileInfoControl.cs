using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileInfoControl : MonoBehaviour
{
  public GameObject title;
  public GameObject button;
  public void updateTitle(string newTitle){
    Text titleText=title.GetComponent(typeof(Text)) as Text;
    titleText.text=newTitle;
  }
  public void setThisDirectory(){
    GameObject eventSystem=GameObject.FindGameObjectsWithTag("EventSystem")[0];
    Directory directory=eventSystem.GetComponent(typeof(Directory)) as Directory;
    Text titleText=title.GetComponent(typeof(Text)) as Text;
    directory.goDeep(titleText.text);
  }
  public void noOpen(){
    Button thisButton=button.GetComponent(typeof(Button)) as Button;
    thisButton.interactable=false;
  }
}
