using Unity.Netcode.Components;
using UnityEngine;


public class ClientNetworkTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        CanCommitToTransform = IsOwner;       //只有拥有者可以提交自己的 transform 数据
    }

    protected override void Update()
    {
        // 防止 ownership 动态改变
        CanCommitToTransform = IsOwner;
        base.Update();
        if (!NetworkManager) return;
        // 只要当前程序是网络运行状态（不管是 Client 还是 Server）
        if (!NetworkManager.IsConnectedClient && !NetworkManager.IsListening) return;
        if(CanCommitToTransform)
        {
            TryCommitTransformToServer(transform, NetworkManager.LocalTime.Time);
        }
    }

}
