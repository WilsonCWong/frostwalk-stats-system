﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frostwalk.StatSystem
{
    [Serializable]
    public class Stat
    {
        float currentPoints;
        float currentExp;

        [SerializeField] String name;

        [SerializeField] LevelingType levelingType;
        [SerializeField] PointsAccuracy accuracy;
        [SerializeField] AnimationCurve expCurve;


        [SerializeField] float maxPoints;
        [SerializeField] float maxExp;

        [SerializeField] public float StartingPoints { get; set; }

        public String Name { get { return name; } set { name = value; } } 
        public float CurrentPoints { get { return WithAccuracy(currentPoints); } private set { currentPoints = WithAccuracy(value); } }
        public float CurrentExp {
            get { return WithAccuracy(currentExp); }
            private set {
                if (currentExp + value >= maxExp)
                    currentExp = WithAccuracy(maxExp);
                else
                    currentExp = WithAccuracy(value);
            }
        }
        public float ExpToNext { get; private set; }
        public float MaxPoints { get { return WithAccuracy(maxPoints); } private set { maxPoints = WithAccuracy(value); } }

        public StatsGameEvent OnAddExp;
        public StatsGameEvent OnAddSkillPoints;
        public StatsGameEvent OnLevelUp;

        public Stat()
        {
            CurrentPoints = WithAccuracy(StartingPoints);
        }

        public Stat(float points, LevelingType levelType)
        {
            levelingType = levelType;
            CurrentPoints = WithAccuracy(points);
        }

        void UpdateExpToNextLevel()
        {
            ExpToNext = WithAccuracy(expCurve.Evaluate((CurrentPoints + 1) / maxPoints) * maxExp - CurrentExp);
        }

        float WithAccuracy(float f)
        {
            if (accuracy == PointsAccuracy.INT)
            {
                return Mathf.RoundToInt(f);
            }

            return f;
        }

        public void AddExp(float exp)
        {
            if (levelingType == LevelingType.EXP)
            {
                CurrentExp += WithAccuracy(exp);

                float newPoints = CurrentPoints;
                for (float i = CurrentPoints; i <= maxPoints; i++)
                {
                    if (CurrentExp >= WithAccuracy(expCurve.Evaluate((i) / maxPoints) * maxExp))
                        newPoints = i;
                    else break;
                }

                float diff = newPoints - CurrentPoints;
                if (diff != 0)
                {
                    CurrentPoints = newPoints;
                    if (OnLevelUp != null) OnLevelUp.Raise(this, diff);
                }

                UpdateExpToNextLevel();
                if (OnAddExp != null) OnAddExp.Raise(this, WithAccuracy(exp));
            }
        }

        public void AddSkillPoints(float p)
        {
            CurrentPoints += WithAccuracy(p);
            if (OnAddSkillPoints != null) OnAddSkillPoints.Raise(this, p);
        }

        public void RemoveSkillPoints(float p)
        {
            CurrentPoints -= WithAccuracy(p);
        }

        public void SetLevelingType(LevelingType lt)
        {
            levelingType = lt;
        }

        public void Reset()
        {
            CurrentPoints = WithAccuracy(StartingPoints);
            CurrentExp = 0;
        }
        
    }

    public enum PointsAccuracy
    {
        INT, FLOAT
    }

    public enum LevelingType
    {
        POINTS, EXP
    }

}