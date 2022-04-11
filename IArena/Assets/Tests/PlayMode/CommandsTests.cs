using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Commands;

public class CommandsTests
{
    private GameObject playerObject;
    private GameObject treasureObject;
    private GameObject weaponObject;
    private GameObject foodObject;
    private GameObject equipObject;

    [SetUp]
    public void Setup()
    {
        // player
        // playerObject = GameObject.Instantiate(new GameObject());
        // var behaviours = playerObject.AddComponent<CharacterBehaviors>();
        // playerObject.AddComponent<BaseStats>();
        // playerObject.AddComponent<BoxCollider2D>();
        // playerBrain = playerObject.AddComponent<BrainBase>();
        playerObject = GameObject.Instantiate(Resources.Load<GameObject>("BaseObjects/PlayerBase"));

        //collectables
        treasureObject = GameObject.Instantiate(new GameObject());
        var trecol = treasureObject.AddComponent<BoxCollider2D>();
        treasureObject.AddComponent<Treasure>();
        treasureObject.layer = LayerMask.NameToLayer("Collectable");
        trecol.isTrigger = true;
        
        weaponObject = GameObject.Instantiate(new GameObject());
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
        new CollectCommand(playerObject.GetComponent<CharacterBehaviors>(), treasureObject.GetComponent<Collectable>()).Execute();
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
        new CollectCommand(playerObject.GetComponent<CharacterBehaviors>(), weaponObject.GetComponent<Collectable>()).Execute();
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
        new CollectCommand(playerObject.GetComponent<CharacterBehaviors>(), equipObject.GetComponent<Collectable>()).Execute();
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
        new CollectCommand(playerObject.GetComponent<CharacterBehaviors>(), foodObject.GetComponent<Collectable>()).Execute();
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
        new CollectCommand(playerObject.GetComponent<CharacterBehaviors>(), weaponObject.GetComponent<Collectable>()).Execute();
        int maxHealth = playerObject.GetComponent<CharacterBehaviors>().GetHealth();
        new AttackCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            playerObject.GetComponent<CharacterBehaviors>()).Execute();
        yield return new WaitForFixedUpdate();
        Assert.Less(playerObject.GetComponent<CharacterBehaviors>().GetHealth(), maxHealth);
    }

    [UnityTest]
    public IEnumerator EatsFood()
    {
        new CollectCommand(playerObject.GetComponent<CharacterBehaviors>(), weaponObject.GetComponent<Collectable>()).Execute();
        new AttackCommand(
            playerObject.GetComponent<CharacterBehaviors>(),
            playerObject.GetComponent<CharacterBehaviors>()).Execute();
        yield return new WaitForFixedUpdate();
        int playerHealth = playerObject.GetComponent<CharacterBehaviors>().GetHealth();
        new CollectCommand(playerObject.GetComponent<CharacterBehaviors>(), foodObject.GetComponent<Collectable>()).Execute();
        yield return new WaitForFixedUpdate();
        new HealCommand(playerObject.GetComponent<CharacterBehaviors>(), foodObject.GetComponent<Food>()).Execute();
        // Use yield to skip a frame.
        yield return new WaitForSeconds(0.1f);
        // Use the Assert class to test conditions.
        Assert.Greater(playerObject.GetComponent<CharacterBehaviors>().GetHealth(), playerHealth);
        Assert.AreEqual(false, foodObject.activeSelf);
        // yield return null;
    }
}
