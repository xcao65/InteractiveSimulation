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
        psi.FileName = $@"{Application.dataPath}/Scripts/NPCBrain/.venv/";

        if (Application.platform == RuntimePlatform.WindowsPlayer)
            psi.FileName += "Scripts/python.exe";
        else
            psi.FileName += "bin/python3.7";

        UnityEngine.Debug.Log($"File path to python executable: {psi.FileName}");

        var script = $@"{Application.dataPath}/Scripts/NPCBrain/npc_brain.py";

        psi.Arguments = $"{script}"; // need quotes?
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
