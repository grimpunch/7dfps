using UnityEngine;
using System.Collections;

/// <summary>
/// created by Markus Davey 22/11/2011
/// Basic weapon script
/// Skype: Markus.Davey
/// Unity forums: MarkusDavey
/// </summary>


public class shootweapon : MonoBehaviour 
{
    // public
    public float projMuzzleVelocity; // in metres per second
    public GameObject projPrefab;
    public float RateOfFire;
    public float Inaccuracy;
	public bool FireButtonPressed;
	public GameObject Muzzle;
	public float gunMoveAmount;
	public float gunMoveAmountSet;
	public GameObject gunposition;
	public GameObject gunkickbackposition;
	public GameObject Gunsound;
	// private
    private float fireTimer;
    
    // Use this for initialization
    void Start () 
    {
		
        fireTimer = Time.time + RateOfFire;
    }
    
    // Update is called once per frame
    void Update () 
    {
		if(this.transform.position == gunposition.transform.position)
		{
			if(Input.GetButtonDown("Fire1") == true)
			{
				Instantiate(Gunsound,transform.position,Quaternion.identity);
	        Debug.DrawLine(transform.position, (transform.position + transform.forward), Color.red);
	        if (Time.time > fireTimer)
	        {
				gunMoveAmount = gunMoveAmountSet;
				transform.position = gunkickbackposition.transform.position;
	            GameObject projectile;
	            Vector3 muzzlevelocity = transform.forward;
	            
	            if (Inaccuracy != 0)
	            {
	                Vector2 rand = Random.insideUnitCircle;
	                muzzlevelocity += new Vector3(rand.x, rand.y, 0) * Inaccuracy;
	            }
	            
	            muzzlevelocity = muzzlevelocity.normalized * projMuzzleVelocity;
	            
	            projectile = Instantiate(projPrefab, Muzzle.transform.position, transform.rotation) as GameObject;
	            projectile.GetComponent<projectile>().muzzleVelocity = muzzlevelocity;
	            fireTimer = Time.time + RateOfFire;
	        }
	        else
	            return;
			}
			else
			{
			return;		
			}
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position,gunposition.transform.position,gunMoveAmount);
			
		}
	}
}