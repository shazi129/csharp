using LitJsonEx;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Connector
{
    protected Socket _clientSocket = null;
    public Socket ClientSocket { get { return _clientSocket; } }

    protected Thread _connectThread = null;
    protected Thread _workThread = null;

    protected string _ip = "127.0.0.1";
    protected int _port = 9527;
    protected int _connectRetryTimes = 1;

    protected bool _connectAsync = false;

    public long uin = 0;

    public bool IsConnected()
    {
        return _clientSocket != null && _clientSocket.Connected;
    }

    public virtual void Connect()
    {
        if (_connectAsync)
        {

            if (_connectThread != null)
            {
                _connectThread.Interrupt();
            }
            _connectThread = new Thread(DoConnect);
            _connectThread.Start();
        }
        else
        {
            DoConnect();
        }
    }

    public virtual void DoConnect()
    {
        try
        {
            //设定服务器IP地址  
            IPAddress ipAddress = IPAddress.Parse(_ip);
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.ReceiveTimeout = 0;
            _clientSocket.SendTimeout = 10000;

            for (int i = 0; i < _connectRetryTimes; i++)
            {
                try
                {
                    _clientSocket.Connect(new IPEndPoint(ipAddress, _port)); //配置服务器IP与端口 
                }
                catch (SocketException soex)
                {
                    Console.WriteLine("Connect to {0}:{1} {2}th time error: {3}", _ip, _port, i + 1, soex.Message);
                }

                if (_clientSocket.Connected)
                {
                    Console.WriteLine("Connect to {0}:{1} {2}th time success", _ip, _port, i + 1);
                    break;
                }
            }
        }
        catch (ThreadInterruptedException Ex)
        {
            Close();
            Console.WriteLine("Connect error:" + Ex);
        }
    }

    //开始收发数据
    public virtual void StartWork()
    {
        if (!IsConnected())
        {
            Console.WriteLine("StartWork error: not connected");
            return;
        }

        if (_connectAsync)
        {
            if (_workThread != null)
            {
                _workThread.Interrupt();
            }
            _workThread = new Thread(DoStartWork);
            _workThread.Start();
        }
        else
        {
            DoStartWork();
        }
    }

    protected virtual void Close()
    {
        _clientSocket.Close();
        _clientSocket = null;
    }

    MLAIPlayerInfo aiPlayer1 = new MLAIPlayerInfo()
    {
        lUin = 11111111,
        eState = E_MLAI_STATE.E_PLAYING,
        iLevel = 1,
    };

    MLAIPlayerInfo aiPlayer2 = new MLAIPlayerInfo()
    {
        lUin = 22222222,
        eState = E_MLAI_STATE.E_PLAYING,
        iLevel = 1,
    };

    MLAIPlayerInfo aiPlayer3 = new MLAIPlayerInfo()
    {
        lUin = 33333333,
        eState = E_MLAI_STATE.E_PLAYING,
        iLevel = 1,
    };

    long hostUin = 332189413;

    private void SendStartGameReq()
    {
        MLMessage msg = new MLMessage();
        msg.stMsgHead.lUin = hostUin;
        msg.stMsgHead.lSeq = 0;
        msg.stMsgHead.eMsgID = E_ML_MSG_ID.E_START_GAME;

        MLStartGameReq startGameReq = new MLStartGameReq();
        startGameReq.iID = 123;
        startGameReq.vecPlayerInfo.Add(aiPlayer1);
        startGameReq.vecPlayerInfo.Add(aiPlayer2);
        startGameReq.vecPlayerInfo.Add(aiPlayer3);

        startGameReq.WriteTo(msg.msgContent);

        JsonData data = new JsonData();
        msg.WriteTo(data);
        SendData(data);

        msgSeq++;
    }

    private void SendObservation(MLMsgHead msgHead, MLObservationMsg obsMsg)
    {
        MLMessage msg = new MLMessage();
        msg.stMsgHead = msgHead;
        obsMsg.WriteTo(msg.msgContent);

        JsonData data = new JsonData();
        msg.WriteTo(data);
        SendData(data);
    }

    int msgSeq = 0;

    public virtual void DoStartWork()
    {
        SendStartGameReq();

        while (true)
        {
            JsonData RspData = RecvData();

            //循环发消息收消息
            MLMessage rsp = new MLMessage();
            rsp.ReadFrom(RspData);

            long aiUin = 0;
            if (rsp.stMsgHead.eMsgID == E_ML_MSG_ID.E_ENV_RESET)
            {
                Console.WriteLine("recevie reset");

                MLResetEnvMsg resetMsg = new MLResetEnvMsg();
                resetMsg.ReadFrom(rsp.msgContent);
                aiUin = resetMsg.lUin;
                
            }
            else if (rsp.stMsgHead.eMsgID == E_ML_MSG_ID.E_STEP_ACTION)
            {
                Console.WriteLine("recevie action");

                MLGameActionMsg actionMsg = new MLGameActionMsg();
                actionMsg.ReadFrom(rsp.msgContent);
                aiUin = actionMsg.lUin;
            }
            else
            {
                Console.WriteLine("recevie unknow");
            }

            MLMsgHead msgHead = rsp.stMsgHead;
            msgHead.lSeq++;
            //msgHead.lUin = hostUin;
            msgHead.eMsgID = E_ML_MSG_ID.E_STEP_OBSERVATION;

            MLObservationMsg obsMsg = new MLObservationMsg();
            obsMsg.lUin = aiUin;

            SendObservation(msgHead, obsMsg);
        }
    }

    public long GetTimeStamp()
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return (long)ts.TotalSeconds;
    }

    public void SendData(JsonData content)
    {
        string stateJson = content.ToJson();
        Console.WriteLine("send:" + stateJson);

        byte[] Bytes = Encoding.UTF8.GetBytes(stateJson);

        byte[] LengthBytes = new byte[]
        {
                (byte)((Bytes.Length>>8) & 0XFF),
                (byte)(Bytes.Length & 0XFF),
        };
        lock (_clientSocket)
        {
            _clientSocket.Send(LengthBytes);
            _clientSocket.Send(Bytes);
        }
    }

    public byte[] ReadBuffer(int len)
    {
        byte[] RecvBuffer = new byte[1024 * 64];
        for (int readLen = 0; readLen < len;)
        {
            readLen += _clientSocket.Receive(RecvBuffer, readLen, len - readLen, SocketFlags.None);
        }
        return RecvBuffer;
    }

    public JsonData RecvData()
    {
        byte[] lengthBuffer = ReadBuffer(2);
        int dataLength = lengthBuffer[0] << 8 | lengthBuffer[1];
        byte[] contentBuffer = ReadBuffer(dataLength);
        string jsonContent = Encoding.UTF8.GetString(contentBuffer, 0, dataLength);

        Console.WriteLine("recv:" + jsonContent);
        return JsonMapper.ToObject(jsonContent);
    }
}