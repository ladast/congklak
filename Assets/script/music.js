#pragma strict
@script RequireComponent(AudioSource)
var musicTrack:AudioClip;
function Start () {
	//DontDestroyOnLoad(this);
	audio.loop=false;
	while(true){
		audio.clip=musicTrack;
		audio.Play();
		if(audio.clip)
			yield WaitForSeconds(audio.clip.length);
		else
			yield;
	}
}

function Update () {

}