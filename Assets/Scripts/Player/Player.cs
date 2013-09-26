using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public ChainJam.PLAYER playerID;					// Useful to remember which player it is :)
	
	public float speed;									// Horizontal movement speed
	public float jumpStrength;							// Jump power
	public float jumpStrengthMultiplier;				// Should be 1, but it can be changed by other functions
	public float gravity;								// Rigidbodies can have their own gravity, but this makes it all a bit more tweakable
	
	public Transform bottomLeftPoint;					
	public Transform bottomMiddlePoint;
	public Transform bottomRightPoint;					// This are points inside the player object, from which we cast a "ray" to find out if it's touching something
	
	private float lockLeft;								// Don't allow left movement
	private float lockRight;							// ... or right movement
	//private Vector3  startScale;						// Remember the start scale, for the squishing animation
	private bool squished;								// Are we alive or what?
	
	private KeyCode keyLeft;
	private KeyCode keyRight;	
	private KeyCode keyUp;
	
	private Vector3 lastPosition;

	void Start () {
		//startScale = transform.localScale;
		lastPosition = transform.position;
	}
	
	void Update() {
		
		
		Vector3 currentPosition = transform.position;
		
		// Moving up
		/*
		if (playerID == ChainJam.PLAYER.PLAYER1)
			Debug.Log("last: " + lastPosition.y + " , now: " + currentPosition.y);
		*/
		if (lastPosition.y + 0.05f < currentPosition.y){
			Debug.Log("yay");
			this.gameObject.layer = 9;
		} else {
			//Debug.Log("no");
			this.gameObject.layer = 0;
		}
		
		if(!squished)
		{
			// Jump and Squish check

			RaycastHit bottomLeft, bottomMiddle, bottomRight, left, right;
			Physics.Raycast (bottomLeftPoint.position, -bottomLeftPoint.up, out bottomLeft, 0.6f * transform.localScale.y);
			Physics.Raycast (bottomMiddlePoint.position, -bottomMiddlePoint.up , out bottomMiddle, 0.6f  * transform.localScale.y);
			Physics.Raycast (bottomRightPoint.position, -bottomRightPoint.up, out bottomRight, 0.6f  * transform.localScale.y);		
			Physics.Raycast (transform.position, transform.right, out right, 0.6f  * transform.localScale.x);		
			Physics.Raycast (transform.position, -transform.right, out left, 0.6f  * transform.localScale.x);		

			if(bottomLeft.collider && bottomLeft.collider.tag == "Player")
			{
				eject();
				SoundManager.i.Play(SoundManager.i.Jump);
				//bottomLeft.collider.transform.GetComponent<Player>().Squish(this);
			}
			if(bottomMiddle.collider && bottomMiddle.collider.tag == "Player")
			{
				eject();
				SoundManager.i.Play(SoundManager.i.Jump);
				//bottomMiddle.collider.transform.GetComponent<Player>().Squish(this);
			}
			if(bottomRight.collider && bottomRight.collider.tag == "Player")
			{
				eject();
				SoundManager.i.Play(SoundManager.i.Jump);
				//bottomRight.collider.transform.GetComponent<Player>().Squish(this);
			}
			
			if(ChainJam.GetButtonJustPressed(playerID,ChainJam.BUTTON.A) || ChainJam.GetButtonJustPressed(playerID,ChainJam.BUTTON.B))
			{
				if (bottomLeft.collider || bottomMiddle.collider || bottomRight.collider) {	
					rigidbody.velocity = (new Vector3(0, jumpStrength * jumpStrengthMultiplier,0));
					SoundManager.i.Play(SoundManager.i.Jump);
					Debug.Log("normal jump");
				}
				else if(left.collider && (!left.collider.CompareTag("Wall")))
				{
					rigidbody.velocity = (new Vector3(jumpStrength* jumpStrengthMultiplier*0.3f, jumpStrength * jumpStrengthMultiplier*0.7f,0));
					SoundManager.i.Play(SoundManager.i.Jump);
					lockLeft = 0.1f;
					Debug.Log("side jump left" + jumpStrength + " " + jumpStrengthMultiplier );
				}
				else if(right.collider && (!right.collider.CompareTag("Wall")))
				{
					rigidbody.velocity = (new Vector3(-jumpStrength* jumpStrengthMultiplier*0.3f, jumpStrength* jumpStrengthMultiplier *0.7f,0));
					SoundManager.i.Play(SoundManager.i.Jump);
					lockRight = 0.1f;
					Debug.Log("side jump right" + jumpStrength + " " + jumpStrengthMultiplier );
				}
			}
			if((ChainJam.GetButtonJustReleased(playerID,ChainJam.BUTTON.A) || ChainJam.GetButtonJustReleased(playerID,ChainJam.BUTTON.B)) && rigidbody.velocity.y > 0 )
			{
				rigidbody.velocity = (new Vector3(rigidbody.velocity.x, rigidbody.velocity.y / 2,0));
			}
		}
		
		lastPosition = currentPosition;
	}
		
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!squished)
		{
		
			if(ChainJam.GetButtonPressed(playerID,ChainJam.BUTTON.LEFT) && lockLeft <= 0)
			{
				rigidbody.velocity = (new Vector3(-1 * speed * Time.deltaTime,rigidbody.velocity.y,0));
			}
			if(ChainJam.GetButtonPressed(playerID,ChainJam.BUTTON.RIGHT)  && lockRight <= 0)
			{
				rigidbody.velocity=(new Vector3(speed * Time.deltaTime,rigidbody.velocity.y,0));
			}
		}

		
		rigidbody.AddForce(new Vector3(0,gravity,0));
		
		if(lockLeft > 0) lockLeft -= Time.deltaTime;
		if(lockRight > 0) lockRight -= Time.deltaTime;
	}
	
	void OnGUI() {
		
		Debug.DrawRay (bottomLeftPoint.position, 	-bottomLeftPoint.up*1f, Color.green,0.1f);
		Debug.DrawRay (bottomMiddlePoint.position, 	-bottomMiddlePoint.up*1f, Color.red,0.1f);
		Debug.DrawRay (bottomRightPoint.position, 	-bottomRightPoint.up*1f, Color.blue,0.1f);
		
	}
	
	public void Squish(Player squishedBy=null) {
		if(!squished)
		{
			if(squishedBy)
			{
				//ChainJam.AddPoints(squishedBy.playerID,1	);
				if(ChainJam.GetTotalPoints() >= 10) ChainJam.GameEnd();
			}
			
			if (Camera.current != null){
				ScreenShake s = Camera.current.GetComponent<ScreenShake>();
				s.Shake();
			}
			
			ChainJam.ResetPoints(playerID);

			SoundManager.i.Play(SoundManager.i.Boom);
			squished = true;
			
			/*iTween.ScaleTo(gameObject,iTween.Hash(
				"y",0.1f, 
				"x",1.3f,
				"time",0.2f,
				"easeType", "linear"));
			iTween.MoveTo(gameObject,iTween.Hash(
				"y",transform.position.y - 0.4f,
				"time",0.2f,
				"easeType", "linear"));*/
			
			GameObject.Find("HUD").SendMessage("addDeath",this);
			
			this.renderer.enabled = false;
			gravity = 0;
			rigidbody.isKinematic = true;
			GetComponentInChildren<ParticleSystem>().Play();
			
			StartCoroutine(Respawn());
		}
	}
	
	void eject(){
	
		rigidbody.velocity = (new Vector3(0, jumpStrength * 1.3f * jumpStrengthMultiplier,0));
		
	}
	
	IEnumerator Respawn() {
		yield return new WaitForSeconds(1);
		
		this.renderer.enabled = true;
		gravity = -80;
		rigidbody.isKinematic = false;
		
		squished = false;
		SoundManager.i.Play(SoundManager.i.Respawn);
		
		transform.position = SpawnPoint.GetRandomSpawnpoint().position+Vector3.up*1.2f;
	}
	

}
