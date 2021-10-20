using UnityEngine;

public class ColorGroup : Colored
{

    [SerializeField] private Colored[] members;

    [SerializeField] private bool randomYRotation = true;


    protected override void Awake()
    {
        if (members == null || members.Length == 0)        
            members = GetComponentsInChildren<Colored>();
        
        base.Awake();

        if (randomYRotation)        
            transform.rotation *= Quaternion.Euler(0, Random.Range(-180f, 180f), 0);        
    }

    public override int ColorId
    {
        get => base.ColorId;
        set
        {
            base.ColorId = value;
            foreach (Colored color in members)            
                color.ColorId = value;
            
        }
    }
}
