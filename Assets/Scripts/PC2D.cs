using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PC2D : MonoBehaviour {
	
	public int speed;
	public float shotSpeed;
	public Transform posLeft;
	public Transform posRight;
	public Transform spawnPoint;
	public GameObject PlayerDeath;
	public GameObject bullet;
	public SpriteRenderer sr;
	public Animator anim;
	public Rigidbody2D rb2d;
	public float jumpforce;
	bool isGrounded;
	bool shotTimer;
	float shootTimer = .2f;
	public Text countText;
	private int counter;
	// Use this for initialization
	void Start () {
		isGrounded = true;
		shotTimer = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(shotTimer)
			shootTimer -= Time.deltaTime;
		if (shootTimer <= 0) {
			shotTimer = false;
			anim.SetBool ("isShoting", false);
		}
		Movement ();
		//toggles animatioms when touching the ground
		if (isGrounded) { 
			anim.SetBool ("isGrounded", true);
		} else {
			anim.SetBool ("isGrounded", false);
		}
		//this helps us get back to the ground faster
		if (rb2d.velocity.y > 0) {
			rb2d.velocity += Vector2.up * Physics2D.gravity.y * 2 * Time.deltaTime;
		}
	}

	void Movement(){

		//shooting
		if (Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space)) {
			if (!shotTimer) {
				GameObject newBullet;
				if (!sr.flipX) {
					newBullet = Instantiate (bullet, posLeft.position, transform.rotation);
					newBullet.GetComponent<Rigidbody2D> ().velocity = Vector2.right * -shotSpeed;
				} else {
					newBullet = Instantiate (bullet, posRight.position, transform.rotation);					
					newBullet.GetComponent<Rigidbody2D> ().velocity = Vector2.right * shotSpeed;
				}
				anim.SetBool ("isShoting", true);
				shotTimer = true;
			}
		}
		//take left right and jump movement, make sure idle/move/fall anim is toggled
		if((Input.GetKeyDown(KeyCode.W)) && isGrounded){
			rb2d.AddForce (Vector2.up * jumpforce);
			anim.SetBool ("isShoting", false);
			isGrounded = false;
		}
		if(Input.GetKey(KeyCode.D)){
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isShoting", false);
			transform.Translate(new Vector3(speed*Time.deltaTime, 0, 0));
			sr.flipX = true;
		}
		else if(Input.GetKey(KeyCode.A)){
			anim.SetBool ("isWalking", true);
			anim.SetBool ("isShoting", false);
			transform.Translate(new Vector3(-speed*Time.deltaTime, 0, 0));				
				sr.flipX = false;
		}
		else {
			anim.SetBool("isWalking", false);
		}
	}
	//ensure jumping only from ground
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Ground")
			isGrounded = true;
		if (col.gameObject.tag == "Spikes") {
			Instantiate (PlayerDeath, transform.position, transform.rotation);
			transform.position = spawnPoint.position;
		}

	}

	void SetCountText () {
		countText.text = "Coins: " + counter.ToString ();
	}
}

