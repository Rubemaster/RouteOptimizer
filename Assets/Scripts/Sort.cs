using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sort : MonoBehaviour
{
  public GameObject eventSystem;
  public void sort(){
    Carrier2 carrier2=eventSystem.GetComponent(typeof(Carrier2)) as Carrier2;
    StartCoroutine(carrier2.SortIt());
    InstantiateAddresses instantiateAddresses=eventSystem.GetComponent(typeof(InstantiateAddresses)) as InstantiateAddresses;
    CallForRoute callForRoute=eventSystem.GetComponent(typeof(CallForRoute)) as CallForRoute;
    //string[] allAddresses=instantiateAddresses.getAddresses();
    //callForRoute.addressTargets=allAddresses;
    //callForRoute.routeCall();
  }
}
