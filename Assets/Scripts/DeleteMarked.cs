using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMarked : MonoBehaviour
{
  public GameObject addressPrefab;
  public List<int> deleted;
  public GameObject eventSystem;
  public void onDelete(){
    deleted= new List<int>();
    List<GLOBAL.Address> newAddresses=new List<GLOBAL.Address>();
      GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag (addressPrefab.tag);
      for (int i = 0 ; i < gameObjects.Length ; i ++){
        ManageAddress manageAddress=gameObjects[i].GetComponent(typeof(ManageAddress)) as ManageAddress;
        if(!manageAddress.getCheck()){
          newAddresses.Add(GLOBAL.addresses[i]);
        }
      }
      GLOBAL.addresses=newAddresses;
      InstantiateAddresses instantiateAddresses=eventSystem.GetComponent(typeof(InstantiateAddresses)) as InstantiateAddresses;
      instantiateAddresses.updateAddresses();
  }
}
