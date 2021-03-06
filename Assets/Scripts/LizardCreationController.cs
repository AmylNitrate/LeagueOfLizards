﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LizardCreationController : MonoBehaviour {

    [SerializeField]
    GameObject outOfPointsPanel;

    [SerializeField]
    Text specPointsValue, gathererPoints, learnerPoints, fitPoints, durablePoints, levelText, XPText;

	void Awake()
    {
        SaveLoad.Load();
        DisplayInfo();
    }

    public void DisplayInfo()
    {
        Debug.Log("Lizard rhp = " + Lizard.current.myMaxRHP);
        specPointsValue.text = Lizard.current.specPoints.ToString();
        gathererPoints.text = Lizard.current.gathererPoints.ToString();
        learnerPoints.text = Lizard.current.learnerPoints.ToString();
        fitPoints.text = Lizard.current.fitPoints.ToString();
        durablePoints.text = Lizard.current.durablePoints.ToString();
        levelText.text = Lizard.current.level.ToString();
        XPText.text = Lizard.current.experiencePoints.ToString() + "/" + Lizard.current.experienceGoal.ToString();
    }

    /// <summary>
    /// Call on gatherer button
    /// </summary>
    public void SpecGatherer()
    {
        if (Lizard.current.Spec(Lizard.SpecTypes.gatherer))
        {
            specPointsValue.text = Lizard.current.specPoints.ToString();
            gathererPoints.text = Lizard.current.gathererPoints.ToString();
            //Play spec worked sound
            SaveLoad.Save();
        }
        else
        {
            outOfPointsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Call on Learner button
    /// </summary>
    public void SpecLearner()
    {
        if (Lizard.current.Spec(Lizard.SpecTypes.learner))
        {
            specPointsValue.text = Lizard.current.specPoints.ToString();
            learnerPoints.text = Lizard.current.learnerPoints.ToString();
            SaveLoad.Save();
            //Play spec worked sound
        }
        else
        {
            outOfPointsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Call on gatherer button
    /// </summary>
    public void SpecFit()
    {
        if (Lizard.current.Spec(Lizard.SpecTypes.fit))
        {
            specPointsValue.text = Lizard.current.specPoints.ToString();
            fitPoints.text = Lizard.current.fitPoints.ToString();
            SaveLoad.Save();
            //Play spec worked sound
        }
        else
        {
            outOfPointsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Call on gatherer button
    /// </summary>
    public void SpecDurable()
    {
        if (Lizard.current.Spec(Lizard.SpecTypes.durable))
        {
            specPointsValue.text = Lizard.current.specPoints.ToString();
            durablePoints.text = Lizard.current.durablePoints.ToString();
            SaveLoad.Save();
            //Play spec worked sound
        }
        else
        {
            outOfPointsPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Deletes the save file and creates a new lizard to save
    /// </summary>
    public void DeleteLizard()
    {
        SaveLoad.DeleteSave();
        SaveLoad.Load(); //This creates a new lizard if a file is not found
    }
}
