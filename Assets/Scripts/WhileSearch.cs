using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class WhileSearch : MonoBehaviour
{
  public GameObject eventSystem;
  public GameObject textField;
  public void onFieldChange(){
    InputField inputField=textField.GetComponent(typeof(InputField)) as InputField;
    string searchTerm=inputField.text;
    InstantiateAddresses instantiateAddresses=eventSystem.GetComponent(typeof(InstantiateAddresses)) as InstantiateAddresses;
    CultureInfo culture=new CultureInfo("sv-SE",false);
    for(int i=0;i<GLOBAL.addresses.Count;i++){
      if(!(culture.CompareInfo.IndexOf(GLOBAL.addresses[i].name, searchTerm, CompareOptions.IgnoreCase) >= 0)){
        GLOBAL.addresses[i].isVisible=false;
      }
    }
    instantiateAddresses.updateAddresses();
  }
}
