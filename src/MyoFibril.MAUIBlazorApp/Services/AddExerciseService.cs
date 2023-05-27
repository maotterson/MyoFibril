﻿using MyoFibril.Contracts.WebAPI.Models;
using MyoFibril.Domain.Entities;

namespace MyoFibril.MAUIBlazorApp.Services;
public class AddExerciseService : IAddExerciseService
{
    private bool _modalOpen = false;
    public event Action OnModalStateChanged;

    private List<ExerciseEntity> _exercises;
    private ExerciseEntity _selectedExercise = new ExerciseEntity();
    public ExerciseEntity SelectedExercise 
    {
        get => _selectedExercise;
        set
        {
            _selectedExercise = value;
            OnModalStateChanged.Invoke();
        }
    }
    public void OpenModal()
    {
        _modalOpen = true;
        OnModalStateChanged.Invoke();
    }
    public void CloseModal()
    {
        _modalOpen = false;
        OnModalStateChanged.Invoke();
    }
    public bool IsModalOpen()
    {
        return _modalOpen;
    }

    public List<ExerciseEntity> GetExercisesList()
    {
        return _exercises;
    }

    public void AddExerciseToList(ExerciseEntity exercise)
    {
        _exercises.Add(exercise);
        OnModalStateChanged.Invoke();
    }
    
}