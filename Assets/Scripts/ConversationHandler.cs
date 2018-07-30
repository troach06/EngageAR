using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Services.Assistant.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Connection;
using IBM.Watson.DeveloperCloud.Logging;
using FullSerializer;
using MiniJSON;

public class ConversationHandler : MonoBehaviour
{
    public string username, password, workspaceID, versionDate, url, spokenText;
    Assistant _assistant;
    private bool _messageTested;
    public bool didIntroduce, nameSaid, locationSaid, occupationSaid, hobbySaid, ageSaid, locationAsked, occupationAsked, ageAsked, hobbyAsked, birthdaySaid, birthdayAsked, goodbyeSaid;
    private string _inputString = "Hello";
    private fsSerializer _serializer = new fsSerializer();
    private Dictionary<string, object> _input = null;
    private Dictionary<string, object> _context = null;

    void Start()
    {
        Credentials credentials = new Credentials(username, password, url);
        _assistant = new Assistant(credentials);
        _assistant.VersionDate = versionDate;
        _input = new Dictionary<string, object>();
        _context = new Dictionary<string, object>();

    }


    private void OnFail(RESTConnector.Error error, Dictionary<string, object> customData)
    {
        Log.Error("ExampleAssistant.OnFail()", "Error received: {0}", error.ToString());
        GetComponent<SpeechHandler>().Active = true;
        GetComponent<SpeechHandler>().StartRecording();
    }

    // Set context by sending messages to assistant
    public void Message()
    {
        if (!_input.ContainsKey("text"))
        {
            _input.Add("text", spokenText);
        }
        else
        {
            _input["text"] = spokenText;
        }

        if (!_context.ContainsKey("didIntroduce"))
        {
            didIntroduce = false;
            _context.Add("didIntroduce", "False");
        }
        if (!_context.ContainsKey("nameSaid"))
        {
            nameSaid = false;
            _context.Add("nameSaid", "False");
        }
        if (!_context.ContainsKey("locationSaid"))
        {
            locationSaid = false;
            _context.Add("locationSaid", "False");
        }
        if (!_context.ContainsKey("locationAsked"))
        {
            locationAsked = false;
            _context.Add("locationAsked", "False");
        }
        if (!_context.ContainsKey("occupationSaid"))
        {
            occupationSaid = false;
            _context.Add("occupationSaid", "False");
        }
        if (!_context.ContainsKey("occupationAsked"))
        {
            occupationAsked = false;
            _context.Add("occupationAsked", "False");
        }
        if (!_context.ContainsKey("hobbySaid"))
        {
            hobbySaid = false;
            _context.Add("hobbySaid", "False");
        }
        if (!_context.ContainsKey("hobbyAsked"))
        {
            hobbyAsked = false;
            _context.Add("hobbyAsked", "False");
        }
        if (!_context.ContainsKey("ageSaid"))
        {
            ageSaid = false;
            _context.Add("ageSaid", "False");
        }
        if (!_context.ContainsKey("ageAsked"))
        {
            ageAsked = false;
            _context.Add("ageAsked", "False");
        }
        if (!_context.ContainsKey("birthdaySaid"))
        {
            birthdaySaid = false;
            _context.Add("birthdaySaid", "False");
        }
        if (!_context.ContainsKey("birthdayAsked"))
        {
            birthdayAsked = false;
            _context.Add("birthdayAsked", "False");
        }
        if (!_context.ContainsKey("goodbyeSaid"))
        {
            goodbyeSaid = false;
            _context.Add("goodbyeSaid", "False");
        }
        MessageRequest messageRequest = new MessageRequest();
            messageRequest.Input = _input;
            messageRequest.Context = _context;
        _assistant.Message(OnMessage, OnFail, workspaceID, messageRequest);

    }

    private void OnMessage(object resp, Dictionary<string, object> customData)
    {
        object _tempContext = null;
        (resp as Dictionary<string, object>).TryGetValue("context", out _tempContext);

        if (_tempContext != null)
            _context = _tempContext as Dictionary<string, object>;
        string contextList = _context.ToString();

        Dictionary<string, object> dict = Json.Deserialize(customData["json"].ToString()) as Dictionary<string, object>;
        Dictionary<string, object> output = dict["output"] as Dictionary<string, object>;
        Debug.Log("JSON INFO: " + customData["json"].ToString());
        List<object> text = output["text"] as List<object>;

        if (text.Count > 0)
        {
            GetComponent<SpeechHandler>().Active = false;
            GetComponent<SpeechHandler>().StopRecording();
            string answer = text[0].ToString();

            Debug.Log("WATSON | Conversation output: \n" + answer);
            GetComponent<TextHandler>().messageText = answer;
            GetComponent<TextHandler>().Synthesize();
        }
        else
        {
            GetComponent<SpeechHandler>().Active = true;
            GetComponent<SpeechHandler>().StartRecording();
        }
    }
}