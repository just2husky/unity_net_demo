using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;

using TMPro;

public class Echo : MonoBehaviour
{
    //�����׽���
    Socket socket;
    //UGUI
    public TMP_InputField InputFeld;
    public TMP_Text text;

    //������Ӱ�ť
    public void Connection()
    {
        //Socket
        socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        //Connect
        socket.Connect("127.0.0.1", 8888);
    }

    //������Ͱ�ť
    public void Send()
    {
        //Send
        string sendStr = InputFeld.text;
        byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
        socket.Send(sendBytes);
        //Recv
        byte[] readBuff = new byte[1024];
        int count = socket.Receive(readBuff);
        string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
        text.text = recvStr;
        //Close
        socket.Close();
    }
}