using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCBehavior : MonoBehaviour
{
    // Components and movement variables
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private float baseSpeed;
    private float currentSpeed;

    [Header("Speed Settings")]
    public float minSpeed = 2.0f;
    public float maxSpeed = 4.0f;
    public float dashMultiplier = 2.5f;

    [Header("Timing Settings")]
    public float changeDirectionInterval = 2f;
    public float stopChance = 0.3f;  // 30% chance to stop
    public float dashChance = 0.15f; // 15% chance to dash
    public float dashDuration = 0.5f;
    public float stopDuration = 2f;

    [Header("Avoidance Settings")]
    public float avoidanceRadius = 1.5f;
    public float avoidanceStrength = 1.2f;
    public LayerMask npcLayer;

    //boundarys
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;

    public int Bounderys;

    // State flags
    private bool isDashing = false;
    private bool isStopped = false;
    private bool allowStopping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //only for level three, allowing them to stop
        string currentScene = SceneManager.GetActiveScene().name;
        allowStopping = currentScene == "LevelThree";

        // Give each NPC a unique base speed
        baseSpeed = Random.Range(minSpeed, maxSpeed);
        currentSpeed = baseSpeed;

        // Random first direction
        moveDirection = Random.insideUnitCircle.normalized;

        // Start behaviors
        StartCoroutine(ChangeDirectionRoutine());


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //StartMove(moveDirection);
        

        if (!isStopped)
        {
            // Keep within boundaries
            if (transform.position.x < minX || transform.position.x > maxX)
            {
                moveDirection.x = -moveDirection.x;
            }

            if (transform.position.y < minY || transform.position.y > maxY)
            {
                moveDirection.y = -moveDirection.y;
            }
            // Avoid other NPCs
            Vector2 avoidanceForce = AvoidOtherNPCs();

            // Move and steer
            Vector2 finalDir = (moveDirection + avoidanceForce).normalized;
            rb.MovePosition(rb.position + finalDir * currentSpeed * Time.fixedDeltaTime);

            // Face direction (flip sprite)
            if (finalDir.x != 0)
                spriteRenderer.flipX = finalDir.x < 0;

        }
    }

    // Coroutine to change direction, stop, or dash at intervals
    IEnumerator ChangeDirectionRoutine()
    {
        // Only allow stopping if in LevelThree
        while (true)
        {
            // Wait for the interval
            yield return new WaitForSeconds(changeDirectionInterval);

            // Decide action based on probabilities
            float rand = Random.value;

            if (rand < stopChance && !isStopped)
            {
                yield return StartCoroutine(StopRoutine());
            }
            else if (rand < stopChance + dashChance && !isDashing)
            {
                yield return StartCoroutine(DashRoutine());
            }
            else
            {
                moveDirection = Random.insideUnitCircle.normalized;
            }
        }
    }

    // Stop movement for a duration
    IEnumerator StopRoutine()
    {
        isStopped = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stopDuration);
        isStopped = false;
    }

    // Dash movement for a duration
    IEnumerator DashRoutine()
    {
        isDashing = true;
        currentSpeed = baseSpeed * dashMultiplier;
        yield return new WaitForSeconds(dashDuration);
        currentSpeed = baseSpeed;
        isDashing = false;
    }

    // Calculate avoidance force from nearby NPCs
    Vector2 AvoidOtherNPCs()
    {
        // Find nearby NPCs within avoidance radius
        Collider2D[] nearbyNPCs = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius, npcLayer);
        Vector2 avoidance = Vector2.zero;

        // Sum repulsion vectors from each nearby NPC
        foreach (Collider2D npc in nearbyNPCs)
        {
            if (npc.gameObject == gameObject) continue; // skip self
            Vector2 away = (Vector2)(transform.position - npc.transform.position);
            float distance = away.magnitude;
            if (distance > 0)
                avoidance += away.normalized / distance; // stronger repulsion when closer
        }

        // Scale by avoidance strength
        return avoidance * avoidanceStrength;
    }

    // Visualize avoidance radius in editor
    void OnDrawGizmosSelected()
    {
        // Visualize avoidance radius in editor
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }
}
