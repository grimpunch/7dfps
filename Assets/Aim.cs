using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {
	
	public AnimationClip ADS;
	public AnimationClip returnToNormal;
	// Use this for initialization
	public bool aiming;
	public bool GunOutbool;
	
	public void GunOut(){GunOutbool = true;}
	public void GunNotOut(){GunOutbool = false;}
	
	void Start () {
	aiming = false;	
		animation.AddClip(ADS,"ADS");
		animation.AddClip(returnToNormal,"RTN");
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(GunOutbool == true)
		{
			if(Input.GetKeyDown(KeyCode.Mouse1))
			{
				if(aiming==false)
				{
					animation.Play("ADS");
					aiming = true;
				}
				else if(aiming==true)
				{
					animation.Play("RTN");
					aiming = false;
				}
				
			}
		}
		else
		{
			if(aiming ==true)
			{
				animation.Play("RTN");
			aiming = false;
			}
		}
	}
}
