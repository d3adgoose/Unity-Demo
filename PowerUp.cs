using UnityEngine;

public enum PowerUpType { WeaponUpgrade, ShieldBoost }

public class Power : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 10f; // Duration for effect, customizable in Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case PowerUpType.WeaponUpgrade:
                    other.GetComponent<PlayerController>().ActivateWeaponUpgrade(duration);
                    break;
                case PowerUpType.ShieldBoost:
                    other.GetComponent<PlayerController>().ActivateShieldBoost(duration);
                    break;
            }
            Destroy(gameObject); // Destroy the power-up object after collection
        }
    }
}