using ExitGames.Client.Photon;
using System.Collections.Generic;

public class PhotonConnector : IPhotonPeerListener 
{
	private PhotonPeer _peer;
	private Login login;
	private int counter = 0;

	public PhotonConnector()
	{
		Properties.Status = "Disconnected";
	}

	public void DebugReturn (DebugLevel level, string message)
	{

	}
	
	public void OnEvent (EventData eventData)
	{

	}
	
	public void OnOperationResponse (OperationResponse operationResponse)
	{
		switch(operationResponse.OperationCode)
		{
			case 1:
				if(operationResponse.Parameters.ContainsKey(1))
				{
					login.DebugMessage((string)operationResponse.Parameters[1]);
				}

			break;
			case 2:
				if(operationResponse.Parameters.ContainsKey(1))
				{
					login.DebugMessage((string)operationResponse.Parameters[1]);
					login.gridGenerated = true;
				}
			break;
			case 3:
				object response = operationResponse.Parameters[1];
				int[] message = (int[])response;
				login.NewPath(message, counter);
				login.DebugMessage("Path Generated");
				counter++;
			break;
			default:
				login.DebugMessage("Error Operation code: "+operationResponse.OperationCode);
			break;

		}
	}
	
	public void OnStatusChanged (StatusCode statusCode)
	{
		switch(statusCode)
		{
			case StatusCode.Connect:
				Properties.Status = "Connected";
				break;
			case StatusCode.Disconnect:
			case StatusCode.DisconnectByServer:
			case StatusCode.DisconnectByServerLogic:
			case StatusCode.DisconnectByServerUserLimit:
			case StatusCode.TimeoutDisconnect:
				Properties.Status = "Disconnected";
				login.connected = false;
				break;
			default:
				Properties.Status = "Unknown";
				break;
		}
	}
	public void Initialize(PhotonPeer peer, string serverAddress, string applicationName, Login loginBehaviour)
	{
		_peer = peer;
		_peer.Connect(serverAddress, applicationName);
		login = loginBehaviour;
	}
	public void Disconnect()
	{
		_peer.Disconnect();
	}

	public void Update()
	{
		_peer.Service();
		Properties.ServerPing = _peer.RoundTripTime;
	}

	public void SendRequest(byte opCode, byte key, object message, bool sendReliable)
	{
		login.DebugMessage("Request Sent");
		_peer.OpCustom(opCode, new Dictionary<byte, object>{{ key, message }}, true);
	}
}
