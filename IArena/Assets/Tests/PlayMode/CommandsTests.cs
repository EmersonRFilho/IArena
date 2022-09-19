using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Commands;
using Managers;

public class CommandsTests
{
    private GameObject playerObject;
    private GameObject enemyObject;
    private GameObject treasureObject;
    private GameObject weaponObject;
    private GameObject foodObject;
    private GameObject equipObject;
    private GameObject managerObject;
    private LevelManager levelManager;

    [SetUp]
    public void Setup()
    {
        // player
        // playerObject = GameObject.Instantiate(new GameObject());
        // var behaviours = playerObject.AddComponent<CharacterBehaviors>();
        // playerObject.AddComponent<BaseStats>();
        // playerObject.AddComponent<BoxCollider2D>();
        // playerBrain = playerObject.AddComponent<BrainBase>();
        playerObject = GameObject.Instantiate(Resources.Load<GameObject>("BaseObjects/PlayerBase"), new Vector3(0,0,0), Quaternion.identity);
        enemyObject = GameObject.Instantiate(Resources.Load<GameObject>("BaseObjects/PlayerBase"), new Vector3(1,0,0), Quaternion.identity);
        managerObject = GameObject.Instantiate(new GameObject());
        levelManager = managerObject.AddComponent<LevelManager>();

        //collectables
        treasureObject = GameObject.Instantiate(new GameObject());
        var trecol = treasureObject.AddComponent<BoxCollider2D>();
        treasureObject.AddComponent<Treasure>();
        treasureObject.layer = LayerMask.NameToLayer("Collectable");
        trecol.isTrigger = true;
        
        weaponObject = GameObject.Instantiate(new GameObject(), playerObject.transform.position, Quaternion.identity);
        var wepcol = weaponObject.AddComponent<BoxCollider2D>();
        var stats = weaponObject.AddComponent<Weapon>();
        weaponObject.layer = LayerMask.NameToLayer("Collectable");
        wepcol.isTrigger = true;

        equipObject = GameObject.Instantiate(new GameObject());
        var equipcol = equipObject.AddComponent<BoxCollider2D>();
        equipObject.AddComponent<Equipment>();
        equipObject.layer = LayerMask.NameToLayer("Collectable");
        equipcol.isTrigger = true;

        foodObject = GameObject.Instantiate(new GameObject());
        var foodcol = foodObject.AddComponent<BoxCollider2D>();
        foodObject.AddComponent<Food>();
        foodObject.layer = LayerMask.NameToLayer("Collectable");
        foodcol.isTrigger = true;

    }
    
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CollectsTreasure()
    {
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            treasureObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        // Use yield to skip a frame.
        yield return new WaitForSeconds(0.1f);
        // Use the Assert class to test conditions.
        Assert.AreEqual(false, treasureObject.activeSelf);
        Assert.AreEqual(50, playerObject.GetComponent<CharacterBehaviors>().Score);
        // yield return null;
    }

    [UnityTest]
    public IEnumerator CollectsWeapon()
    {
        weaponObject.transform.position = playerObject.transform.position;
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            weaponObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        // Use yield to skip a frame.
        yield return new WaitForEndOfFrame();
        // Use the Assert class to test conditions.
        Assert.AreEqual(false, weaponObject.activeSelf);
        Assert.NotNull(playerObject.GetComponent<CharacterBehaviors>().Weapon);
        // yield return null;
    }

    [UnityTest]
    public IEnumerator CollectsEquipment()
    {
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            equipObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        // Use yield to skip a frame.
        yield return new WaitForSeconds(0.1f);
        // Use the Assert class to test conditions.
        Assert.AreEqual(false, equipObject.activeSelf);
        Assert.True(playerObject.GetComponent<CharacterBehaviors>().Backpack.Contains(equipObject.GetComponent<Equipment>()));
        // yield return null;
    }

    [UnityTest]
    public IEnumerator CollectsFood()
    {
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            foodObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        // Use yield to skip a frame.
        yield return new WaitForSeconds(0.1f);
        // Use the Assert class to test conditions.
        Assert.AreEqual(false, foodObject.activeSelf);
        Assert.True(playerObject.GetComponent<CharacterBehaviors>().FoodBag.Contains(foodObject.GetComponent<Food>()));
        // yield return null;
    }

    [UnityTest]
    public IEnumerator AttacksPlayer()
    {
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            weaponObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        int maxHealth = enemyObject.GetComponent<CharacterBehaviors>().GetHealth();
        new AttackCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            enemyObject.GetComponent<CharacterBehaviors>(),
            managerObject.GetComponent<LevelManager>()).Execute();
        yield return new WaitForFixedUpdate();
        Assert.Less(enemyObject.GetComponent<CharacterBehaviors>().GetHealth(), maxHealth);
    }

    [UnityTest]
    public IEnumerator SimultaneousAttacks() {
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            weaponObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        int playerMaxHealth = playerObject.GetComponent<CharacterBehaviors>().GetHealth();
        GameObject fists = (GameObject) GameObject.Instantiate(Resources.Load("Weapons/Dem Fists"), enemyObject.transform.position, Quaternion.identity);
        levelManager.QueueCommand(new CollectCommand(
            enemyObject.GetComponent<CharacterBehaviors>(),
            fists.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        int enemyMaxHealth = enemyObject.GetComponent<CharacterBehaviors>().GetHealth();
        levelManager.QueueCommand(new AttackCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            enemyObject.GetComponent<CharacterBehaviors>(),
            managerObject.GetComponent<LevelManager>()));
        levelManager.QueueCommand(new AttackCommand(
            enemyObject.GetComponent<CharacterBehaviors>(),
            playerObject.GetComponent<CharacterBehaviors>(),
            managerObject.GetComponent<LevelManager>()));
        yield return new WaitForFixedUpdate();
        Assert.Less(playerObject.GetComponent<CharacterBehaviors>().GetHealth(), playerMaxHealth);
        Assert.Less(enemyObject.GetComponent<CharacterBehaviors>().GetHealth(), enemyMaxHealth);
    }

    [UnityTest]
    public IEnumerator EatsFood()
    {
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            weaponObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        levelManager.QueueCommand(new AttackCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            playerObject.GetComponent<CharacterBehaviors>(),
            managerObject.GetComponent<LevelManager>()));
        yield return new WaitForFixedUpdate();
        int playerHealth = playerObject.GetComponent<CharacterBehaviors>().GetHealth();
        levelManager.QueueCommand(new CollectCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            foodObject.GetComponent<Collectable>(),
            managerObject.GetComponent<LevelManager>()));
        yield return new WaitForFixedUpdate();
        levelManager.QueueCommand(new HealCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            foodObject.GetComponent<Food>(),
            managerObject.GetComponent<LevelManager>()));
        // Use yield to skip a frame.
        yield return new WaitForSeconds(0.1f);
        // Use the Assert class to test conditions.
        Assert.Greater(playerObject.GetComponent<CharacterBehaviors>().GetHealth(), playerHealth);
        Assert.AreEqual(false, foodObject.activeSelf);
        // yield return null;
    }
}
