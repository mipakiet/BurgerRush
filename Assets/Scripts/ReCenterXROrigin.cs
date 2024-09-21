using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ReCenterXROrigin : MonoBehaviour
{
   public Transform head;
   public Transform origin;
   public Transform target;

   private Vector3 offset;
   private Vector3 targetForward;
   private Vector3 cameraForward;
   private float angle;


   void Awake()
   {
      Recenter();
   }

   public void Recenter()
   {
      offset = head.position - origin.position;
      offset.y = 0;
      origin.position = target.position - offset;
      
      targetForward = target.forward;
      targetForward.y = 0;
      cameraForward = head.forward;
      cameraForward.y = 0;

      angle = Vector3.SignedAngle(cameraForward, targetForward, Vector3.up);
      
      origin.RotateAround(head.position, Vector3.up, angle);
   }
}