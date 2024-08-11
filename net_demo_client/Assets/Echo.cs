using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;
using System;
using TMPro;
public class Echo : MonoBehaviour
{

    //�����׽���
    Socket socket;
    //UGUI
    public TMP_InputField InputFeld;
    public TMP_Text text;
    //���ջ�����
    byte[] readBuff = new byte[1024];
    string recvStr = "";

    //������Ӱ�ť
    public void Connection()
    {
        //Socket
        socket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
        //Connect
        socket.BeginConnect("127.0.0.1", 8888, ConnectCallback, socket);
    }

    //Connect�ص�
    public void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndConnect(ar);
            Debug.Log("Socket Connect Succ");
            socket.BeginReceive(readBuff, 0, 1024, 0,
                ReceiveCallback, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Connect fail" + ex.ToString());
        }
    }

    //Receive�ص�
    public void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndReceive(ar);
            string s = System.Text.Encoding.Default.GetString(readBuff, 0, count);
            recvStr = s + "\n" + recvStr;
            Debug.Log("string s " + s + " recvStr " + recvStr);

            socket.BeginReceive(readBuff, 0, 1024, 0,
                ReceiveCallback, socket);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Receive fail" + ex.ToString());
        }
    }

    //������Ͱ�ť
    //������Ͱ�ť
    public void Send()
    {
        //Send
        string sendStr = InputFeld.text;
        byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
        socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socket);
    }

    //Send�ص�
    public void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket socket = (Socket)ar.AsyncState;
            int count = socket.EndSend(ar);
            Debug.Log("Socket Send succ" + count);
        }
        catch (SocketException ex)
        {
            Debug.Log("Socket Send fail" + ex.ToString());
        }
    }
    public void Update()
    {
        text.text = recvStr;
    }
}