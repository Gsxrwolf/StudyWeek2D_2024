using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvents : MonoBehaviour
{
    // Private Memeber -> Player Controller
    [SerializeField] private PlayerController _playerController;

    #region Public Functions

    /// <summary>
    /// Adds 5 health to the player.
    /// </summary>
    public void AddHealth(GameObject _player)
    {
        // Negative damage should be + health
        _playerController.DealDamage(-5);
    }

    /// <summary>
    /// Adds 1 health / sec for 5 seconds.
    /// </summary>
    public void AddHealthOverTime(GameObject _player)
    {
        StartCoroutine(HealthTimer());
    }

    /// <summary>
    /// Dubles the players damage for 5 seconds.
    /// </summary>
    public void DoubleDamage(GameObject _player)
    {
        StartCoroutine(DoubleDamageTimer());
    }

    /// <summary>
    /// Doubles the players speed for 3 seconds
    /// </summary>
    public void ExtraSpeed(GameObject _player)
    {
        StartCoroutine(DoubleSpeedTimer());
    }

    /// <summary>
    /// Doubles the players jump power for 3 seconds
    /// </summary>
    public void ExtraJumpPower(GameObject _player)
    {
        StartCoroutine(DoubleJumpTimer());
    }

    #endregion

    #region Routines

    private IEnumerator HealthTimer()
    {
        int count = 0;

        while(count < 5)
        {
            _playerController.DealDamage(-1);            

            // Wait for one sec
            yield return new WaitForSeconds(1);

            // Increase counter
            count++;
        }
    }

    private IEnumerator DoubleDamageTimer()
    {
        //int originalDamage = _player.Damage;

        //_player.Damage = originalDamage * 2;

        yield return new WaitForSeconds(5);

        //_player.Damage = originalDamage;
    }

    private IEnumerator DoubleSpeedTimer()
    {
        //float originalSpeed = _player.Speed;

        //_player.Speed = originalSpeed * 2;

        yield return new WaitForSeconds(3);

        //_player.Speed = originalSpeed;
    }

    private IEnumerator DoubleJumpTimer()
    {
        //float originalJP = _player.JumpPower;

        //_player.JumpPower = originalJP * 2;

        yield return new WaitForSeconds(3);

        //_player.JumpPower = originalJP;
    }


    #endregion
}
