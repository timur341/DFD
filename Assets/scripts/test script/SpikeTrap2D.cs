using UnityEngine;

public class SpikeTrap2D : MonoBehaviour
{
    [Header("Timing Settings")]
    [Tooltip("Delay before spikes appear when triggered")]
    [SerializeField] private float activationDelay = 0.5f;
    [Tooltip("How long spikes stay active")]
    [SerializeField] private float activeDuration = 1.5f;
    [Tooltip("Cooldown between activations")]
    [SerializeField] private float cooldown = 3f;

    [Header("Combat Settings")]
    [Tooltip("Damage dealt to player/enemies")]
    [SerializeField] private int damage = 1;
    [Tooltip("Knockback force when hit")]
    [SerializeField] private float knockbackForce = 5f;

    [Header("Visual References")]
    [SerializeField] private GameObject spikesVisual;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem activationParticles;

    [Header("Collider References")]
    [SerializeField] private Collider2D spikeCollider;

    private enum TrapState { Ready, Activating, Active, Cooldown }
    private TrapState currentState = TrapState.Ready;
    private float stateTimer;

    private void Start()
    {
        SetSpikesActive(false);
    }

    private void Update()
    {
        stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case TrapState.Ready:
                // Waiting for activation (could be triggered by player proximity)
                break;

            case TrapState.Activating:
                if (stateTimer >= activationDelay)
                {
                    SetSpikesActive(true);
                    currentState = TrapState.Active;
                    stateTimer = 0f;
                }
                break;

            case TrapState.Active:
                if (stateTimer >= activeDuration)
                {
                    SetSpikesActive(false);
                    currentState = TrapState.Cooldown;
                    stateTimer = 0f;
                }
                break;

            case TrapState.Cooldown:
                if (stateTimer >= cooldown)
                {
                    currentState = TrapState.Ready;
                    stateTimer = 0f;
                }
                break;
        }
    }

    // Call this method to activate the trap (e.g. from trigger zone)
    public void ActivateTrap()
    {
        if (currentState == TrapState.Ready)
        {
            currentState = TrapState.Activating;
            stateTimer = 0f;

            if (animator != null)
                animator.SetTrigger("Activate");

            if (activationParticles != null)
                activationParticles.Play();
        }
    }

    private void SetSpikesActive(bool active)
    {
        spikesVisual.SetActive(active);
        spikeCollider.enabled = active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!spikeCollider.enabled) return;

        // Check for damageable objects
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // Apply damage
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            // Apply knockback
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}