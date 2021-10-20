using Obi;
using UnityEngine;

public class RopeCollisionData : MonoBehaviour
{
    private ObiSolver solver;

    private float[] _traction;
    private Vector3[] _surfaceNormals;

    private bool _initialized;

    public void GetData(out float[] traction, out Vector3[] surfaceNormal)
    {
        traction = _traction;
        surfaceNormal = _surfaceNormals;
    }

    public void Init(ObiRope rope)
    {
        //_traction = new float[rope.particleCount];
        _surfaceNormals = new Vector3[rope.particleCount];
        solver = LevelManager.Instance.ObiSolver;
        solver.OnBeginStep += ResetSurfaceInfo;
        solver.OnCollision += AnalyzeContacts;
        solver.OnParticleCollision += AnalyzeContacts;
        _initialized = true;

    }

    private void OnDestroy()
    {
        if (!_initialized)
            return;

        solver.OnBeginStep -= ResetSurfaceInfo;
        solver.OnCollision -= AnalyzeContacts;
        solver.OnParticleCollision -= AnalyzeContacts;
    }


    private void ResetSurfaceInfo(ObiSolver s, float stepTime)
    {
        for (int i = 0; i < _traction.Length; ++i)
        {
            _traction[i] = 0;
            _surfaceNormals[i] = Vector3.zero;
        }
    }

    private void AnalyzeContacts(object sender, ObiSolver.ObiCollisionEventArgs e)
    {
        for (int i = 0; i < e.contacts.Count; ++i)
        {
            var contact = e.contacts.Data[i];
            if (contact.distance < 0.005f)
            {
                int index = solver.simplices[contact.bodyA];
                int particleIndex = solver.particleToActor[index].indexInActor;

                _traction[particleIndex] = 1;
                _surfaceNormals[particleIndex] += (Vector3)contact.normal;
            }
        }
    }

}
