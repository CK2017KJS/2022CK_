using System;
using System.Threading;

namespace ThreadControl
{

    public class UsedJoin
    //05. Join 
    {
        const int RunningTime = 10;
        //Program의 동작 시간
        public void Run()
        {
            Thread JoinThread = new Thread(ThreadFunc);
            JoinThread.IsBackground = true;

            JoinThread.Start(); //Started JoinThread.

            JoinThread.Join();  
            /*
                AThread.Join();  Keyword is 
                "Until 'AThread' has done all his work. wait on
                the all Every thread, including main thread." 

                AThread.Join();은
                "AThread 의 모든 작업이 종료될 때 까지, 메인
                스레드를 포함한 모든 스레드가 대기한다는 뜻입니다."

            */

            Console.WriteLine("JoinThread is End.");
            Console.WriteLine("Join 스레드 종료.");
        }

        private void ThreadFunc()
        {
            for(int i =0 ; i < RunningTime; i++)
            {
                Console.WriteLine(" {0} second left until the End " , RunningTime - i);
                Console.WriteLine(" 종료까지 {0} 초 남았습니다. " , RunningTime - i);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Thread End.");

        }

    }



    public class ThreadDatas
    {
        public int Value1;
        public int Value2;

    }
    public class ThreadArgsControl
    {    

        public void Run()
        {
            Thread t = new Thread(ThreadFunc);
            ThreadDatas param = new ThreadDatas();

            param.Value1=10;
            param.Value2=20;

            t.Start(param);
        }

        private void ThreadFunc(Object initalvaule)
        {
            ThreadDatas Value = (ThreadDatas)initalvaule;
            //Type Cast  Object Convert To ThreadDatas Type
            //Object를 Thread형으로 형변환 합니다.

            Console.WriteLine("Get Object Type.({0},{1})",Value.Value1,Value.Value2);
            

        }


    }



}
