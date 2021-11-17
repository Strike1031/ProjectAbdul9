using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using WebSocketSharp;
using WebSocketUnity;

//{ "event":"message","success":true,"message":"here_message_to_be_signed","errors":[] }
// public class ResponseSignMessage
// {
//     public string events;
//     public bool success;
//     public string message;
//     public string[] errors;
// }

//mine :: MYGlobalClasses saves global variables
public class MyGlobalClasses : MonoBehaviour
{
    public static MyGlobalClasses mInstance;
     
    public static string mySignMessage;
    public static string accountsWalletAddress;
    public static int chainId;
    private void Awake()
    {
         mInstance = this;
         DontDestroyOnLoad(mInstance);
    }
}