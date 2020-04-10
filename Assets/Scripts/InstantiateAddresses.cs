using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAddresses : MonoBehaviour
{
  public GameObject AddressPrefab;
  public GameObject ScrollContent;
  private string[] allAddresses;
  private Vector2[] coordinates;

  void Start(){
    allAddresses=new string[0];
  }
  private void makeAddressBox(float y, string text,int addressId){
    GameObject newAddress=Instantiate(AddressPrefab,ScrollContent.transform);
    ManageAddress manageAddress=newAddress.GetComponent(typeof(ManageAddress)) as ManageAddress;
    manageAddress.setText(text);
    manageAddress.thisId=addressId;
    RectTransform rectTransform=newAddress.GetComponent(typeof(RectTransform)) as RectTransform;
    rectTransform.anchoredPosition=new Vector2(rectTransform.anchoredPosition.x,y);

  }
  public void updateAddresses(){
    float offset=40;
    GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag (AddressPrefab.tag);
    for (int i = 0 ; i < gameObjects.Length ; i ++)Destroy(gameObjects[i]);
    for (int i=0;i<GLOBAL.addresses.Count;i++){
      if(GLOBAL.addresses[i].isVisible){
        offset-=40;
        makeAddressBox(offset,GLOBAL.addresses[i].name,i);
        Debug.Log(GLOBAL.addresses[i].coordinate);
      }
    }
  }
  public void redrawAddresses(string[] addresses,bool temp=false){
    if(!temp)allAddresses=addresses;
    GameObject[] gameObjects =  GameObject.FindGameObjectsWithTag (AddressPrefab.tag);
    for (int i = 0 ; i < gameObjects.Length ; i ++)Destroy(gameObjects[i]);
    for (int i=0;i<addresses.Length;i++){
      for(int i2=0;i2<allAddresses.Length;i2++){
        if(allAddresses[i2]==addresses[i]){
          makeAddressBox(-i*40,addresses[i],i2);
        }
      }
    }
  }
  public void addAddress(string newAddress){
    List<string> newAddressList=new List<string>();
    for(int i=0;i<allAddresses.Length;i++){
      newAddressList.Add(allAddresses[i]);
    }
    newAddressList.Add(newAddress);
    allAddresses=newAddressList.ToArray();
    makeAddressBox(40-newAddressList.Count*40,newAddress,newAddressList.Count-1);
  }
  public string[] getAddresses(){
    return allAddresses;
  }

}
