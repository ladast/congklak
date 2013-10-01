using UnityEngine;
using System.Collections;

public class hole_script : MonoBehaviour {
	private Ray myray;
	private RaycastHit myhit;
	private Transform hole;
	private int jumlahbiji;
	public GameObject myText;
	public GameObject[] myBeans;
	public GameObject[] objBeans;
	private Vector3 createPos;
	// Use this for initialization
	void Start () {
		objBeans=new GameObject[25];
		hole=this.transform;
		TextMesh t=(TextMesh)myText.gameObject.GetComponent(typeof(TextMesh));
		t.fontSize=60;
		foreach(GameObject objBean in myBeans){
			objBean.renderer.enabled=false;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		TextMesh t=(TextMesh)myText.gameObject.GetComponent(typeof(TextMesh));
		t.text=jumlahbiji.ToString();
		foreach(Touch touch in Input.touches){
			if(Input.touchCount>0){
				myray=Camera.main.ScreenPointToRay(touch.position);
				if(Physics.Raycast(myray,out myhit)){
					if(myhit.transform==hole){
						
					}
				}
			}
		}
	}
	public void setJumlahBiji(int i){
		this.jumlahbiji=i;
		int tmp_jb=0;
		if(this.jumlahbiji<=20){
			tmp_jb=this.jumlahbiji;
		}
		else if(this.jumlahbiji>20 && this.jumlahbiji<25){
			tmp_jb=21;
		}
		else if(this.jumlahbiji>24 && this.jumlahbiji<30){
			tmp_jb=22;
		}
		else if(this.jumlahbiji>29 && this.jumlahbiji<35){
			tmp_jb=23;
		}
		else if(this.jumlahbiji>34){
			tmp_jb=25;
		}
		
		foreach(GameObject objBean in myBeans){
			if(tmp_jb.ToString()==objBean.name){
				objBean.renderer.enabled=true;
			}
			else{
				objBean.renderer.enabled=false;
			}
		}
	}
}
