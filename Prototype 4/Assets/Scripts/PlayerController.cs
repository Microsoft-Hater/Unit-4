using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody playerRb;
	public float speed;
	private GameObject focalPoint;
	public bool hasPowerup;
	private float powerupStrength = 15.0f;
	public GameObject powerUpIndicator;

    // Start is called before the first frame update
    void Start()
    {
		playerRb = GetComponent<Rigidbody>();
		focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
		float forwardInput = Input.GetAxis("Vertical");
		playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
		powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other){
		if (other.CompareTag("Powerup")){
			hasPowerup = true;
			Destroy(other.gameObject);
			StartCoroutine(PowerupCountdownRoutine());
			powerUpIndicator.gameObject.SetActive(true);
		}
	}

	private void OnCollisionEnter(Collision collision){
		if (collision.gameObject.CompareTag("Enemy") && hasPowerup){
			Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
			Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

			Debug.Log("Player collided with " + collision.gameObject + " with powerup set to " + hasPowerup);
			enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
		}
	}

	IEnumerator PowerupCountdownRoutine(){
		yield return new WaitForSeconds(7);
		hasPowerup = false;
		powerUpIndicator.gameObject.SetActive(false);
	}
}
