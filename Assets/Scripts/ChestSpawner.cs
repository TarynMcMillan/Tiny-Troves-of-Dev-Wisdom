using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] chestPrefabs;
    [SerializeField] private GameObject explosionPrefab;

    [Header("Internal References")]
    GameObject selectedChest;
    
    private NewPlayerMovement player;
    private AudioSource audioSource;
    List<GameObject> spawnList = new List<GameObject>();

    private void Start()
    {
        player = FindObjectOfType<NewPlayerMovement>();
        audioSource = GetComponent<AudioSource>();
        if (player == null)
        {
            Debug.LogError("Can't find the Player!");
        }
        else
        {
            SpawnChests();
        }
    }

    private void SpawnChests()
    {
        spawnList.Clear();
        
        foreach (Transform pos in spawnPositions)
        {
            GameObject chestInstance = Instantiate(chestPrefabs[Random.Range(0, chestPrefabs.Length)], pos.position, Quaternion.identity);
            spawnList.Add(chestInstance);
        }
    }

    public void DisplayAdvice(GameObject chest)
    {
        selectedChest = chest;
        
        foreach (GameObject chestInstance in spawnList)
        {
            chestInstance.GetComponent<BoxCollider2D>().enabled = false;
            
            if (chestInstance != selectedChest)
            {
                GameObject explosion = Instantiate(explosionPrefab, chestInstance.transform.position, Quaternion.identity);
                Destroy(chestInstance);
                Destroy(explosion, 2f);
            }
        }

        audioSource.Play();
        StartCoroutine(StartWaving());
        
        FindObjectOfType<AdviceManager>().GenerateAdvice();
            
        StartCoroutine(RespawnChests());
    }

    IEnumerator StartWaving()
    {
        player.GetComponent<Animator>().SetBool("isWaving", true);

        yield return new WaitForSeconds(1f);
        player.GetComponent<Animator>().SetBool("isWaving", false);
    }

    private void DisablePlayerMovement()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    IEnumerator RespawnChests()
    {
        yield return new WaitForSeconds(2f);
        Destroy(selectedChest);
        SpawnChests();
    }

    private void EnablePlayerMovement()
    {
    }
}
