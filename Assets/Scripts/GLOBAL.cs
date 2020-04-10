using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL : MonoBehaviour
{
  public class Address{
    public bool isVisible;
    public Vector2 coordinate;
    public string name;
    public Address(Vector2 coordinateIn, string nameIn){
      coordinate=coordinateIn;
      name=nameIn;
      isVisible=true;
    }
  }
  public static List<Address> addresses;
  public static int selectedAddress;
  void Start(){
    addresses=new List<Address>();
  }
}
