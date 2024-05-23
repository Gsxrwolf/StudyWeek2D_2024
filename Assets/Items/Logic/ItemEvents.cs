using System.Collections;
using UnityEngine;

public class ItemEvents : MonoBehaviour
{
    // Constant Values
    private const int HealthIncrement = 5;
    private const int HealthOverTimeIncrement = 1;
    private const int HealthOverTimeDuration = 5;
    private const float DoubleDamageDuration = 5f;
    private const float DoubleSpeedDuration = 3f;
    private const float DoubleJumpDuration = 3f;

    // Gets the player controller from game object
    private PlayerController GetController(GameObject player)
    {
        return player.GetComponent<PlayerController>();
    }

    #region Public Functions

    /// <summary>
    /// Adds a fixed amount of health to the player. (5 health points)
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    public void AddHealth(GameObject player)
    {
        var playerController = GetController(player);
        if (playerController == null) return;

        playerController.DealDamage(-HealthIncrement);
    }

    /// <summary>
    /// Gradually adds health to the player over a period of time. (5 health points over 5 seconds)
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    public void AddHealthOverTime(GameObject player)
    {
        var playerController = GetController(player);
        if (playerController == null) return;

        StartCoroutine(HealthTimer(playerController));
    }

    /// <summary>
    /// Temporarily doubles the player's damage for a fixed duration. (5 seconds)
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    public void DoubleDamage(GameObject player)
    {
        var playerController = GetController(player);
        if (playerController == null) return;

        StartCoroutine(DoubleDamageTimer(playerController));
    }

    /// <summary>
    /// Temporarily doubles the player's speed for a fixed duration. (3 seconds)
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    public void ExtraSpeed(GameObject player)
    {
        var playerController = GetController(player);
        if (playerController == null) return;

        StartCoroutine(DoubleSpeedTimer(playerController));
    }

    /// <summary>
    /// Temporarily doubles the player's jump power for a fixed duration. (3 seconds)
    /// </summary>
    /// <param name="player">The player GameObject.</param>
    public void ExtraJumpPower(GameObject player)
    {
        var playerController = GetController(player);
        if (playerController == null) return;

        StartCoroutine(DoubleJumpTimer(playerController));
    }

    #endregion

    #region Routines

    private IEnumerator HealthTimer(PlayerController playerController)
    {
        int count = 0;

        while (count < HealthOverTimeDuration)
        {
            playerController.DealDamage(-HealthOverTimeIncrement);
            yield return new WaitForSeconds(1);
            count++;
        }
    }

    private IEnumerator DoubleDamageTimer(PlayerController playerController)
    {
        float originalDamage = playerController._damage;
        playerController._damage = originalDamage * 2;

        yield return new WaitForSeconds(DoubleDamageDuration);

        playerController._damage = originalDamage;
    }

    private IEnumerator DoubleSpeedTimer(PlayerController playerController)
    {
        float originalSpeed = playerController._speed;
        playerController._speed = originalSpeed * 2;

        yield return new WaitForSeconds(DoubleSpeedDuration);

        playerController._speed = originalSpeed;
    }

    private IEnumerator DoubleJumpTimer(PlayerController playerController)
    {
        float originalJumpForce = playerController._jumpForce;
        playerController._jumpForce = originalJumpForce * 2;

        yield return new WaitForSeconds(DoubleJumpDuration);

        playerController._jumpForce = originalJumpForce;
    }

    #endregion
}