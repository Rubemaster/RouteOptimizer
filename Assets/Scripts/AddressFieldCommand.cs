using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddressFieldCommand : MonoBehaviour
{
  public GameObject addressField;
  public GameObject eventSystem;
  public void registerAddress(){
    InstantiateAddresses instantiateAddresses=eventSystem.GetComponent(typeof(InstantiateAddresses)) as InstantiateAddresses;
    InputField inputField=addressField.GetComponent(typeof(InputField)) as InputField;
    instantiateAddresses.addAddress(inputField.text);
  }
}
