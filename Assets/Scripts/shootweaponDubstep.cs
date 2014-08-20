using UnityEngine;
using System.Collections;

/// <summary>
/// created by Markus Davey 22/11/2011
/// Basic weapon script
/// Skype: Markus.Davey
/// Unity forums: MarkusDavey
/// </summary>


public class shootweaponDubstep : MonoBehaviour 
{
    // public
    public float projMuzzleVelocity; // in metres per second
    public GameObject projPrefab;
    public float RateOfFire;
    public float Inaccuracy;
	public bool FireButtonPressed;
	public GameObject Muzzle;
    
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
		/*if(Input.GetButtonDown("Fire1") == true)
		{*/
       /* Debug.DrawLine(transform.position, (transform.position + transform.forward), Color.red);*/
        if (Time.time > fireTimer)
        {
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
}