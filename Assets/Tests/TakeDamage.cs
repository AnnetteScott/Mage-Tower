using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TakeDamage
{
    [Test]
    public void NegativeDamage()
    {
        Player player = new Player();
        Assert.IsTrue(player.takeDamage(-10) == 1);
    }

    [Test]
    public void ToMuchDamage()
    {
        Player player = new Player();
        Assert.IsTrue(player.takeDamage(20) <= 0);
    }

    [Test]
    public void TakeNormalDamage()
    {
        Player player = new Player();
        Assert.IsTrue(player.takeDamage(5) == 6);
    }
}

internal class Player
{
    public float armour;
    public float health;
    public Player()
    {
        armour = 1;
        health = 10;
    }

    public float takeDamage(float damage)
    {
        float damageDone = Mathf.Abs(damage) - armour;
        if (damageDone > 0)
        {
            health -= Mathf.Abs(damageDone);

        }

        if (this.health <= 0)
        {
            //died();
        }

        return this.health;
    }
}