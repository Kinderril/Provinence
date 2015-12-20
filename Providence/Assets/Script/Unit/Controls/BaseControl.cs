using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class BaseControl : MonoBehaviour
{
    private const string ANIM_WALK = "walk";
    private const string ANIM_DEATH = "death";
    protected const string ANIM_ATTACK = "attack";
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

    protected virtual void RotateToTarget(Transform tr2rotate,Vector3 dir)
    {
        dir = tr2rotate.InverseTransformDirection(dir);
        m_TurnAmount = Mathf.Atan2(dir.x, dir.z);
        m_ForwardAmount = dir.z;
        float turnSpeed = 360;//Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
     //   Debug.Log("m_ForwardAmount: " + m_ForwardAmount + "   turnSpeed:" + turnSpeed + "  m_TurnAmount:"+ m_TurnAmount.ToString("0.000") );
        tr2rotate.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
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

    public virtual void PlayAttack()
    {
        Animator.SetTrigger(ANIM_ATTACK);
    }
}

