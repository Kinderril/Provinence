using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class BaseControl : MonoBehaviour
{
    private const string ANIM_WALK = "walk";
    private const string ANIM_DEATH = "death";
    private const float WALK = 0.000001f;

	[SerializeField] float m_MovingTurnSpeed = 360;
	[SerializeField] float m_StationaryTurnSpeed = 180;

	protected Rigidbody m_Rigidbody;
	public Animator Animator;
	float m_TurnAmount;
	float m_ForwardAmount;
    private bool moving = false;
    public Vector3 Direction;
    public Vector3 TargetDirection;

	void Awake()
	{
	    Init();
	}
    public virtual bool IsPathComplete()
    {
        return true;
    }

    public virtual void SetSpped(float speed)
    {

    }

    protected virtual void Init()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        
    }
    public virtual bool MoveTo(Vector3 v)
    {
        return true;
    }

    protected virtual void UpdateRotation(Vector3 d)
    {
    }

    protected virtual void RotateToTarget(Vector3 dir)
    {
        dir = transform.InverseTransformDirection(dir);
        // Direction = Vector3.ProjectOnPlane(Direction, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(dir.x, dir.z);
        m_ForwardAmount = dir.z;
        ApplyExtraTurnRotation();
    }

    public void UpdateFromUnit()
    {
        UpdateCharacter();
    }

    protected virtual void UpdateCharacter()
    {
    }

    public void SetToDirection(Vector3 dir)
    {
        TargetDirection = dir;
    }
    
	protected void UpdateAnimator(Vector3 move)
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

    public virtual void Stop()
    {

    }

    public void Dead()
    {
        Stop();
    }

    public void PlayAttack()
    {
        Animator.SetTrigger("attack");
        
    }
}

