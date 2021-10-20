using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System.Collections;

[GlobalConfig("Assets/Resources/Data")]
public partial class Colors : GlobalConfig<Colors>
{
    [SerializeField] private Data[] values;

    public Color this[in int id] => values[id].color;

    public Color GetColor(in int id) => this[id];

    public int RandomId => Random.Range(0, values.Length);


    [System.Serializable]
    public struct Data
    {
#if UNITY_EDITOR
        public string name;
#endif
        public Color color;
    }
}
