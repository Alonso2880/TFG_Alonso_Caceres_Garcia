using UnityEngine;

public interface ISimulationTickable
{
    void SimulationTick(float simulatedDeltaTime);
}
