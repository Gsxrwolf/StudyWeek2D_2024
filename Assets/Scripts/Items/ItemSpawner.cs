using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Editor Setting -> Holds all the spawn points for items
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();

    // Editor Setting -> Determines how many items should be spawned
    [SerializeField] private int _count = 3;

    // Editor Setting -> Determines which items can be spawned by this spawner
    [SerializeField] private List<GameObject> _availableItems = new List<GameObject>();

    // Private Memeber -> The currently available spawn points
    private List<GameObject> _freeSpawns = new List<GameObject>();




    // On first frame
    private void Start()
    {
        // All spawn points free
        _freeSpawns = _spawnPoints;

        // Spawn items
        SpawnItems();
    }


    #region Functions

    /// <summary>
    /// Spawns all items (based on count) at all free spawn points.
    /// </summary>
    private void SpawnItems()
    {
        // Make sure we have spawn points and available items and enough space
        if(_spawnPoints.Count <= 0 && _availableItems.Count <= 0 && _freeSpawns.Count < _count)
        {
            return;
        }

        // Loop for count and spawn items at random location
        for(int i = 0; i < _count; i++)
        {
            // Get random spawn point
            GameObject spawnPoint = _freeSpawns[Random.Range(0, _freeSpawns.Count)];

            // Spawn random item
            GameObject item = Instantiate(_availableItems[Random.Range(0, _availableItems.Count)]);
            item.transform.position = spawnPoint.transform.position;

            // Remove spawn point from free spawns
            _freeSpawns.Remove(spawnPoint);
        }
    }

    /// <summary>
    /// Returns boolean if chance was met
    /// </summary>
    /// <param name="chance">The chance you want to check for</param>
    /// <returns>Succeed or Failed</returns>
    private bool SpawnChance(int chance)
    {
        return Random.Range(1 , 100) <= chance;
    }

    /// <summary>
    /// Can be used to spawn a random item on the position of a karen.
    /// Use this on karens death event to drop random loot.
    /// </summary>
    /// <param name="karen">The karen</param>
    /// <param name="chance">spawn chance (0 low -> 100 high)</param>
    /// <param name="forceDrop">100% drop sum</param>
    public void KarenDrop(GameObject karen, int chance, bool forceDrop = false)
    {
        // make sure the karen is valid
        if(karen is null)
        {
            return;
        }

        // if drop is forced
        if(forceDrop)
        {
            // Spawn random item
            GameObject item = Instantiate(_availableItems[Random.Range(0, _availableItems.Count)]);
            item.transform.position = karen.transform.position;
        }
        else
        {
            // Random chance if item should be spawned
            if(SpawnChance(chance))
            {
                // Spawn random item
                GameObject item = Instantiate(_availableItems[Random.Range(0, _availableItems.Count)]);
                item.transform.position = karen.transform.position;
            }
        }
    }

    #endregion
}
