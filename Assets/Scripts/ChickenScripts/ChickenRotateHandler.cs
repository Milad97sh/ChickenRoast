using System;
using DG.Tweening;
using UnityEngine;


namespace ChickenRoast.Chicken
{
    public class ChickenRotateHandler
    {
        private readonly Transform skewer;
        private readonly Action<int> onRotateComplete;
        private float verticalInput;
        private Vector3 rotationDegree = Vector3.zero;

        public ChickenRotateHandler(Transform skewer, Action<int> onRotateComplete)
        {
            this.skewer = skewer;
            this.onRotateComplete = onRotateComplete;
        }

        public void HandleRotateInputs()
        {
            verticalInput = Input.GetAxis("Vertical");

            switch (verticalInput)
            {
                case > 0:
                    TryRotate(90, onRotateComplete);
                    break;
                case < 0:
                    TryRotate(-90, onRotateComplete);
                    break;
            }
        }

        private void TryRotate(float rotateTargetDegree, Action<int> onRotateComplete = null)
        {
            if (DOTween.IsTweening(skewer) == false)
                Rotate(rotateTargetDegree, onRotateComplete);
        }
        
        private void Rotate(float rotateTargetDegree, Action<int> onRotateComplete = null)
        {
            rotationDegree += Vector3.right * rotateTargetDegree;
            skewer.DORotate(rotationDegree, 0.5f).OnComplete(() => onRotateComplete?.Invoke(Math.Sign(rotateTargetDegree)));
        }
    }
}