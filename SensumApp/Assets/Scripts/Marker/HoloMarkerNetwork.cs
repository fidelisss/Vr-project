using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class HoloMarkerNetwork : NetworkBehaviour
{
    public MarkerController _holoMarker;

    public void NetworkStart()
    {
        if (!isServer)
        {
            CmdStartDraw();
        }
    }

    public void NetworkStop()
    {
        if (!isServer)
        {
            CmdStopDraw();
        }
    }
    [Command]
    void CmdStartDraw()
    {
        RpcStartDraw();
    }

    [ClientRpc]
    void RpcStartDraw()
    {
        _holoMarker.StartDraw();
    }
    
    [Command]
    void CmdStopDraw()
    {
        RpcStopDraw();
    }

    [ClientRpc]
    void RpcStopDraw()
    {
        _holoMarker.StopDraw();
    }
}
