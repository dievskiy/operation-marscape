using UnityEngine;
using System.Collections;


// this class was inside alien 2nd level package
// but it has been modified
[RequireComponent (typeof (Animator))]
public class Actions : MonoBehaviour {

	private Animator animator;
	private bool isMoving;
	const int countOfDamageAnimations = 3;
	int lastDamageAnimation = -1;

	void Awake () {
		animator = GetComponent<Animator> ();
	}
	public bool IsCurrentlyMoving()
	{
		return isMoving;
	}

	public void Stay () {
		isMoving = false;
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 0f);
		}

	public void Walk () {
		isMoving = true;
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 0.5f);
	}

	public void Run () {
		isMoving = true;
		animator.SetBool("Aiming", false);
		animator.SetFloat ("Speed", 1f);
	}

	public void Attack () {
		isMoving = false;
		Aiming ();
		animator.SetTrigger ("Attack");
	}

	public void Death () {
		isMoving = false;
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
			animator.Play("Idle", 0);
		else
			animator.SetTrigger ("Death");
	}

	public void Damage () {
		isMoving = false;
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) return;
		int id = Random.Range(0, countOfDamageAnimations);
		if (countOfDamageAnimations > 1)
			while (id == lastDamageAnimation)
				id = Random.Range(0, countOfDamageAnimations);
		lastDamageAnimation = id;
		animator.SetInteger ("DamageID", id);
		animator.SetTrigger ("Damage");
	}

	public void Jump () {
		isMoving = false;
		animator.SetBool ("Squat", false);
		animator.SetFloat ("Speed", 0f);
		animator.SetBool("Aiming", false);
		animator.SetTrigger ("Jump");
	}

	public void Fire()
	{
		isMoving = false;
		animator.SetBool ("Squat", false);
		animator.SetFloat ("Speed", 0f);
		animator.SetBool("Fire", true);
		StartCoroutine(StopFire());

	}

	private IEnumerator StopFire()
	{
		yield  return new WaitForSeconds(.3f);
		animator.SetBool("Fire", false);
	}


	public void Aiming () {
		isMoving = false;
		animator.SetBool ("Squat", false);
		animator.SetFloat ("Speed", 0f);
		animator.SetBool("Aiming", true);
	}

}
