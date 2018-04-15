using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebellionBehaviour : MonoBehaviour {

    [Header("Movement speed"), Range(0f, 20f)]
    public float speed;
    [Header("Time required to turn"), Range(0f, 1f)]
	public float timeToTurn;
	[Header("Movement speed"), Range(0f, 20f)]
	public float speed;
	[Header("Cooldown"), Range(5f, 40f)]
	public float cooldown;
    public float climbingHeight;
    public float patience;

	private Vector3 savedPos;
	private Quaternion savedRot;
    private Quaternion previousRot;
    private EatFoodScript stomach;
    private float lastHappyTime;
    private float previousEnergy;

    private static float mood = 1;

    public enum State
    {
        READY_TO_REBEL,
        TURNING_UPWARDS,
        CLIMBING,
        TURNING_DOWNWARDS,
        DIVING,
        RESTORING_NORMALITY,
        CANNOT_REBEL      
    }
    public State state;
    private float timeAtStateChange;
    private Dictionary<State, float> phaseDuration = new Dictionary<State, float>();

    private void Start()
    {
        lastHappyTime = Time.time;
        stomach = GetComponent<EatFoodScript>();
        phaseDuration[State.READY_TO_REBEL] = float.PositiveInfinity;
        phaseDuration[State.TURNING_UPWARDS] = timeToTurn;
        phaseDuration[State.CLIMBING] = float.PositiveInfinity;
        phaseDuration[State.TURNING_DOWNWARDS] = timeToTurn;
        phaseDuration[State.DIVING] = float.PositiveInfinity;
        phaseDuration[State.RESTORING_NORMALITY] = timeToTurn;
        phaseDuration[State.CANNOT_REBEL] = cooldown;
        patience += Random.Range(-2f, 2f);
        patience *= 2;
    }

    private void To(State newState)
    {
        timeAtStateChange = Time.time;
        this.state = newState;
        this.previousRot = transform.rotation;
    }

    void FixedUpdate()
    {
        float phaseProgress = Mathf.Clamp((Time.fixedTime - timeAtStateChange) / phaseDuration[state], 0f, 1f);
        switch (state)
        {
            case State.READY_TO_REBEL:
                if (Time.fixedTime - lastHappyTime > patience / RebellionBehaviour.mood || Input.GetButtonDown("Fire1"))
                {
                    RebellionBehaviour.mood *= 1.1f;
                    lastHappyTime = Time.fixedTime;
                    savedPos = transform.position;
                    savedRot = transform.rotation;
                    gameObject.GetComponent<FishBehaviour>().enabled = false;
                    To(State.TURNING_UPWARDS);
                }
                break;

            case State.TURNING_UPWARDS:
                transform.rotation = Quaternion.Slerp(savedRot, Quaternion.Euler(0, 0, 90), phaseProgress);
                if (phaseProgress == 1f)
                {
                    To(State.CLIMBING);
                }
                break;

            case State.CLIMBING:
                transform.position += speed * transform.right * Time.fixedDeltaTime;
                float random = Random.Range(-5f, 5f);
                if (random > -1f && random < 1f)
                {
                    transform.position += speed * random * transform.up * Time.fixedDeltaTime;
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                if (transform.position.y > savedPos.y + climbingHeight || transform.position.y >= Aquarium.GetWaterY() - 0.5f)
                {
                    To(State.RESTORING_NORMALITY);
                }                
                break;

            case State.TURNING_DOWNWARDS:
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(previousRot.z, 270, phaseProgress));
                    
                    // Quaternion.Slerp(savedRot, Quaternion.Euler(0, 0, 270), phaseProgress);

                if (phaseProgress == 1f)
                {
                    To(State.DIVING);
                }
                break;

            case State.DIVING:
                GetComponent<Collider>().enabled = false;
                transform.position = Vector3.MoveTowards(transform.position, savedPos, speed * Time.fixedDeltaTime);
                if (Mathf.Approximately(transform.position.y, savedPos.y) || transform.position.y <= Aquarium.GetGroundY() + 0.5f)
                {
                    GetComponent<Collider>().enabled = true;
                    To(State.RESTORING_NORMALITY);
                }
                break;

            case State.RESTORING_NORMALITY:
                transform.rotation = Quaternion.Slerp(previousRot, savedRot, phaseProgress);
                if (phaseProgress == 1f)
                {
                    gameObject.GetComponent<FishBehaviour>().enabled = true;
                    To(State.CANNOT_REBEL);
                }
                break;

            case State.CANNOT_REBEL:
                if (phaseProgress == 1f)
                {
                    To(State.READY_TO_REBEL);
                }
                break;
        }
    }

    private void Update()
    {
        if (stomach.isActiveAndEnabled)
        {
            float currentEnergy = stomach.Energy;
            if (currentEnergy > previousEnergy)
            {
                lastHappyTime = Time.time;
            }
            previousEnergy = currentEnergy;
        }
    }

    public void LowerSavedPos()
    {
        savedPos.y = Mathf.Clamp(savedPos.y - climbingHeight, Aquarium.GetGroundY() + 0.5f, Aquarium.GetWaterY());
    }

    public void DiveDown()
    {
        Debug.Log("DiveDown from " + state);
        switch (state)
        {
            case State.READY_TO_REBEL:
            case State.CANNOT_REBEL:
                gameObject.GetComponent<FishBehaviour>().enabled = false;
                savedRot = transform.rotation;
                savedPos = transform.position;
                LowerSavedPos();
                To(State.TURNING_DOWNWARDS);
                break;

            case State.TURNING_UPWARDS:
            case State.CLIMBING:
                To(State.TURNING_DOWNWARDS);
                break;

            case State.TURNING_DOWNWARDS:
                break;

            case State.DIVING:
                LowerSavedPos();
                break;

            case State.RESTORING_NORMALITY:
                LowerSavedPos();
                To(State.TURNING_DOWNWARDS);
                break;

        }
    }

    void OnTriggerEnter(Collider collider)
    {
        CollideWithWall(collider.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollideWithWall(collision.collider.gameObject);
    }

    void CollideWithWall(GameObject wall)
    {
        if (state == State.CLIMBING || state == State.DIVING)
        {
            if (wall.tag == "WallR" || wall.tag == "WallL")
            {
                savedRot = Quaternion.Euler(0, wall.tag == "WallR" ? -180 : 0, 0);
                To(State.RESTORING_NORMALITY);
            }
        }
    }

	IEnumerator RebellionEnd() {
		yield return new WaitForSeconds(rebellionDuration);
		rebellionStop = false;
		gameObject.GetComponent<FishBehaviour>().enabled = true;
	}
}
