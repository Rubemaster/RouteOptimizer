using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawOnPanel : MonoBehaviour
{
  private Vector2 getPointOnLine(Vector2 start, Vector2 end, float t){
    float oppose=1-t;
    Vector2 at=new Vector2(start.x*oppose+end.x*t,start.y*oppose+end.y*t);
    return at;
  }
  private Texture2D activeTexture;
  private void circle(Vector2 pos,float radius){
    for(float x=-radius;x<radius;x++){
      float height=Mathf.Sqrt(Mathf.Pow(radius,2)-Mathf.Pow(x,2));
      for(float y=-height+0.5f;y<height;y++){
          activeTexture.SetPixel((int)(pos.x+x),(int)(pos.y+y),Color.black);
      }
    }
  }
  public GameObject targetPanel;
  private Sprite sprite;
  private Image image;
  private Vector2 lastPosition;
  private bool mouseWasUp;
  private bool mouseIsDown;
  private RectTransform panelTransform;
  void Start()
  {
    panelTransform=targetPanel.GetComponent(typeof(RectTransform)) as RectTransform;
    mouseWasUp=true;
    mouseIsDown=false;
    image=targetPanel.GetComponent(typeof(Image)) as Image;
    activeTexture=new Texture2D((int)panelTransform.rect.width, (int)panelTransform.rect.height);
    clear();
    sprite=Sprite.Create(activeTexture, new Rect(0.0f, 0.0f, activeTexture.width, activeTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    image.sprite=sprite;
  }
  public void clear(){
    for(int x=0;x<activeTexture.width;x++)for(int y=0;y<activeTexture.height;y++)activeTexture.SetPixel(x,y,Color.white);
    activeTexture.Apply();
  }
  private bool isInside;
  public void enterArea(){
    isInside=true;
  }
  public void exitArea(){
    isInside=false;
  }
  // Update is called once per frame
  void Update()
  {
    if(Input.GetMouseButtonDown(0)){
      mouseIsDown=true;
    }
    if(Input.GetMouseButtonUp(0)){
      mouseIsDown=false;
      mouseWasUp=true;
    }
    if(mouseIsDown&&isInside){
      if(mouseWasUp){
        lastPosition=new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        mouseWasUp=false;
      }

      Vector2 newPosition=new Vector2(Input.mousePosition.x-panelTransform.offsetMin.x,Input.mousePosition.y-panelTransform.offsetMin.y);
      for(float i=0;i<1;i+=1/Vector2.Distance(newPosition,lastPosition)){
        Vector2 pos=getPointOnLine(lastPosition,newPosition,i);
        circle(pos, 2);
      }
      lastPosition=newPosition;
      activeTexture.Apply();
    }
  }
}
