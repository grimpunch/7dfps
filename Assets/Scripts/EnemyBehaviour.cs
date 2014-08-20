using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {
	
	public Transform[] waypoints;
	public float waypointRadius = 1.5f;
	public float damping = 0.1f;
    public bool loop = false;
	private bool faceHeading = false;
    private Vector3 currentHeading,targetHeading;
    public int targetwaypoint;
 	public GameObject playerpos;
	private int Health;
	public int EnemyHealth;
	public float AttackRadius;
	public float speed;
	public bool hit = false;
	public float recoveryTime;
	public float recoveryTimeSet;
	public GameObject HitSound;
	public Color EnemyColorNormal;
	public Color EnemyColorHit;
	
	//Behaviour FSM
	public enum eState {eStateNull,eStatePatrol,eStateAttack,};
	public eState eCurrentState;
	
	
	// Use this for initialization
	void Start () {
		eCurrentState = eState.eStatePatrol;
		playerpos = GameObject.FindGameObjectWithTag("Player");
		Health = EnemyHealth;
		this.renderer.material.color = EnemyColorNormal;
		
		 if(waypoints.Length<=0)
        {
            Debug.Log("No waypoints on "+name);
            enabled = false;
        }
		for(int i = 0 ; i<waypoints.Length;i++)
		{
			if(i==0){waypoints[i] = GameObject.FindGameObjectWithTag("way1").transform;}
			if(i==1){waypoints[i] = GameObject.FindGameObjectWithTag("way2").transform;}
			if(i==2){waypoints[i] = GameObject.FindGameObjectWithTag("way3").transform;}
			if(i==3){waypoints[i] = GameObject.FindGameObjectWithTag("way4").transform;}
			if(i==4){waypoints[i] = GameObject.FindGameObjectWithTag("way5").transform;}
			if(i==5){waypoints[i] = GameObject.FindGameObjectWithTag("way6").transform;}
			if(i==6){waypoints[i] = GameObject.FindGameObjectWithTag("way7").transform;}
			if(i==7){waypoints[i] = GameObject.FindGameObjectWithTag("way8").transform;}
			if(i==8){waypoints[i] = GameObject.FindGameObjectWithTag("way9").transform;}
			if(i==9){waypoints[i] = GameObject.FindGameObjectWithTag("way10").transform;}
			
		}
		targetwaypoint = 0;
		FaceHeading(waypoints[targetwaypoint].position);
	}
	
	//State Setters
	void SetPatrol(){Debug.Log("NPC returning to Patrol");eCurrentState = eState.eStatePatrol;}
	void SetAttacking(){Debug.Log("NPC Attacking Player");eCurrentState = eState.eStateAttack;}
	void FaceHeading(Vector3 target){transform.LookAt(target);}
	
	 protected void FixedUpdate ()
    {
		switch(eCurrentState)
		{
			case eState.eStatePatrol:
				{
		        targetHeading = waypoints[targetwaypoint].position - transform.position;
				break;
			//Debug.Log ("GUARD MOVING");
				}
			case eState.eStateAttack:
				{
				targetHeading = playerpos.transform.position - transform.position;
				break;
				}
			case eState.eStateNull:
			{
				//dead
				break;
			}
			default:
				break;
		}
		if(eCurrentState != eState.eStateNull)
		{
			//currentHeading = Vector3.Lerp(currentHeading,targetHeading,damping*Time.deltaTime);
			currentHeading = Vector3.Normalize(currentHeading+targetHeading);
		}
    }
	
	void OnCollisionStay(Collision collide)
	{
		if(collide.gameObject.tag == "bullet")
		{
			Destroy(collide.gameObject);
			hit = true;
			Health = Health - 1;
			faceHeading = false;
			rigidbody.AddRelativeForce(Vector3.Normalize(playerpos.transform.position),ForceMode.Impulse);
			recoveryTime = recoveryTimeSet;
			this.renderer.material.color = EnemyColorHit;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
			switch(eCurrentState)
			{
				case eState.eStatePatrol:
				{
					//rigidbody.AddForce(currentHeading*speed);
					
						//rigidbody.AddRelativeTorque(Vector3.forward.z,ForceMode.Force);
						if(hit==false)
						{
						transform.position +=currentHeading * Time.deltaTime * speed;				
						
				        if(faceHeading)
						{
				            transform.LookAt(transform.position+currentHeading);
						}
				        if(Vector3.Distance(transform.position,waypoints[targetwaypoint].position)<=waypointRadius)
				        {
				            targetwaypoint++;
							
							
				            if(targetwaypoint>=waypoints.Length)
				            {
				                targetwaypoint = 0;
								if(!loop)
				                    enabled = false;
				            }
							FaceHeading(waypoints[targetwaypoint].position);
				        }
						if(Vector3.Distance(transform.position,playerpos.transform.position)<=AttackRadius)
							{
								eCurrentState = eState.eStateAttack;
							}
						}
					break;
				}
				case eState.eStateAttack:
				{
					if(hit==false)
						{
								transform.position +=currentHeading * Time.deltaTime * speed;				
								
						        if(faceHeading)
								{
						            transform.LookAt(transform.position+currentHeading);
								}
							if(Vector3.Distance(transform.position,playerpos.transform.position)<=transform.localScale.z)
					        {
							//Explosive force
							playerpos.transform.Translate(0,5,-30,Space.Self);
							//HurtPlayer
							Debug.Log("Player Hit");
							
							}
							else if(Vector3.Distance(transform.position,playerpos.transform.position)>=AttackRadius)
							{
							
									//Insert animation code and sound for this change
							//rigidbody.AddForce(currentHeading,ForceMode.Force);
									
							}
						}
					break;
				}
				case eState.eStateNull:
				{
					
					break;
				}
				default:
					break;
			}
		if(hit == true){recoveryTime = (recoveryTime - (Time.fixedDeltaTime * 1));}
		if(recoveryTime <= 0)
		{
			recoveryTime = recoveryTimeSet;
			hit = false;
			faceHeading = true;
			if(Health <= 3){eCurrentState = eState.eStateAttack;}
			this.renderer.material.color = EnemyColorNormal;
		}
		if(Health <= 0)
		{
			Kill();
		}

	}//End update
	
	
	
	public void Kill()
	{
		//ANIMS FOR DYING
		//Set to die
		eCurrentState = eState.eStateNull;
		Debug.Log ("CUBE IS DEAD");
		//DEBUG
		Destroy(gameObject);
	}
	public void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,AttackRadius);
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(waypoints[targetwaypoint].position,waypointRadius);
		if(waypoints==null)
            return;
        for(int i=0;i< waypoints.Length;i++)
        {
            Vector3 pos = waypoints[i].position;
            if(i>0)
            {
                Vector3 prev = waypoints[i-1].position;
                Gizmos.DrawLine(prev,pos);
            }
        }
    }
	
	
	
	
}


