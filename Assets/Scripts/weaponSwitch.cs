using UnityEngine;
using System.Collections;

public class weaponSwitch : MonoBehaviour {
	
	public enum WeaponOut{pistol,dubstep};
	public WeaponOut weaponSelected;
	public GameObject Pistol;
	public GameObject DubstepGun;
	public float dubstepModeTimer;
	public AudioClip dubstep;

	// Use this for initialization
	void Start () {Screen.showCursor = false;
	
		weaponSelected = WeaponOut.pistol;
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Alpha1))
		{
			weaponSelected = WeaponOut.pistol;
		}
		if(Input.GetKey(KeyCode.Alpha2))
		{
			weaponSelected = WeaponOut.dubstep;
			
				dubstepModeTimer = dubstep.length;
			
			
		}
		
		switch(weaponSelected)
		{
		case WeaponOut.pistol:
			Camera.main.GetComponent("VortexEffect").SendMessage("Fix");
			Camera.main.GetComponent("MotionBlur").SendMessage("Fix");
			DubstepGun.active = false;
			SendMessage("GunOut");
			Pistol.active = true;
			break;
		case WeaponOut.dubstep:
			Camera.main.GetComponent("VortexEffect").SendMessage("Iterate");
			Camera.main.GetComponent("MotionBlur").SendMessage("Iterate");
			DubstepGun.active = true;
			SendMessage("GunNotOut");
			Pistol.active = false;
			break;
			
		}
		if(weaponSelected == WeaponOut.dubstep)
		{
			dubstepModeTimer= dubstepModeTimer- Time.fixedDeltaTime;
			if(dubstepModeTimer<=0)
			{
				weaponSelected = WeaponOut.pistol;
			}
		}
		
		
	
	}
}
