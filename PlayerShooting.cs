using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable"); //Can only shoot Game Objects on this Shootable layer.
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

		//If we fire and evnough time has progressed, we turn off the flash, sound, etc of the gunBarrelEnd.
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        gunParticles.Stop ();//Ensure that the particles are stopped before trying to play.
        gunParticles.Play ();

		//Turn on line renderer
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position); //Set Start of gunLine to the end of gun barrel.

        shootRay.origin = transform.position;  //Start ray at tip of gun
		shootRay.direction = transform.forward;  //Direction that ray should shoot. (Z - AXIS)

		//out shootHit gives info about what was hit by shootRay. shootableMask specifies the mask that the ray is able to hit. (Shootable Layer above)
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> (); //Give me the enemy health script Component.
            if(enemyHealth != null) //If no health script returned, don't go through this loop.
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point); //shootHit.point is the location where the stuffing particles get rendered.
            }
            gunLine.SetPosition (1, shootHit.point); //Start at the end of the gun, end at the shootable collider that was hit.
        }
        else
        {
			//Not shooting something in the Shootable Layer
			//Start of Ray + Direction(Where mouse is pointed) * range of shot (100f)
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
