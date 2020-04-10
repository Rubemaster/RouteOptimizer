using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageAddress : MonoBehaviour
{
  public GameObject addressText;
  public GameObject addressToggle;
  public int thisId;

  public void setText(string newText){
    Text thisText=addressText.GetComponent(typeof(Text)) as Text;
    thisText.text=newText;
  }
  public void setCheck(bool newValue){
    Toggle toggle=addressToggle.GetComponent(typeof(Toggle)) as Toggle;
    toggle.isOn=newValue;
  }
  public bool getCheck(){
    Toggle toggle=addressToggle.GetComponent(typeof(Toggle)) as Toggle;
    return toggle.isOn;
  }
  public void onClick(){
    GLOBAL.selectedAddress=thisId;
  }
}
