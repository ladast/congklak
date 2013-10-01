using UnityEngine;
using System.Collections;

public class controlMain : MonoBehaviour {
	public Transform mainButton;
	public Transform keluarButton;
	private Ray myray;
	private RaycastHit myhit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Touch touch in Input.touches){
			if(Input.touchCount>0){
				myray=Camera.main.ScreenPointToRay(touch.position);
				if(Physics.Raycast(myray,out myhit)){
					if(myhit.transform==mainButton){
						Application.LoadLevel(1);
					}else if(myhit.transform==keluarButton){
						Application.Quit();						
					}
				}
			}
		}
	}
}
