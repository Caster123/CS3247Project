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
        rigidbody.SetDensity((float)Convert.ToInt32(this.tag));
	}

    public AudioClip attack = Resources.Load("lazer") as AudioClip;

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
		realPlayer = getRealPlayer ();
		if (!isPause() && isReady() && !removed)
		{
			reset ();
	        audio.pitch = (float)1.5;
	        audio.PlayOneShot(attack);
			removed = true;
	        
	        //Application.LoadLevel("basic");
	        //Application.LoadLevel("1st");
	        StartCoroutine(Wait(0.0F));
	        //Application.LoadLevel("basic");
		}
    }

    IEnumerator Wait(float waitTime)
    {
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
		//** Temporary usage!!!! Remeber to change it to Shared
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
		if (isSingle ())
		{
			//print("S");
			GameObject target = GameObject.Find("Counter");
			Counter c = target.GetComponent<Counter>();
			c.addRemove();
		}
		else
		{
			//print ("MU");
			GameObject target = GameObject.Find ("TimerMulti");
			TimerMulti tm = target.GetComponent<TimerMulti>();
			tm.addRemove(realPlayer);
		}
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
			tm.changePlayer();
		}
	}
}
