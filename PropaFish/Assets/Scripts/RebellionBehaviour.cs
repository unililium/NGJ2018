using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebellionBehaviour : MonoBehaviour {

    [Header("Movement speed"), Range(0f, 20f)]
    public float speed;
    [Header("Time required to turn"), Range(0f, 1f)]
	public float timeToTurn;
	[Header("Cooldown"), Range(5f, 40f)]
	public float cooldown;
    public float climbingHeight;

	private Vector3 savedPos;
	private Quaternion savedRot;
    private Quaternion previousRot;

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
        phaseDuration[State.READY_TO_REBEL] = float.PositiveInfinity;
        phaseDuration[State.TURNING_UPWARDS] = timeToTurn;
        phaseDuration[State.CLIMBING] = float.PositiveInfinity;
        phaseDuration[State.TURNING_DOWNWARDS] = timeToTurn;
        phaseDuration[State.DIVING] = float.PositiveInfinity;
        phaseDuration[State.RESTORING_NORMALITY] = timeToTurn;
        phaseDuration[State.CANNOT_REBEL] = cooldown;

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
                if (Input.GetButtonDown("Fire1"))
                {
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
                transform.rotation = Quaternion.Slerp(savedRot, Quaternion.Euler(0, 0, 270), phaseProgress);

                if (phaseProgress == 1f)
                {
                    To(State.DIVING);
                }
                break;

            case State.DIVING:
                transform.position = Vector3.MoveTowards(transform.position, savedPos, speed * Time.fixedDeltaTime);
                if (Mathf.Approximately(transform.position.y, savedPos.y))
                {
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

    public void DiveDown()
    {
        float lowerLevel = Mathf.Clamp(savedPos.y, Aquarium.GetGroundY(), savedPos.y - climbingHeight);
        switch (state)
        {
            case State.READY_TO_REBEL:
            case State.CANNOT_REBEL:
                gameObject.GetComponent<FishBehaviour>().enabled = false;
                savedRot = transform.rotation;
                savedPos = transform.position;
                savedPos.y = lowerLevel;
                To(State.TURNING_DOWNWARDS);
                break;

            case State.TURNING_UPWARDS:
            case State.CLIMBING:
                To(State.TURNING_DOWNWARDS);
                break;

            case State.TURNING_DOWNWARDS:
                break;

            case State.DIVING:
                savedPos.y = lowerLevel;
                break;

            case State.RESTORING_NORMALITY:
                savedPos.y = lowerLevel;
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
}
