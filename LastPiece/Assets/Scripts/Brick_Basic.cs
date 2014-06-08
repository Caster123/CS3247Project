// Used for basic blocks
// To set the density and destroy when clicked

using UnityEngine;
using System.Collections;
using System;

public class Brick_Basic : MonoBehaviour {

	// Use this for initialization
	bool removed;
	private int realPlayer;
    void Start()
    {
		removed = false;
		realPlayer = 0;
		// Set the density to be the tag of the cube
        rigidbody.SetDensity((float)Convert.ToInt32(this.tag));
	}

    public AudioClip attack;
	
    void OnMouseDown()
    {
		realPlayer = getRealPlayer ();
		// Check whether it is some special case that we can not destroy the cube
		if (!isPause() && isReady() && !removed && !isPadTouched())
		{
			reset ();
	        audio.pitch = (float)1.5;
	        audio.PlayOneShot(attack);
			removed = true;
	        StartCoroutine(Wait(0.0F));
		}
    }

    IEnumerator Wait(float waitTime)
    {
		// flicker
        for (int i=0; i< 5; i++)
        {
            renderer.enabled = false;
            yield return new WaitForSeconds((float)0.05);
            renderer.enabled = true;
            yield return new WaitForSeconds((float)0.05);
        }
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
		updateDisplay ();
    }

	bool isSingle()
	{
		return (GameObject.Find ("TimerMulti") == null);
	}

	int getRealPlayer()
	{
		if (!isSingle ()){
			GameObject target = GameObject.Find ("TimerMulti");
			TimerMulti tm = target.GetComponent<TimerMulti>();
			return tm.getPlayer();
		}
		return 0;
	}

	void updateDisplay()
	{
		// If single player, then update counter
		if (isSingle ())
		{
			//print("S");
			GameObject target = GameObject.Find("Counter");
			Counter c = target.GetComponent<Counter>();
			c.addRemove();
		}
	}

	bool isPadTouched()
	{
		GameObject target = GameObject.Find("PAD");
		Pad p = target.GetComponent<Pad>();
		return p.isPadTouched();
	}
	
	bool isPause()
	{
		GameObject target = GameObject.Find("Pause");
		Pause p = target.GetComponent<Pause>();
		return p.check();
	}

	bool isReady()
	{
		if (isSingle())
		{
			GameObject target = GameObject.Find("EnergyTimer");
			EnergyTimer et = target.GetComponent<EnergyTimer>();
			return et.isReady();
		}
		else
		{
			GameObject target = GameObject.Find ("TimerMulti");
			TimerMulti tm = target.GetComponent<TimerMulti>();
			if (tm.getPlayer() == 0)
			{
				GameObject target0 = GameObject.Find("EnergyTimer0");
				EnergyTimer et0 = target0.GetComponent<EnergyTimer>();
				return et0.isReady();
			}
			else
			{
				GameObject target0 = GameObject.Find("EnergyTimer1");
				EnergyTimer et0 = target0.GetComponent<EnergyTimer>();
				return et0.isReady();
			}
		}
	}

	// Reset energy bar
	void reset()
	{
		if (isSingle())
		{
			GameObject target = GameObject.Find("EnergyTimer");
			EnergyTimer et = target.GetComponent<EnergyTimer>();
			et.Reset();
		}
		else
		{
			GameObject target = GameObject.Find ("TimerMulti");
			TimerMulti tm = target.GetComponent<TimerMulti>();
			if (tm.getPlayer() == 0)
			{
				GameObject target0 = GameObject.Find("EnergyTimer0");
				EnergyTimer et0 = target0.GetComponent<EnergyTimer>();
				et0.Reset();
			}
			else
			{
				GameObject target0 = GameObject.Find("EnergyTimer1");
				EnergyTimer et0 = target0.GetComponent<EnergyTimer>();
				et0.Reset();
			}
			// Change to the other player
			tm.changePlayer();
		}
	}
}
