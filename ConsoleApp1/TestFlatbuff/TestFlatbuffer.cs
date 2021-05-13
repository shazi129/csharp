using FlatBuffers;
using System;
using System.Net;
using System.Net.Sockets;
using Flatbuffers_Proto;

public class TestFlatbuffers
{
    public static void Test_Flatbuffers()
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(1);
        StringOffset msg = builder.CreateString("ddd");
        Message.StartMessage(builder);
        Message.AddSeq(builder, 9527);
        Message.AddMsg(builder, msg);
        var offset = Message.EndMessage(builder);
        builder.Finish(offset.Value);

        byte[] bytes = builder.DataBuffer.ToFullArray();

        Message message = Message.GetRootAsMessage(builder.DataBuffer);
        Console.WriteLine("message: {0}， {1}", message.Msg, message.Seq);
    }

    public static void Test()
    {
//         Test_Flatbuffers();
//         return;

        //设定服务器IP地址  
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _clientSocket.ReceiveTimeout = 0;
        _clientSocket.SendTimeout = 10000;
        _clientSocket.Connect(new IPEndPoint(ipAddress, 9527)); //配置服务器IP与端口

        int seq = 0;

        while(true)
        {
            Console.WriteLine("输入数据:");
            string m = Console.ReadLine();

            string msg = "dddd";

            SendMsg(_clientSocket, msg, seq);

            Message rspMsg = RecvMsg(_clientSocket);
            Console.WriteLine("recv: " + rspMsg.Msg + ", seq:" + rspMsg.Seq);
        }
    }

    private static void SendMsg(Socket socket, string strMsg, int seq)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(1);
        StringOffset msg = builder.CreateString(strMsg);

        Message.StartMessage(builder);
        Message.AddSeq(builder, seq);
        Message.AddMsg(builder, msg);
        var offset = Message.EndMessage(builder);

        builder.Finish(offset.Value);
        byte[] b = builder.DataBuffer.ToFullArray();

        socket.Send(b);
    }

    private static Message RecvMsg(Socket socket)
    {
        byte[] RecvBuffer = new byte[1024];
        int len = socket.Receive(RecvBuffer, 0, 1024, SocketFlags.None);

        //byte[] b = new byte[len];
        
        return Message.GetRootAsMessage(new ByteBuffer(RecvBuffer));
    }
}