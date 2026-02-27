using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterRandomizer : MonoBehaviour
{
    // Singleton to manage character randomization across scenes
    [Header("List of character prefabs to randomize from")]
    public List<Sprite> characterSprites = new List<Sprite>();

    [Header("References for Player and NPC prefabs")]
    public GameObject playerPrefab;
    public GameObject npcPrefab;

    // Store the chosen player sprite
    private Sprite chosenPlayerSprite;

    // Singleton instance
    public static CharacterRandomizer Instance;

    // Ensure only one instance exists
    private void Awake()
    {
        // Singleton pattern to persist between scenes
        if (Instance == null)
        {
            // First instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Subscribe to scene loaded event
            SceneManager.sceneLoaded += OnLevelLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Initial randomization on start
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-randomize each time a level loads
        RandomizeCharacters();
    }

    // Randomly select a character for the player and assign others as NPCs
    public void RandomizeCharacters()
    {
        // Clear existing characters in the scene
        if (characterSprites.Count == 0)
        {
            Debug.LogError("No character sprites assigned!");
            return;
        }

        // Pick a random sprite for the player
        int randomIndex = Random.Range(0, characterSprites.Count);
        chosenPlayerSprite = characterSprites[randomIndex];

        // Instantiate player with chosen sprite
        CreatePlayer(chosenPlayerSprite);

        // Create NPCs for all other sprites
        for (int i = 0; i < characterSprites.Count; i++)
        {
            // Skip the chosen player sprite
            if (i != randomIndex)
                CreateNPC(characterSprites[i]);
        }

        Debug.Log($"Player character: {chosenPlayerSprite.name}");
    }

    // Instantiate player character
    void CreatePlayer(Sprite sprite)
    {
        GameObject player = Instantiate(playerPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        player.GetComponent<SpriteRenderer>().sprite = sprite;
        player.name = "Player";
    }

    // Instantiate NPC character
    void CreateNPC(Sprite sprite)
    {

        GameObject npc = Instantiate(npcPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        npc.GetComponent<SpriteRenderer>().sprite = sprite;
        npc.name = "NPC_" + sprite.name;
        npc.tag = "NPC";


        // If the prefab doesn’t already have NPCBehavior, add it here
        if (npc.GetComponent<NPCBehavior>() == null)
        {
            npc.AddComponent<NPCBehavior>();
        }

        // Randomize movement parameters per NPC
        NPCBehavior behavior = npc.GetComponent<NPCBehavior>();
        behavior.minSpeed = Random.Range(0.8f, 1.5f);
        behavior.maxSpeed = Random.Range(2.0f, 3.5f);
        behavior.changeDirectionInterval = Random.Range(1.0f, 3.0f);
    }

    // Generate a random spawn point within defined bounds
    Vector2 GetRandomSpawnPoint()
    {
        // Example random spawn range (you can adjust this)
        return new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
    }
}
