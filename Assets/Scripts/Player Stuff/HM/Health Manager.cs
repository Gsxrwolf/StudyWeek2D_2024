using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private bool playerIsDead = true;
    private bool karenIsDead = true;
    private int playerHealth = 50;
    private int karenHealth = 20;
    private int playerDamage = 10;
    private int karenDamage = 10;

    class Human
    {
        public int health { get; set; }
        public int damage { get; private set; }

    }
    class Player : Human
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        karenIsDead = false;
        playerIsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TakeDamage(GameObject Instigator, int damage)
    {
        Human Karen = new Human();
        Player Player1 = new Player();

        if (playerIsDead == false)
        {
            if (Player1.health > 0)
            {
                

            }
            if (Player1.health <= 0)
            {
                playerIsDead = true;
            }

        }
        if (!karenIsDead)
        { 
            if (Karen.health > 0)
            {


            }
            if (Karen.health <= 0)
            {
                karenIsDead = true;
            }
        }
    }
    private void AddHealth(GameObject Player1)
    {

    }

}
