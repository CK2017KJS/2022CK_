using System;
using System.Text; 
using System.Diagnostics;
using System.IO; 
using System.Net.Sockets; 

namespace MyClass
{
    public class ProcSocket
    {
        public Process ThisProc;
        public string Name;
        public DateTime StartTime;
        public int ID;

        public int VirtualMem;

        public int PrivMem;
        public int PhysMem;
        public int Priority;
        public ProcessPriorityClass PriClass;
        public TimeSpan CpuTime;


        public Init()
        {
            this.ThisProc = Process.GetCurrentProcess();

            this.StartTime      = ThisProc.StartTime;
            
            this.VirtualMem     = ThisProc.VirtualMemorySize;
            this.PrivMem        = ThisProc.PrivateMemorySize;
            this.PhysMem        = ThisProc.WorkingSet;

            this.Priority       = ThisProc.BasePriority;
            
            this.PriClass       = ThisProc.PriorityClass;

            this.CpuTime        = ThisProc.TotalProcessorTime;

        }





    }

}