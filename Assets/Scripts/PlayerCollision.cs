//============================
//Amy Becerra
//Task Description (9/27): Create a collision function for the player that deals damage to the player when they collide with an asteroid and the asteroid has a particular velocity
//Task Description (10/2): Modify the code so that the player tests the velocity value component in the direction of the player, and takes damage only if that particular velocity is over a certain value (this means that a player colliding with an asteroid that is moving away from them will not damage them)
//Last edited : 10/4/16
//============================

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour 
{

	//Declaration of Variables
	private GameObject _asteroidInput;
	private bool _playerDamaged = false;
	private Rigidbody2D _asteroidRigidbody;
	private float _asteroidVelocity = 0f;
	private float _asteroidMinimum = 1f; //Minimum velocity of asteroid to deal damage to player, can be changed later
	private float _tempHealth = 100f;
	private float _asteroidDamageForce = 0f;
	const float _ASTEROIDFORCECONSTANT = 2f; //Can change later
	public Vector2 asteroidDirection;

	void Start () 
	{
		
	}

	void Update () 
	{
			
	}



	//Function that grabs velocity from the asteroid object and stores it in a var, applies damage to player if velocity is high enough, calculates a force, and subtracts that force from player health
	//Function also calculates if asteroid and player are moving away from each other or are facing 
	void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.tag == "SceneLoader")
        {
            SceneManager.LoadScene("TitleMenu");
        }
        if (col.gameObject.name == "AsteroidPlaceholder")
		{
			float _directionStatus;
			_asteroidInput = col.gameObject;
			_asteroidRigidbody = _asteroidInput.GetComponent<Rigidbody2D>( );
			_asteroidVelocity = _asteroidRigidbody.velocity.magnitude;

			//Get direction vector from asteroid to player
			asteroidDirection = (col.transform.position - transform.position).normalized;

			//Find the Dot to see if they are facing the same way, facing opposite ways, etc..
			_directionStatus = Vector2.Dot(asteroidDirection, _asteroidRigidbody.velocity.normalized);

			if (_asteroidVelocity > _asteroidMinimum && _directionStatus >= 0f) //temporarily set to if the asteroid is moving, it deals damage automatically
			{
				_playerDamaged = true;
				_asteroidDamageForce = _asteroidVelocity * _ASTEROIDFORCECONSTANT * _directionStatus; //Damage to scale; Need requirements for damage 
				_tempHealth = _tempHealth - _asteroidDamageForce;
				print (_playerDamaged);
				print ("Velocity= " + _asteroidVelocity);
				print ("Player health= " + _tempHealth);
			}
		}
	}
}