using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class _LEVEL_ : MonoBehaviour {
	public AudioClip run;
	public GUISkin mySkin;
	private Ray myray;
	private RaycastHit myhit;
	public GameObject[] myText;
	private GameObject objCamera;
	private int player_beans;
	private int oponent_beans;
	private int[] totArray;
	public GameObject[] player_holes;
	public GameObject beans;
	private Vector3 createPos;
	private Vector3 textPos;
	private int idxHoleTouch;
	private string touchType;
	private int endRun;
	public int buttonW=150;
	public int buttonH=75;
	public float halfScreenW;
	public float halfButtonW;
	private bool showRestart;
	private int idx;
	public GameObject selectedObj;
	private bool showSelectedObj;
	private bool playerTurn;
	private bool oponentTurn;
	private bool canSelect;
	private bool canRun;
	private bool swap;
	public GameObject playerTurnText;
	public GameObject selectText;
	private string winner;
	private bool gameOver;
	private bool draw;
	public GameObject result;
	private bool showButton;
	// Use this for initialization
	void Start () {
		objCamera=(GameObject)GameObject.FindWithTag("MainCamera");
		totArray=new int[16]{7,7,7,7,7,7,7,0,7,7,7,7,7,7,7,0};
		setBeans();
		showRestart=true;
		selectedObj.renderer.enabled=false;
		playerTurn=true;
		oponentTurn=false;
		canSelect=true;
		canRun=true;
		swap=false;
		gameOver=false;
		selectText.renderer.enabled=true;
		draw=false;
		showButton=false;
		
	}
	
	// Update is called once per frame
	private void Update () {
		//cekHole(totArray);
		if(totArray[0] == 0  && totArray[1]  == 0 && totArray[2]  == 0 && totArray[3]  == 0 && totArray[4]  == 0 && totArray[5]  == 0 && totArray[6] == 0){
			playerTurn=false;
			oponentTurn=true;
		}
		else if(totArray[8] == 0  && totArray[9]  == 0 && totArray[10]  == 0 && totArray[11]  == 0 && totArray[12]  == 0 && totArray[13]  == 0 && totArray[14] == 0){
			playerTurn=true;
			oponentTurn=false;
		}
		if(totArray[7]+totArray[15]==98){
			gameOver=true;
			if(totArray[7]>totArray[15]){
				winner="Player 1";
			}
			else if(totArray[7]<totArray[15]){
				winner="Player 2";
			}
			else if(totArray[7]==totArray[15]){
				winner="Seri";
				draw=true;
			}
		}
		TextMesh t=(TextMesh)playerTurnText.gameObject.GetComponent(typeof(TextMesh));
		TextMesh r=(TextMesh)result.gameObject.GetComponent(typeof(TextMesh));
		if(gameOver){
			selectText.renderer.enabled=false;
			if(!draw){
			r.text="Permainan Selesai ,Pemenangnya adalah "+winner;
			}
			else if(draw){
				r.text="Permainan Selesai ,Hasilnya adalah "+winner;
			}
			showRestart=false;
			showButton=true;
		}
		else if(!gameOver){
			if(playerTurn){
				t.text="Player 1";
			}
			else if(oponentTurn){
				t.text="Player 2";
			}
			if(totArray[idx]==0){
				showRestart=false;
			}
			else{
				showRestart=true;
			}
		}
		setCameraPosition();
		foreach(Touch touch in Input.touches){
			if(Input.touchCount>0){
				myray=Camera.main.ScreenPointToRay(touch.position);
				if(Physics.Raycast(myray,out myhit)){
					if(playerTurn && canSelect){
						for(int i=0;i<7;i++){
							if(myhit.transform==player_holes[i].transform){
							createPos=new Vector3(player_holes[i].transform.position.x,3f,player_holes[i].transform.position.z);
							selectedObj.transform.position=createPos;
							selectedObj.renderer.enabled=true;
							idx=i;
								canRun=true;
							}
						}
					}
					if(oponentTurn && canSelect){
						for(int i=7;i<15;i++){
							if(myhit.transform==player_holes[i].transform){
							createPos=new Vector3(player_holes[i].transform.position.x,3f,player_holes[i].transform.position.z);
							selectedObj.transform.position=createPos;
							selectedObj.renderer.enabled=true;
							idx=i;
								canRun=true;
							}
						}
					}
				}
			}
		}
		for(int i=0;i<16;i++){
			hole_script hs=(hole_script)player_holes[i].gameObject.GetComponent(typeof(hole_script));
			hs.setJumlahBiji(totArray[i]);
		}
		if(this.idx==7 ||this.idx==15){
			canRun=false;
			selectText.renderer.enabled=true;
			canSelect=true;
		}
	}
	private void setBeans(){
		for(int i=0;i<16;i++){
			if(i!=7 && i!=15){
				createPos=new Vector3(player_holes[i].transform.position.x,1.5f,player_holes[i].transform.position.z);
				//Instantiate(beans,createPos,Quaternion.identity);
			}
			if(i<=7){
				myText[i].transform.position=new Vector3(player_holes[i].transform.position.x,1.5f,player_holes[i].transform.position.z-0.5f);
			}
			else{
				myText[i].transform.position=new Vector3(player_holes[i].transform.position.x,1.5f,player_holes[i].transform.position.z+1.1f);
			}
			
			
		}
	}
	private void setCameraPosition(){
		objCamera.transform.position=new Vector3(transform.position.x,12f,transform.position.z);
		objCamera.transform.eulerAngles=new Vector3(90,0,0);
	}
	private void swapPlayer(){
		canSelect=true;
		if(swap){
			if(playerTurn){
				playerTurn=false;
				oponentTurn=true;
				canRun=false;
				swap=false;
				
			}
			else if(oponentTurn){
				oponentTurn=false;
				playerTurn=true;
				canRun=false;
				swap=false;
			}
			
		}
		selectText.renderer.enabled=true;
	}
	private void runBeans(int[] a,int idx){
		canSelect=false;
		int runCount,i;
		runCount=a[idx];
		i=1;
		if(a[idx]!=0){
			a[idx]=0;
			while(runCount!=0){
				if(playerTurn){
					if(idx+i<15){
						a[idx+i]+=1;
						this.idx=idx+i;
					}
					else if(idx+i>=15){
						a[idx+i-15]+=1;
						this.idx=idx+i-15;
					}
				}
				else if(oponentTurn){
					if(idx+i<7){
						a[idx+i]+=1;
						this.idx=idx+i;
					}
					else if(idx+i>7 && idx+i<=15){
						a[idx+i]+=1;
						this.idx=idx+i;
					}
					else if(idx+i>15){
						a[idx+i-16]+=1;
						this.idx=idx+i-16;
					}
					else if(idx+i==7){
						a[idx+i]=a[idx+i];
						runCount++;
					}
				}
				i++;
				runCount--;
			}
		}
		
		if(this.idx!=7 && this.idx!=15 && a[this.idx]<=1){
			
			if(a[this.idx]==1){
				if(playerTurn){
					if(this.idx==0){
						if(a[14]!=0){
							a[7]=a[7]+a[14]+a[0];
							a[14]=0;
							a[0]=0;
						}
					}
					else if(this.idx==1){
						if(a[13]!=0){
							a[7]=a[7]+a[13]+a[1];
							a[13]=0;
							a[1]=0;
						}
					}
					else if(this.idx==2){
						if(a[12]!=0){
							a[7]=a[7]+a[12]+a[2];
							a[12]=0;
							a[2]=0;
						}
					}
					else if(this.idx==3){
						if(a[11]!=0){
							a[7]=a[7]+a[11]+a[3];
							a[11]=0;
							a[3]=0;
						}
					}
					else if(this.idx==4){
						if(a[10]!=0){
							a[7]=a[7]+a[10]+a[4];
							a[10]=0;
							a[4]=0;
						}
					}
					else if(this.idx==5){
						if(a[9]!=0){
							a[7]=a[7]+a[9]+a[5];
							a[9]=0;
							a[5]=0;
						}
					}
					else if(this.idx==6){
						if(a[8]!=0){
							a[7]=a[7]+a[8]+a[6];
							a[8]=0;
							a[6]=0;
						}
					}
				}
				else if(oponentTurn){
					if(this.idx==14){
						if(a[0]!=0){
							a[15]=a[15]+a[14]+a[0];
							a[14]=0;
							a[0]=0;
						}
					}
					else if(this.idx==13){
						if(a[1]!=0){
							a[15]=a[15]+a[13]+a[1];
							a[13]=0;
							a[1]=0;
						}
					}
					else if(this.idx==12){
						if(a[2]!=0){
							a[15]=a[15]+a[12]+a[2];
							a[12]=0;
							a[2]=0;
						}
					}
					else if(this.idx==11){
						if(a[3]!=0){
							a[15]=a[15]+a[11]+a[3];
							a[11]=0;
							a[3]=0;
						}
					}
					else if(this.idx==10){
						if(a[4]!=0){
							a[15]=a[15]+a[10]+a[4];
							a[10]=0;
							a[4]=0;
						}
					}
					else if(this.idx==9){
						if(a[5]!=0){
							a[15]=a[15]+a[9]+a[5];
							a[9]=0;
							a[5]=0;
						}
					}
					else if(this.idx==8){
						if(a[6]!=0){
							a[15]=a[15]+a[8]+a[6];
							a[8]=0;
							a[6]=0;
						}
					}
				}
			}
			swap=true;
			swapPlayer();
		}
	}
	private void OnGUI(){
		GUI.skin=mySkin;
		halfScreenW=Screen.width/2;
		halfButtonW=buttonW/2;
		if(showRestart){
			if(GUI.Button(new Rect(halfScreenW-halfButtonW,480 ,buttonW,buttonH),"Jalankan")){
				if(canRun){
				runBeans(totArray,this.idx);
				selectedObj.renderer.enabled=false;
				audio.PlayOneShot(run);
				}
				selectText.renderer.enabled=false;
			}
		}
		if(showButton){
			if(GUI.Button(new Rect(halfScreenW-halfButtonW-100,480 ,buttonW,buttonH),"Ulangi")){
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(halfScreenW-halfButtonW+100,480 ,buttonW,buttonH),"Berhenti")){
				Application.LoadLevel(0);
			}
		}
	}
}
