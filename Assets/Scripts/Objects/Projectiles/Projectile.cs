﻿using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] protected float lifetime;
    protected float lifetimeCountdown;
    [SerializeField] public float projectileSpeed;

    [Tooltip("How long to delay calling `Destroy` after hitting a collider.")]
    [SerializeField] protected float destroyDelay;

    public string targetTag { get; private set; }

    public new Rigidbody2D rigidbody { get; protected set; }
    protected new Collider2D collider;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        lifetimeCountdown = lifetime;
    }

    protected virtual void Update() => LifetimeCountdown();

    protected void LifetimeCountdown()
    {
        lifetimeCountdown -= Time.deltaTime;
        if (lifetimeCountdown <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected void OnCollisionEnter2D(Collision2D other) => OnHitCollider(other.transform);
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            OnHitCollider(other.transform);
        }
    }

    protected void OnHitCollider(Transform other)
    {
        if (destroyDelay > 0)
        {
            // Prevent this projectile from any other interactions
            collider.enabled = false;
            
            // Attach the rigidbody to the transform it collided with
            Destroy(rigidbody);
            transform.SetParent(other);

            lifetimeCountdown = destroyDelay;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OverrideSpeed(float speed)
    {
        projectileSpeed = speed;
        rigidbody.velocity = rigidbody.velocity.normalized * projectileSpeed;
    }

    public void OverrideDamage(float damage)
    {
        var hitbox = GetComponent<DamageOnTrigger>();
        if (hitbox != null)
        {
            hitbox.damage = damage;
        }
    }

    public void OverrideDamage(float damage, bool isCritical)
    {
        var hitbox = GetComponent<DamageOnTrigger>();
        if (hitbox != null)
        {
            hitbox.damage = damage;
            hitbox.isCritical = isCritical;
        }
    }

    public static Projectile Instantiate(GameObject prefab, Vector2 position, Vector2 direction, Quaternion rotation, string targetTag)
    {
        var projectile = GameObject.Instantiate(prefab, position, rotation).GetComponent<Projectile>();
        projectile.targetTag = targetTag;
        projectile.rigidbody.velocity = direction.normalized * projectile.projectileSpeed;
        return projectile;
    }

    public static Quaternion CalculateRotation(Vector2 direction)
    {
        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg * Vector3.forward;
        return Quaternion.Euler(rotation);
    }
}
