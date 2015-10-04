using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Character : MonoBehaviour
{
    private const string ANIM_WALK = "walk";
    private const string ANIM_DEATH = "death";
    private const float WALK = 0.000001f;

	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;

	protected Rigidbody m_Rigidbody;
	public Animator Animator;
	bool m_IsGrounded;
	float m_OrigGroundCheckDistance;
	float m_TurnAmount;
	float m_ForwardAmount;
	Vector3 m_GroundNormal;
	float m_CapsuleHeight;
	Vector3 m_CapsuleCenter;
	CapsuleCollider m_Capsule;
	bool m_Crouching;
    NavMeshAgent agent;
    private bool moving = false;
    protected Vector3 dir;

	void Start()
	{
	    agent = GetComponent<NavMeshAgent>();
        if (Animator == null)
		    Animator = GetComponent<Animator>();
		m_Rigidbody = GetComponent<Rigidbody>();

		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}
    
	public bool Move(Vector3 move, bool crouch, bool jump)
	{
        return agent.SetDestination(move);
	}

    public void MoveToDir(Vector3 dir)
    {
        if (dir != Vector3.zero)
            this.dir = dir;
        m_Rigidbody.velocity = dir;
        UpdateAnimator(dir);

    }

    protected virtual void UpdateRotation(Vector3 dir)
    {
        dir = transform.InverseTransformDirection(dir);
        dir = Vector3.ProjectOnPlane(dir, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(dir.x, dir.z);
        m_ForwardAmount = dir.z;
        ApplyExtraTurnRotation();
    }

    public bool IsPathComplete()
    {
        return agent.remainingDistance < 1;
    }

    void Update()
    {
        UpdateCharacter();
    }

    protected virtual void UpdateCharacter()
    {

        if (agent != null)
        {
            UpdateRotation(agent.velocity);
            UpdateAnimator(agent.velocity);
        }
    }

    
	void UpdateAnimator(Vector3 move)
	{
        float speed = move.magnitude;
	    moving = speed > WALK;
        Animator.SetBool(ANIM_WALK, moving);


	}
    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void SetDeath()
    {
        Animator.SetBool(ANIM_DEATH,true);
    }

    public void SetSpped(float speed)
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (agent != null)
            agent.speed = speed;
    }

    public void Dead()
    {
        if (agent != null)
        {
            agent.Stop();
            agent.speed = 0;
        }

    }

    public void PlayAttack()
    {
        Animator.SetTrigger("attack");
        
    }
}

