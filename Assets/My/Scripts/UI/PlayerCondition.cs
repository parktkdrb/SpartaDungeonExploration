using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePgsicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour,  IDamagable
{
    public UIconditions uiCondition;

    Conditions heath { get { return uiCondition.health; } }
    Conditions hunger {  get { return uiCondition.hunger; } }
    Conditions stamina {  get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;

    public event Action onTakeDamage;

    // Update is called once per frame
    void Update()
    {
        hunger.Subtract(hunger.passibeValue * Time.deltaTime);
        stamina.Add(stamina.passibeValue * Time.deltaTime);

        if(hunger.curValue == 0f)
        {
            heath.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (heath.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        heath.Add(amount);
    }
    public void Eat(float amount)
    {
       hunger.Add(amount);
    }


    private void Die()
    {
        Debug.Log("죽었다.");
    }
    public void TakePgsicalDamage(int damage)
    {
        heath.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        Debug.Log($"스테미나 감소{stamina.curValue}");
        if(stamina.curValue - amount <= 0f)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;

    }
}
