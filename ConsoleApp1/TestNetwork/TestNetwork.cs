using System;
using System.Collections.Generic;
using System.Threading;

public class TestNetwork
{
    public static void Test()
    {
        Connector conn = new Connector();
        conn.Connect();
        Thread.Sleep(1000);
        conn.StartWork();
//         List<Connector> conns = new List<Connector>();
//         for (int i = 0; i < 7; i++)
//         {
//             Connector connector = new Connector();
//             connector.uin = 1000 + i;
//             connector.Connect();
// 
//             conns.Add(connector);
//         }
// 
//         Thread.Sleep(2000);
//         for (int i = 0; i < conns.Count; i++)
//         {
//             conns[i].StartWork();
//         }


        while(true)
        {
            Thread.Sleep(1000);
        }
    }
}