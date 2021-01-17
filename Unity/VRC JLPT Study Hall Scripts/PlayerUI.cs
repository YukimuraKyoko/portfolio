
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PlayerUI : UdonSharpBehaviour
{


    void Start()
    {
        
    }

    public void Update()
    {
        VRCPlayerApi.TrackingData trackingData = Networking.LocalPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
        transform.SetPositionAndRotation(trackingData.position, trackingData.rotation);

    }
}
