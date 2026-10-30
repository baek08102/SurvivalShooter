using System.Security;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;

public class Monster : Entities
{
    public static readonly int IdHasTarget = Animator.StringToHash("HasTarget");
    public static readonly int IdDie = Animator.StringToHash("Die");
    public enum Status
    {
        Idle,
        Trace,
        Attack,
        Die
    }

    public Transform target;
    public LayerMask targetLayer;

    public float traceDistance = 50f;
    public float attackDistance = 2f;
    public float attackDelay = 1f;
    public float deactiveTime = 3f;

    public ParticleSystem shotEffect;
    public Collider monsterCollider;
    public Renderer monsterRenderer;
    public AudioClip hitClip;
    public AudioClip deathlip;

    private NavMeshAgent agent;
    private Animator monsterAnimator;
    private AudioSource monsterAudioSource;

    private Status currentStatus;

    public Status CurrentStatus
    {
        get => currentStatus;
        set
        {
            currentStatus = value;
            switch (currentStatus)
            {
                case Status.Idle:
                    monsterAnimator.SetBool(IdHasTarget, false);
                    agent.isStopped = true;
                    break;
                case Status.Trace:
                    monsterAnimator.SetBool(IdHasTarget, true);
                    agent.isStopped = false;
                    break;
                case Status.Attack:
                    monsterAnimator.SetBool(IdHasTarget, false);
                    agent.isStopped = true;
                    break;
                case Status.Die:
                    agent.isStopped = true;
                    monsterAnimator.SetTrigger(IdDie);
                    monsterCollider.enabled = false;
                    monsterAudioSource.PlayOneShot(deathlip);
                    break;
            }
        }
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        monsterAnimator = GetComponent<Animator>();
        monsterAudioSource = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        agent.enabled = true;

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, traceDistance, targetLayer))
        {
            agent.Warp(hit.position);
        }

        monsterCollider.enabled = true;

        CurrentStatus = Status.Idle;
    }

    private void ODisable()
    {
        if (agent.isOnNavMesh)
        {
            agent.ResetPath();
        }
    }

    private void Update()
    {
        switch (currentStatus)
        {


        }
    }
}
