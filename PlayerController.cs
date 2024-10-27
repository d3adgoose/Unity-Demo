using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector3 velocity;
    public float MAX_SPEED;

    public float last_shot;
    public float fire_delay;
    public GameObject bullet;
    private bool isWeaponUpgraded = false;
    private float upgradeDuration = 0;
    private bool isShieldActive = false;
    private float shieldDuration = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * Time.deltaTime * MAX_SPEED, Space.Self);

        Vector3 mouse = Mouse.current.position.ReadValue();
        mouse.z = Camera.main.transform.position.y;
        Vector3 world = Camera.main.ScreenToWorldPoint(mouse);
        world.y = transform.position.y;
        transform.LookAt(world);

        // Update power-up durations and deactivate if time expired
        if (isWeaponUpgraded && (Time.time > upgradeDuration))
        {
            DeactivateWeaponUpgrade();
        }
        if (isShieldActive && (Time.time > shieldDuration))
        {
            DeactivateShieldBoost();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        velocity.x = move.x;
        velocity.z = move.y;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && last_shot + fire_delay < Time.time)
        {
            var new_bullet = Instantiate(bullet, transform.position + transform.forward * 3, transform.rotation);
            var ctrl = new_bullet.GetComponent<BulletController>();
            if (isWeaponUpgraded)
            {
                // Enhanced bullet properties when weapon is upgraded
                ctrl.speed = 60; // Increased speed
                ctrl.damage = 20; // Increased damage
            }
            else
            {
                ctrl.speed = 40; // Normal speed
                ctrl.damage = 10; // Normal damage
            }
            ctrl.lifetime = 3;
            ctrl.player = true;
            last_shot = Time.time;
        }
    }

    public void ActivateWeaponUpgrade(float duration)
    {
        isWeaponUpgraded = true;
        upgradeDuration = Time.time + duration;
        // Additional effects can be added here
    }

    public void DeactivateWeaponUpgrade()
    {
        isWeaponUpgraded = false;
        // Revert any effects that were changed
    }

    public void ActivateShieldBoost(float duration)
    {
        isShieldActive = true;
        shieldDuration = Time.time + duration;
        // Increase player health or add a shield value here
    }

    public void DeactivateShieldBoost()
    {
        isShieldActive = false;
        // Reduce player health or remove the shield value
    }
}
