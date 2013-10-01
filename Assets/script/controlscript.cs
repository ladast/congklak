using UnityEngine;
using System.Collections;

public class controlscript : MonoBehaviour {
	public GameObject objLevel;
	public Transform exitButton;
	public Transform restartButton;
	private Ray myray;
	private RaycastHit myhit;
	private Transform hole;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Touch touch in Input.touches){
			if(Input.touchCount>0){
				myray=Camera.main.ScreenPointToRay(touch.position);
				if(Physics.Raycast(myray,out myhit)){
					if(myhit.transform==exitButton){
						Application.LoadLevel(0);
						Destroy(objLevel);
					}else if(myhit.transform==restartButton){
						Application.LoadLevel(1);						
						Destroy(objLevel);
					}
				}
			}
		}
	}
}
