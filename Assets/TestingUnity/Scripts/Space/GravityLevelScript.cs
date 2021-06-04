using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CelBody
{
    public CelestrialBodyScript2 celestrialBody;
    private Vector3 _velocity;
    public Vector3 velocity
    {
        get { return this._velocity; }
        set
        {
            this._velocity = value;
        }
    }
    private Vector3 _position;
    public Vector3 position
    {
        get { return this._position; }
        set
        {
            this._position = value;
        }
    }
    public float mass
    {
        get { return this.celestrialBody.mass; }
        set
        {
            this.celestrialBody.mass = value;
        }
    }
    public Vector3 initVelocity
    {
        get { return this.celestrialBody.initVelocity; }
    }
}

public class OrbitalState
{
    private CelBody[] celBodies;

    public CelBody[] celestBodies
    {
        get { return this.celBodies; }
    }

    // OrbitalState(CelestrialBodyScript2[] celestrialBodies)
    // {
    //     celBodies = new CelBodies[celestrialBodies.Length];
    // }
    public OrbitalState(CelBody[] oldCelBodies)
    {
        this.celBodies = new CelBody[oldCelBodies.Length];
        for (int i = 0; i < oldCelBodies.Length; i++)
        {
            this.celBodies[i] = oldCelBodies[i];
        }
    }

    // public void InitOrbitalState(CelBody[] oldCelBodies)
    // {
    //     this.celBodies = new CelBody[oldCelBodies.Length];
    //     for (int i = 0; i < oldCelBodies.Length; i++)
    //     {
    //         this.celBodies[i] = oldCelBodies[i];
    //     }
    // }

    public void NextState(float gravityConstant, float timeStep)
    {
        // Calculate accelerations:
        for (int i = 0; i < this.celBodies.Length; i++)
        {
            foreach (var otherBody in this.celBodies)
            {
                if (this.celBodies[i].celestrialBody != otherBody.celestrialBody)
                {
                    Vector3 gravityAcceleration = CalculateGravity(this.celBodies[i].position, otherBody.position, otherBody.mass, gravityConstant);
                    this.celBodies[i].velocity += gravityAcceleration * timeStep;
                }
            }
        }

        // Change positions
        for (int i = 0; i < this.celBodies.Length; i++)
        {
            this.celBodies[i].position += this.celBodies[i].velocity * timeStep;
        }
    }

    public static Vector3 CalculateGravity(Vector3 thisPos, Vector3 otherPos, float otherMass, float gravityConstant)
    {
        Vector3 vec = otherPos - thisPos;
        float radius = vec.magnitude;
        Vector3 force = gravityConstant * (otherMass / (radius * radius)) * vec.normalized;
        return force;
    }
}

public class GravityLevelScript : MonoBehaviour
{
    [SerializeField]
    private int nr_of_states = 10000;
    [SerializeField]
    private float gravityConstant = 1f;
    [SerializeField]
    private float timeStep = 0.1f;

    private CelestrialBodyScript2[] celestrialBodies;
    private OrbitalState[] orbitalStates;

    // [SerializeField]
    // private Vector3[] initVelocities = null;
    // [SerializeField]
    // private bool[] drawOrbit = null;
    [SerializeField]
    private Vector3 pos = Vector3.zero;

    private float _currentTime = 0f;
    public float currentTime
    {
        get { return this._currentTime; }
    }
    private float _maxTime = 0f;
    public float maxTime
    {
        get { return this._maxTime; }
    }
    void OnValidate()
    {
        this.celestrialBodies = GameObject.FindObjectsOfType<CelestrialBodyScript2>();
        Debug.Log("Gravity Newage!");

        pos = this.transform.position;

        // if (this.initVelocities == null)
        // {
        //     this.initVelocities = new Vector3[this.celestrialBodies.Length];
        //     for (int i = 0; i < this.initVelocities.Length; i++)
        //     {
        //         this.initVelocities[i] = this.celestrialBodies[i].initVelocity;
        //     }
        // }
        // else
        // {
        //     for (int i = 0; i < this.initVelocities.Length; i++)
        //     {
        //         this.celestrialBodies[i].initVelocity = this.initVelocities[i];
        //     }
        // }

        // if (this.drawOrbit == null)
        // {
        //     this.drawOrbit = new bool[this.celestrialBodies.Length];
        //     for (int i = 0; i < this.drawOrbit.Length; i++)
        //     {
        //         this.drawOrbit[i] = this.celestrialBodies[i].drawOrbit;
        //     }
        // }
        // else
        // {
        //     for (int i = 0; i < this.drawOrbit.Length; i++)
        //     {
        //         this.celestrialBodies[i].drawOrbit = this.drawOrbit[i];
        //     }
        // }

        this._maxTime = (float)this.nr_of_states * this.timeStep;
        this.FirstTimeCalculateOrbits();
        // if (Application.IsPlaying(this))
        // {
        //     Debug.Log(this.orbitalStates);
        //     this.RecalculateOrbits();
        // }
        // else
        // {
        //     this.FirstTimeCalculateOrbits();
        // }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.celestrialBodies = GameObject.FindObjectsOfType<CelestrialBodyScript2>();
        this._maxTime = (float)this.nr_of_states * this.timeStep;
        this.FirstTimeCalculateOrbits();

        // foreach (var state in this.orbitalStates)
        // for (int i = 1; i < this.orbitalStates.Length; i++)
        // {
        //     Debug.DrawLine(this.orbitalStates[i-1].celestBodies[1].position, this.orbitalStates[i].celestBodies[1].position, Color.red, 20f);
        //     // Debug.DrawLine(this.orbitalStates[i-1].celestBodies[0].position, this.orbitalStates[i].celestBodies[0].position, Color.blue, 20f);
        //     // Debug.DrawRay(state.celestBodies[1].position, state.celestBodies[1].velocity, Color.red, 20f);
        // }
        // Debug.DrawRay(this.orbitalStates[950].celestBodies[1].position, this.orbitalStates[950].celestBodies[1].velocity, Color.red, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.currentTime + (20f * this.timeStep) > this.maxTime)
        {
            RecalculateOrbits();
        }

        OrbitalState orbState = GetState(this.currentTime);
        foreach (var body in orbState.celestBodies)
        {
            body.celestrialBody.transform.position = body.position;
        }
        this._currentTime += Time.deltaTime;
    }

    public void FirstTimeCalculateOrbits()
    {
        OrbitalState[] orbStates = new OrbitalState[this.nr_of_states];

        // First orbState:
        CelBody[] firstCelBodies = new CelBody[celestrialBodies.Length];
        for (int i = 0; i < firstCelBodies.Length; i++)
        {
            firstCelBodies[i].celestrialBody = celestrialBodies[i];
            firstCelBodies[i].velocity = celestrialBodies[i].initVelocity;
            firstCelBodies[i].mass = celestrialBodies[i].mass;
            firstCelBodies[i].position = celestrialBodies[i].transform.position;
        }
        orbStates[0] = new OrbitalState(firstCelBodies);
        // orbStates[0].InitOrbitalState(firstCelBodies);

        // for (int i = 1; i < orbStates.Length; i++)
        // {
        //     // orbStates[i].InitOrbitalState(orbStates[i-1].celestBodies);
        //     orbStates[i] = new OrbitalState(orbStates[i-1].celestBodies);
        //     orbStates[i].NextState(this.gravityConstant, this.timeStep);
        // }

        this.CalculateRest(orbStates, 1);

        this.orbitalStates = orbStates;
        this._currentTime = 0f;
    }

    private void RecalculateOrbits()
    {
        OrbitalState[] orbStates = new OrbitalState[this.nr_of_states];

        int currentIndex = this.TimeToIndex(this.currentTime);
        int i = 0;

        for (; currentIndex < this.orbitalStates.Length; i++, currentIndex++)
        {
            orbStates[i] = this.orbitalStates[currentIndex];
        }

        CalculateRest(orbStates, i);

        this.orbitalStates = orbStates;
        this._currentTime = 0f;
    }

    private void CalculateRest(OrbitalState[] orbStates, int fromIndex)
    {
        for (int i = fromIndex; i < orbStates.Length; i++)
        {
            orbStates[i] = new OrbitalState(orbStates[i-1].celestBodies);
            orbStates[i].NextState(this.gravityConstant, this.timeStep);
        }
    }

    public int TimeToIndex(float time)
    {
        return Mathf.Clamp((int)Mathf.Round(time/timeStep), 0, this.orbitalStates.Length-1);
    }

    public OrbitalState GetState(float time)
    {
        int index = this.TimeToIndex(time);
        return this.orbitalStates[index];
    }

    public int GetNrOfStates()
    {
        return this.nr_of_states;
    }

    public float GetTimeStep()
    {
        return this.timeStep;
    }

    public int[] GetDrawIndices()
    {
        if (this.celestrialBodies != null)
        {
            List<int> indices = new List<int>(this.celestrialBodies.Length);
            for (int i = 0; i < this.celestrialBodies.Length; i++)
            {
                if (this.celestrialBodies[i].drawOrbit)
                {
                    indices.Add(i);
                }
            }
            return indices.ToArray();
        }
        return new int[0];
    }
}
