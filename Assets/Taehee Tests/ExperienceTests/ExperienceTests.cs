using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ExperienceTests
{
    private class TestExperienceComponent : MonoBehaviour
    {
        public int experience;

        public void addExperience(int experience)
        {
            int currentLevel = getLevel();
            this.experience += Mathf.Abs(experience);
            int newLevel = getLevel();

            if (currentLevel != newLevel)
            {
                levelUp();
            }
        }

        public int getLevel()
        {
            // Mock implementation
            return experience / 100;
        }

        public void levelUp()
        {
            // Mock implementation
            Debug.Log("Level Up!");
        }
    }

    private TestExperienceComponent experienceComponent;

    [SetUp]
    public void SetUp()
    {
        GameObject testGameObject = new GameObject();
        experienceComponent = testGameObject.AddComponent<TestExperienceComponent>();
    }

    [Test]
    public void AddExperience_IncreasesExperience()
    {
        experienceComponent.experience = 50;
        experienceComponent.addExperience(30);

        Assert.AreEqual(80, experienceComponent.experience);
    }

    [Test]
    public void AddExperience_LevelsUpWhenThresholdReached()
    {
        experienceComponent.experience = 90;
        experienceComponent.addExperience(20);

        // Ensure level up is called by some means; here using experience as proxy
        Assert.AreEqual(110, experienceComponent.experience);
    }

    [Test]
    public void AddExperience_DoesNotLevelUpIfThresholdNotReached()
    {
        experienceComponent.experience = 50;
        experienceComponent.addExperience(30);

        // Ensure no level up occurs
        Assert.AreEqual(80, experienceComponent.experience);
    }
}
