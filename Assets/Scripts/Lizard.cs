using UnityEngine;
using System.Collections;

[System.Serializable]
public class Lizard{

    //Static reference to the current lizard
    public static Lizard current;

    //MaxRHP = 100 + (5 * fitPoints)
    public int myMaxRHP, myRHPRemaining, specPoints, regenBugs;
    //Add one for each spec added in
    public int learnerPoints, gathererPoints, durablePoints, fitPoints;
    //Add 0.25 to 1 for each point in learner (1 + (0.25 * learnerPoints))
    public float losingXPModifier;
    //10% less RHP cost for every point in durable (1 - (durablePoints * 0.1))
    public float actionCostModifier;
    public int experiencePoints, level;

    public enum SpecTypes { learner, gatherer, durable, fit }

    /// <summary>
    /// Constructor for when a new lizard is created
    /// </summary>
    public Lizard()
    {
        //Every lizard starts off with 100 RHP
        myMaxRHP = 100;
        //Lizard has just been created so they can not have lost any RHP
        myRHPRemaining = myMaxRHP;
        //Every lizard gets 3 spec points to start with
        specPoints = 3;
        //Users start with 3 regen bugs
        regenBugs = 3;
        //There are no spec points yet because they haven't been specced
        learnerPoints = 0;
        gathererPoints = 0;
        durablePoints = 0;
        fitPoints = 0;
        //The modifiers remain as one until the spec changes them
        losingXPModifier = 1;
        actionCostModifier = 1;
        //The player starts at level one with 0 experience points
        experiencePoints = 0;
        level = 1;
    }

    /// <summary>
    /// Call when investing a spec point. If returns false have pop up informing user they don't have any more spec points
    /// </summary>
    /// <param name="spec">The spec type that the user is investing in</param>
    /// <returns></returns>
    public bool Spec(SpecTypes spec)
    {
        if (specPoints > 0)
        {
            specPoints--;
            switch (spec)
            {
                case SpecTypes.durable:
                    AddDurablePoint();
                    break;
                case SpecTypes.fit:
                    AddFitPoint();
                    break;
                case SpecTypes.gatherer:
                    AddGathererPoint();
                    break;
                case SpecTypes.learner:
                    AddLearnerPoint();
                    break;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Adds a learner point and adjusts the losing XP modifier
    /// </summary>
    void AddLearnerPoint()
    {
        learnerPoints++;
        losingXPModifier += 0.25f;
    }

    /// <summary>
    /// Adds a durable point and adjusts the action cost modifier
    /// </summary>
    void AddDurablePoint()
    {
        durablePoints++;
        actionCostModifier *= 0.9f;
    }

    /// <summary>
    /// Adds a gatherer point and adjusts the number of regen bugs the player has
    /// </summary>
    void AddGathererPoint()
    {
        gathererPoints++;
        regenBugs += 3;
    }

    /// <summary>
    /// Adds a fit point and adjusts the users max RHP and tops up the lizards remaining RHP
    /// </summary>
    void AddFitPoint()
    {
        fitPoints++;
        myMaxRHP += 5;
        myRHPRemaining = myMaxRHP;
    }

    public void TakeAwayRHP(int val)
    {
        myRHPRemaining -= val;
        SaveLoad.Save();
    }

    public bool UseRegenBug()
    {
        if (regenBugs > 0)
        {
            if (myRHPRemaining < myMaxRHP)
            {
                regenBugs--;
                myRHPRemaining = myMaxRHP;
                SaveLoad.Save();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
