using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Net.WebSockets;
using System.Net.Http;
using System.Collections.Concurrent;
using System.Diagnostics;

public class ProcessManager
{
    Task m_ListenThread;
    bool m_IsListening;
    CancellationTokenSource m_CancellationTokenSource;
    ProcessStartInfo psi;
    Process proc;
    public ProcessManager(ProcessStartInfo psi)
    {
        m_CancellationTokenSource = new CancellationTokenSource();

        this.psi = psi;
    }

    public void Start()
    {
        if (m_IsListening) return;

        m_ListenThread = Task.Run(() => ExecuteProcess(m_CancellationTokenSource.Token));

        m_IsListening = true;

        UnityEngine.Debug.Log("Started process thread.");
        Logger.Instance.Log("Started process thread.");
    }

    async Task ExecuteProcess(CancellationToken cancellationToken)
    {
        //UnityEngine.Debug.Log("Executing process...");
        //Logger.Instance.Log("Executing process...");
        
        try 
        {
            proc = Process.Start(psi);
        }
        catch (Exception e)
        {
            Logger.Instance.Log($"Exception executing process: {e.Message}");
        }

        Logger.Instance.Log($"Exit code: {proc.ExitCode}");
    }

    public void Stop(int timeout = 2000)
    {
        if (!m_IsListening) return;

        if (m_ListenThread == null) return; // Not started yet

        if (proc != null)
        {
            proc.Kill();
            proc.WaitForExit(1000);
        }

        m_CancellationTokenSource.Cancel();

        // Wait for queued requests to go out
        try
        {
            m_ListenThread.Wait(timeout);
        }
        catch (AggregateException) { } // Throws when threads are cancelled

        m_IsListening = false;

        UnityEngine.Debug.Log("Stopped process.");
    }
}
