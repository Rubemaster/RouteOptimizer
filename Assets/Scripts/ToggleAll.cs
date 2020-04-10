using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAll : MonoBehaviour
{
  public GameObject addressPrefab;
  public GameObject thisToggle;
  public void onChange(){
    Toggle toggle=thisToggle.GetComponent(typeof(Toggle)) as Toggle;
    bool newValue=toggle.isOn;
    GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag (addressPrefab.tag);
    for (int i = 0 ; i < gameObjects.Length ; i ++){
      ManageAddress manageAddress=gameObjects[i].GetComponent(typeof(ManageAddress)) as ManageAddress;
      manageAddress.setCheck(newValue);
    }
  }
}
