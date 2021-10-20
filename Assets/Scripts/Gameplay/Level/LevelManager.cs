using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] Level[] levels;
    [SerializeField] ObiSolver solver;
    [SerializeField] UnityEvent OnLevelLoaded;


    public Level Current { get; private set; }
    public ObiSolver ObiSolver => solver;

    public void Load()
    {
        Unload();
        Current = Instantiate(levels.GetRandom(), solver.transform);
        OnLevelLoaded?.Invoke();
    }

    public void Unload()
    {
        if (Current == null)
            return;

        Destroy(Current.gameObject);
    }
}
