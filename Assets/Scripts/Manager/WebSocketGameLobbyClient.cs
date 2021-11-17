using Newtonsoft.Json.Linq;
using UnityEngine;
using WebSocketUnity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public struct Message
{

    public string type;

    public string gameId;

    public string gameCode;

    public string playerId;

}

public class WebSocketGameLobbyClient : MonoBehaviour
{

    [SerializeField]
    private WebSocketJsonController _webSocketJsonController;

    public void HandleMessage(JObject message)
    {
        Debug.LogError($"$$Message received: {JsonConvert.SerializeObject(message)}"); 
        //Debug.LogError("$$$$$$:"+message["event"]);
        //Debug.LogError($"$$Message received: {message.ToString(Newtonsoft.Json.Formatting.None)}"); 
        //ResponseSignMessage mySignMessage = JsonConvert.DeserializeObject<ResponseSignMessage>(message.ToString(Newtonsoft.Json.Formatting.None));
        string myTypes = message["event"].ToString();
        //mine for signing///
        //
        string signMessage;
        switch(myTypes)
        {
         case "message":
            Debug.LogError("11111111____SignMessage");   
            signMessage = message["message"].ToString();
            //Debug.LogError("SignedMessage::"+ message["message"]);
            Debug.LogError("SignedMessage::"+ signMessage);
            MyGlobalClasses.mySignMessage = signMessage;
            break;
        case "verify":
        case "error":
            Debug.LogError("22222222___verifyMessage");
            SceneManager.LoadScene("main");
            break;
        case "move":
            Debug.LogError("33333333___move");
            break;
        default:
            break;
        }
    }
    /*
    public void CreateGame()
    {

        _webSocketJsonController.Send(new Message { type = "create" });

    }

    public void JoinGame()
    {

        _webSocketJsonController.Send(new Message { type = "join" });

    }*/

}