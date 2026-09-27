using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


public class SceneLoadTests
{
    [UnityTest]
    public IEnumerator SimulationScene_LoadsWithoutErrors()
    {
        yield return SceneManager.LoadSceneAsync("Simulation");
        Assert.IsTrue(SceneManager.GetActiveScene().isLoaded);
    }
}
