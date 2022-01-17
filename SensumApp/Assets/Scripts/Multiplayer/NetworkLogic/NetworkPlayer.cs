using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Transform head;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Transform headRig;
    [SerializeField] private Transform leftHandRig;
    [SerializeField] private Transform rightHandRig;
    [SerializeField] private Animator leftHandAnimator;
    [SerializeField] private Animator rightHandAnimator;
    [SerializeField] private Text nameText;
    
    private string _name;
    private bool _iAmTeacher;
    private bool _isHeTeacher;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        var rig = FindObjectOfType<XRRig>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
        rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");

        _name = photonView.Owner.NickName;

        if (photonView.Owner.NickName == "ADMINMAN" || photonView.Owner.NickName == "ADMINWOMAN")
        {
            _iAmTeacher = true;
        }

        if (PhotonNetwork.NickName == "ADMINMAN" || PhotonNetwork.NickName == "ADMINWOMAN")
        {
            _isHeTeacher = true;
        }
        
        if (photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;
            }
        }
        else
        {
            if (_iAmTeacher || _isHeTeacher)
            {
                foreach (var item in GetComponentsInChildren<Renderer>())
                {
                    item.enabled = true;
                }
            }
            else
            {
                transform.Find("LeftHand").gameObject.SetActive(false);
                transform.Find("RightHand").gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }

    private void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Index", triggerValue);
        }
        else if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float fingerValue))
        {
            handAnimator.SetFloat("ThreeFingers", fingerValue);
        }
        else if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float thumbValue))
        {
            handAnimator.SetFloat("Thumb", thumbValue);
        }
    }

    private void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}