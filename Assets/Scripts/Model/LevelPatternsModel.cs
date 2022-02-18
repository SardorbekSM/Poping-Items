﻿using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using VContainer.Unity;

namespace Model
{
    public class LevelPatternsModel
    {
        private readonly LevelPatternsData _levelPatternsData;
        private int _currentLevel = 0;
        private readonly int _levelsCount = 0;

        public List<GameObject> CorrectPatterns { get; private set; }
        public List<GameObject> WrongPatterns { get; private set; }
        public float Frequency { get; private set; }
        public bool UseCorrectsAsWrong { get; private set; }

        public LevelPatternsModel(LevelPatternsData levelPatternsData)
        {
            _levelPatternsData = levelPatternsData;
            
            _levelsCount = levelPatternsData.LevelPatterns.Length;
        }

        public void InitializeNewPatterns()
        {
            if (_currentLevel < _levelsCount)
            {
                CorrectPatterns = new List<GameObject>(_levelPatternsData.LevelPatterns[_currentLevel].CorrectPatterns);
                WrongPatterns = new List<GameObject>(_levelPatternsData.LevelPatterns[_currentLevel].WrongPatterns);
                Frequency = _levelPatternsData.LevelPatterns[_currentLevel].Frequency;
                UseCorrectsAsWrong = _levelPatternsData.LevelPatterns[_currentLevel].UseCorrectsAsWrong;
                
                _currentLevel++;
                
                return;
            } //if nado popravit logiku
            
            ResetToDefault();
        }

        public void ResetToDefault()
        {
            _currentLevel = 0;
            CorrectPatterns.Clear();
            WrongPatterns.Clear();
        }
    }
}