﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Character
{
    [SerializeField] private XPSystem levelSystem = default;
    [SerializeField] private float _speed = default;
    private float speed => (Input.GetButton("Run")) ? _speed * 2 : _speed;

    private Vector3 change;

    [SerializeField] private ConstrainedFloat _lumen = default;
    public ConstrainedFloat lumen => _lumen;
    [SerializeField] private ConstrainedFloat _mana = default;
    public ConstrainedFloat mana => _mana;
    [SerializeField] private ConstrainedFloat _health = default;
    public ConstrainedFloat healthMeter => _health;
    public override float health {
        get => _health.current;
        set {
            _health.current = value;
            if (_health.current <= 0) {
                StartCoroutine(DeathCo());
            }
        }
    }

    public VectorValue startingPosition;

    public Inventory inventory;
    [SerializeField] private Item arrow = default;

    public SpriteRenderer receivedItemSprite;

    [Header("Hitboxes")]
    [SerializeField] private DamageOnTrigger[] directionalAttacks = default;
    [SerializeField] private DamageOnTrigger roundAttack = default;
    [SerializeField] private PolygonCollider2D[] hitBoxColliders = default;


    [Header("Projectiles")]
    [SerializeField] private float arrowSpeed = 1;
    public GameObject projectile; //arrows and so on

    [Header("Sound FX")]
    [SerializeField] private AudioClip[] attackSounds = default;
    [SerializeField] private AudioClip levelUpSound = default;

    [Header("Lamp")]
    [SerializeField] private LampLight lamp = default;
    //############### LIFT-TEST      ##############
    //  public GameObject thing;
    public SpriteRenderer thingSprite;
    //############### LIFT-TEST-ENDE ##############
    [SerializeField] private SpriteSkinRPC weaponSkin = default;


    private void OnEnable() => levelSystem.OnLevelChanged += LevelUpPlayer;
    private void OnDisable() => levelSystem.OnLevelChanged -= LevelUpPlayer;

    private void LevelUpPlayer()
    {
        _health.max = _health.max + 10;
        _mana.max = _mana.max + 10;
        _health.current = _health.max;
        _mana.current = _mana.max;
        currentState = State.idle;
        SoundManager.RequestSound(levelUpSound);
        if (effectAnimator)
        {
            effectAnimator.Play("LevelUp");
        }
    }

    private void Start()
    {
        SetAnimatorXY(Vector2.down);
        currentState = State.walk;
        transform.position = startingPosition.value;
    }

    private AudioClip GetAttackSound() => attackSounds[Random.Range(0, attackSounds.Length)];
    private AudioClip GetLevelUpSound() => levelUpSound;

    private void Update()
    {
        // Is the player in an interaction?
        if (currentState == State.interact)
        {
            // Debug.Log("helpmeout");
            return;
        }

        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        SetAnimatorXY(change);

        animator.SetBool("isRunning", Input.GetButton("Run"));

        var notStaggeredOrLifting = (currentState != State.stagger && currentState != State.lift);

        if (Input.GetButtonDown("Attack") && currentState != State.attack && notStaggeredOrLifting && inventory.currentWeapon != null)
        {
            StartCoroutine(AttackCo());
        }
        //########################################################################### Round Attack if Mana > 0 ##################################################################################
        if (Input.GetButton("RoundAttack") && currentState != State.roundattack && notStaggeredOrLifting && inventory.currentWeapon != null && mana.current > 0)  //Getbutton in GetButtonDown für die nicht dauerhafte Abfrage
        {
            StartCoroutine(RoundAttackCo());
        }
        //########################################################################### Bow Shooting with new Inventory ##################################################################################
        if (Input.GetButton("UseItem") && currentState != State.roundattack && notStaggeredOrLifting && currentState != State.attack)
        {
            if (arrow != null && inventory.items[arrow] > 0 && inventory.currentBow)
            {
                inventory.items[arrow]--;
                StartCoroutine(SecondAttackCo());
            }
        }
        //############################################################################### Spell Cast ###############################################################################
        if (Input.GetButton("SpellCast") && inventory.currentSpellbook && mana.current > 0 && notStaggeredOrLifting && currentState != State.attack)
        {
            StartCoroutine(SpellAttackCo());
        }

        if (Input.GetButtonDown("Lamp") && inventory.currentLamp && lumen.current > 0)
        {
            lamp.enabled = !lamp.enabled;
        }
        //##############################################################################################################################################################

        if (Input.GetButtonUp("UseItem"))
        {
            animator.SetBool("isShooting", false);
        }

       animator.SetBool("isHurt", (currentState == State.stagger));
      /*
        if (currentState == State.stagger)
        {
            animator.Play("Hurt");
        }
      */
        animator.SetBool("Moving", (change != Vector3.zero));

        // ################################# Trying to drop things ################################################################
        // if (Input.GetButtonDown("Lift") && currentState == State.lift)
        // {
        //     LiftItem();
        //     Debug.Log("Item Dropped!");

        // }
        // ################################# Trying to drop things END ############################################################
    }

    private void FixedUpdate()
    {
        if (currentState == State.walk || currentState == State.idle || currentState == State.lift)
        {
            rigidbody.MovePosition(transform.position + change.normalized * speed * Time.deltaTime);
        }

        if (currentState != State.stagger)
        {
            rigidbody.velocity = Vector2.zero;
        }
    }

    public bool IsCriticalHit() => (inventory.totalCritChance > 0 && Random.Range(0, 99) <= inventory.totalCritChance);

    // #################################### Casual Attack ####################################
    private IEnumerator AttackCo()
    {
        //###################ALTERING HITBOX###############################
        /*
        Debug.Log("Start altering Uphitbox-Points");
        Vector2 point0 = new Vector2(0.5f, 0.7f);
        Vector2 point1 = new Vector2(0.4f, 0.9f);
        Vector2 point2 = new Vector2(0.6f, 1.1f);
        Vector2 point3 = new Vector2(-0.5f, 0.8f);
        Vector2 point4 = new Vector2(-0.25f, 0.5f);
        Vector2 point5 = new Vector2(0.2f, 0.5f);
        Vector2 point6 = new Vector2(0.2f, 0.5f);

        var hitBoxOriginalPoints = hitBoxColliders[0].points;
        hitBoxColliders[0].points = new[] { point0, point1, point2, point3, point4, point5, point6 };

        for(int i = 0;  i < 6; i++)
        {
            Debug.Log("Point " + i + " is " + hitBoxColliders[0].points[i]);
        }
   
        Debug.Log("Stop altering Uphitbox-Points");
        */
        //###################ALTERING HITBOX###############################


        var currentWeapon = inventory.currentWeapon;
        hitBoxColliders[0].points = currentWeapon.upBox;
        hitBoxColliders[1].points = currentWeapon.downBox;
        hitBoxColliders[2].points = currentWeapon.rightBox;
        hitBoxColliders[3].points = currentWeapon.leftBox;
        weaponSkin.newSprite = currentWeapon.weaponSkin;


        var isCritical = IsCriticalHit();
        for (int i = 0; i < directionalAttacks.Length; i++)
        {
            directionalAttacks[i].damage = Random.Range(inventory.currentWeapon.minDamage, inventory.currentWeapon.maxDamage +1 );
            directionalAttacks[i].isCritical = isCritical;
        }

        SoundManager.RequestSound(GetAttackSound());

            animator.SetBool("Attacking", true);
            currentState = State.attack;
            yield return null;
            animator.SetBool("Attacking", false);

        yield return new WaitForSeconds(0.3f);

        if (currentState != State.interact)
        {
            currentState = State.walk;
        }

    }

    // ############################# Roundattack ################################################
    private IEnumerator RoundAttackCo()
    {
        roundAttack.damage = Random.Range(inventory.currentWeapon.minDamage, inventory.currentWeapon.maxDamage + 1);
        roundAttack.isCritical = IsCriticalHit();
        //! Is this missing a sound request?
        animator.SetBool("RoundAttacking", true);
        currentState = State.roundattack;
        yield return null;  //! This allows a round attack to be executed every other frame when the input is held, causing mana to drain very quickly
        animator.SetBool("RoundAttacking", false);
        currentState = State.walk;

        mana.current -= 1;
    }

    // ############################## Using the Item / Shooting the Bow #########################################
    private IEnumerator SecondAttackCo()
    {
        currentState = State.attack;
        animator.SetBool("isShooting", true);
        CreateProjectile(projectile, arrowSpeed, Random.Range(inventory.currentBow.minDamage, inventory.currentBow.maxDamage + 1));

        yield return new WaitForSeconds(0.3f);

        if (currentState != State.interact)
        {
            currentState = State.walk;
        }
    }

    private void CreateProjectile(GameObject projectilePrefab, float projectileSpeed, float projectileDamage)
    {
        var position = new Vector2(transform.position.x, transform.position.y + 0.5f); // Pfeil höher setzen
        var direction = new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        var proj = Instantiate(projectilePrefab, position, Projectile.CalculateRotation(direction)).GetComponent<Projectile>();
        proj.rigidbody.velocity = direction.normalized * projectileSpeed; // This makes the object move
        var hitbox = proj.GetComponent<DamageOnTrigger>();
        hitbox.damage = projectileDamage;    //replace defaultvalue with the value given from the makespell()/playervalue
        hitbox.isCritical = IsCriticalHit();  // gets written into Derived class
    }

    // ############################## Using the SpellBook /Spellcasting #########################################
    private IEnumerator SpellAttackCo()
    {
        animator.SetBool("isCasting", true); // Set to cast Animation
        currentState = State.attack;
        MakeSpell();
        yield return new WaitForSeconds(0.3f);
        if (currentState != State.interact)
        {
            currentState = State.walk;
        }
        animator.SetBool("isCasting", false);

    }

    //################### instantiate spell when casted ###############################
    private void MakeSpell()
    {

        var prefab = inventory.currentSpellbook.prefab;
        var speed = inventory.currentSpellbook.speed;
        CreateProjectile(prefab, speed,Random.Range(inventory.totalMinSpellDamage, inventory.totalMaxSpellDamage + 1));
        mana.current -= inventory.currentSpellbook.manaCosts;

    }

    //#################################### Item Found RAISE IT! #######################################

    public void RaiseItem()
    {
        if (inventory.currentItem != null)
        {
            if (currentState != State.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = State.interact;
                receivedItemSprite.sprite = inventory.currentItem.sprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = State.idle;
                receivedItemSprite.sprite = null;
                inventory.currentItem = null;
            }
        }
    }

    // #################################### LIFT Item ######################################
    /* 
       public void LiftItem()
       {
           if (playerInventory.currentItem != null)
           {
               if (currentState != State.lift)
               {             
                   animator.SetBool("isCarrying", true);
                   currentState = State.lift;
                   Debug.Log("State = Lift");
                   thingSprite.sprite = myInventory.currentItem.itemImage;
               }
               else
               {   
                   animator.SetBool("isCarrying", false);
                   currentState = State.idle;
                   Debug.Log("State = idle");
                   thingSprite.sprite = null;

                   myInventory.currentItem = null;
               }
           }
       }
    */

    public override void TakeDamage(float damage, bool isCritical)
    {
        if (!isInvulnerable)
        {
            inventory.CalcDefense();
            var finalDamage = damage - inventory.totalDefense;
            if (finalDamage > 0)
            {
                health -= finalDamage;
                DamagePopUpManager.RequestDamagePopUp(finalDamage, isCritical, transform);
                iframes?.TriggerInvulnerability();
            }
            else
            {
                DamagePopUpManager.RequestDamagePopUp(0, isCritical, transform);
            }
        //    Debug.Log(finalDamage + " damage after defense calculation.");
        }
    }

    // ########################### Getting hit and die ##############################################

    public override void Knockback(Vector2 knockback, float duration)
    {
        if (currentState != State.stagger && this.gameObject.activeInHierarchy)
        {
            StartCoroutine(KnockbackCo(knockback, duration));
        }
    }

    //##################### Death animation and screen ##############################

    private IEnumerator DeathCo()
    {
        currentState = State.dead;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("DeathMenu");
    }
}
