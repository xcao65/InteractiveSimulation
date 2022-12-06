using UnityEngine;

// navmesh moved to UnityEngine.AI in 2017
#if !UNITY_5
using UnityEngine.AI;
#endif

using System.Diagnostics;


public class NPC : MonoBehaviour
{
    NavMeshAgent agent;
    ProcessManager pm;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        var psi = new ProcessStartInfo();
        psi.FileName = $@"{Application.dataPath}/npc_brain/npc_brain";

        if (Application.platform == RuntimePlatform.WindowsPlayer)
            psi.FileName += ".exe";

        UnityEngine.Debug.Log($"File path to executable: {psi.FileName}");
        Logger.Instance.Log($"File path to executable: {psi.FileName}");

        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;

        pm = new ProcessManager(psi);

        pm.Start();
    }

    public void MoveTo( Vector3 pos )
    {
        agent.destination = pos;
    }

    void OnApplicationQuit()
    {
        //ScenarioPlayer.Instance.StopListening();
        pm.Stop();
    }
}
