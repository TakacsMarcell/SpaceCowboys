using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class LevelTests
{
    private GameObject levelGameObject;
    private GameObject playerShipGameObject;
    private Level level;
    private Ship playerShip;
    private Text scoreText;

    [SetUp]
    public void SetUp()
    {
        levelGameObject = new GameObject();
        level = levelGameObject.AddComponent<Level>();

        playerShipGameObject = new GameObject();
        playerShip = playerShipGameObject.AddComponent<Ship>();

        level.GetType().GetField("playerShip", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(level, playerShip);

        GameObject scoreTextObject = new GameObject();
        scoreText = scoreTextObject.AddComponent<Text>();
        scoreText.text = "0";

        level.GetType().GetField("scoreText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(level, scoreText);

        level.AddScore(0);  
    }

    [TearDown]
    public void TearDown()
    {
       
        Object.DestroyImmediate(levelGameObject);
        Object.DestroyImmediate(playerShipGameObject);
    }

   
    [Test]
    public void AddScore_UpdatesScore()
    {
        
        level.AddScore(10);

       
        Assert.AreEqual("10", scoreText.text, "The score should be updated correctly.");
    }


    [Test]
    public void AddAndRemoveDestructables_ChangesNextLevelFlag()
    {
        
        level.AddDestructable();
        level.RemoveDestructable();  

        
        Assert.IsTrue((bool)level.GetType().GetField("startNextLevel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(level), "The flag for starting the next level should be set to true.");
    }

    [Test]
    public void RemoveDestructable_TriggersLevelTransition()
    {
        
        level.AddDestructable();
        level.RemoveDestructable(); 

       
        Assert.IsTrue((bool)level.GetType().GetField("startNextLevel", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(level), "The flag for starting the next level should be set to true after removing destructables.");
    }
}
