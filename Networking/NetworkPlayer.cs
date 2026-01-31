using Mirror;
using UnityEngine;

namespace Mod.Networking
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public void Start()
        {
            Debug.Log("Player spawned");
            if (isLocalPlayer)
                Destroy(GetComponentInChildren<BoxCollider>());
        }

        public void Update()
        {
            if (isLocalPlayer)
            {
                transform.position = NewMovement.Instance.transform.position;
                transform.rotation = NewMovement.Instance.transform.rotation;
                transform.localScale = NewMovement.Instance.transform.lossyScale;
            }
        }
    }
}