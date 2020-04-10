using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSceneById : MonoBehaviour
{
  void setSceneById(int id){
    SceneManager.LoadScene (id);
  }
}
