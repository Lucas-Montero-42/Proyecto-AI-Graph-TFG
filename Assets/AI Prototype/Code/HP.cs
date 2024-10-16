using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class HP : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHealth = 1;
    [SerializeField] private int Health = 0;

    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDMG()
    {
        Health--;
    }
    public void Heal()
    {
        Health++;
    }
}
