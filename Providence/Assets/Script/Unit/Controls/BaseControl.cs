using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class BaseControl : MonoBehaviour
{
    private const string ANIM_WALK = "walk";
    private const string ANIM_DEATH = "death";
    protected const string ANIM_ATTACK = "attack";
    private const float WALK = 0.000001f;

	[SerializeField] float m_MovingTurnSpeed = 660;
	[SerializeField] float m_StationaryTurnSpeed = 180;

	protected Rigidbody m_Rigidbody;
	public Animator Animator;
	float m_TurnAmount;
	float m_ForwardAmount;
    private bool moving = false;
    public Vector3 Direction;
    protected Vector3 targetDirection;
    public RotateByQuaterhnion ThisByQuaterhnion;

    public Vector3 TargetDirection
    {
        get { return targetDirection; }
    }
    public Vector3 Velocity
    {
        get { return m_Rigidbody.velocity; }
    }

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
        ThisByQuaterhnion = GetComponent<RotateByQuaterhnion>();
        ThisByQuaterhnion.Init(null,OnComeRotation);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        
    }

    private void OnComeRotation()
    {

    }

    public virtual bool MoveTo(Vector3 v)
    {
        return true;
    }

    protected virtual void UpdateRotation(Vector3 d)
    {
    }

    protected void RotateToTarget()
    {
//        ThisByQuaterhnion.UpdateRotate();
        /*
        dir = tr2rotate.InverseTransformDirection(dir);
        m_TurnAmount = Mathf.Atan2(dir.x, dir.z);
        m_ForwardAmount = dir.z;
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        tr2rotate.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
         */ 
    }

    public void UpdateFromUnit()
    {
        UpdateCharacter();
    }

    protected virtual void UpdateCharacter()
    {
    }

    public virtual void SetToDirection(Vector3 dir)
    {
        var angle = Vector3.Angle(TargetDirection, dir);
        targetDirection = dir;
        if (angle > 2)
        {
            ThisByQuaterhnion.SetLookDir(targetDirection);
        }
    }
    public void SetToDirection(Vector3 dir,int side)
    {
        targetDirection = dir;
        ThisByQuaterhnion.SetLookDir(targetDirection, side);
//        var angle = Vector3.Angle(TargetDirection, dir);
//        targetDirection = dir;
//        if (angle > 2)
//        {
//        }
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

    public virtual void Stop(bool setSpeedToZero = true)
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

