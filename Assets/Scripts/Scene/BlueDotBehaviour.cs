using System;
using App;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scene
{
    /// <summary>
    /// Attached to BlueDot.prefab. Handles gameObjects movements and speed on runtime.
    /// Speed is changed by <see cref="BlueDotUIController"/>
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class BlueDotBehaviour : MonoBehaviour
    {
        private const float MaxMoveSpeed = 4000f;
        private const float MinMoveSpeed = 250f;
        private const float SpeedChangeAmount = 250f;
        private const float PlayFieldSizePercentage = 0.99f;
        private const float PlayFieldOutOfBoundsTolerancePercentage = 1.2f;
        
        public event Action Bouncing;
        
        public bool IsMinSpeedReached { get; private set; }
        public bool IsMaxSpeedReached { get; private set; }
        
        public float CircleRadius => GetComponent<RectTransform>().sizeDelta.x;
        public Vector2 CirclePos => new Vector2(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y);
        
        [SerializeField] private Transform bounceEffect;
        
        private float currentMoveSpeed = 2000f;
        private float startingDirection;
        private bool canBounce;
        
        private Vector2 xBorderVector2;
        private Vector2 yBorderVector2;
        private Vector2 screenResolution;
        private Quaternion facingDirection;
        private Vector3 movement;
        
        private RectTransform rect;
        private ResolutionViewer resolutionViewer;

        private void OnEnable()
        {
            rect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            resolutionViewer = ResolutionViewer.Instance;

            ResetTransform();
            SetScreenBoundaries();
        }

        private void Update()
        {
            facingDirection = Quaternion.Euler(0, 0, startingDirection);
            movement = facingDirection * Vector2.up;
            rect.rotation = facingDirection;
            rect.position += movement * (Time.deltaTime * currentMoveSpeed);

            if (rect.anchoredPosition.x > xBorderVector2.y * PlayFieldOutOfBoundsTolerancePercentage ||
                rect.anchoredPosition.x < xBorderVector2.x * PlayFieldOutOfBoundsTolerancePercentage ||
                rect.anchoredPosition.y > yBorderVector2.y * PlayFieldOutOfBoundsTolerancePercentage ||
                rect.anchoredPosition.y < yBorderVector2.x * PlayFieldOutOfBoundsTolerancePercentage)
            {
                ResetTransform();
            }
        }

        private void LateUpdate()
        {
            if (canBounce)
            {
                HandleBounce();
            }

            ResetCanBounce();
        }

        private void ResetTransform()
        {
            rect.anchoredPosition = Vector2.zero;
            startingDirection = Random.Range(0f, 360f);
        }

        private void SetScreenBoundaries()
        {
            screenResolution = resolutionViewer.ScreenResolution;

            xBorderVector2 = new Vector2(-screenResolution.x / 2f + CircleRadius / 2f,
                screenResolution.x / 2f - CircleRadius / 2f);
            yBorderVector2 = new Vector2(-screenResolution.y / 2f + CircleRadius / 2f,
                screenResolution.y / 2f - CircleRadius / 2f);
        }

        private void HandleBounce()
        {
            if (rect.anchoredPosition.x > screenResolution.x / 2f - CircleRadius / 2f ||
                rect.anchoredPosition.x < -screenResolution.x / 2f + CircleRadius / 2f)
            {
                Bounce(360f - transform.rotation.eulerAngles.z);
            }
            else if (rect.anchoredPosition.y > screenResolution.y / 2f - CircleRadius / 2f ||
                     rect.anchoredPosition.y < -screenResolution.y / 2f + CircleRadius / 2f)
            {
                Bounce(180f - transform.rotation.eulerAngles.z);
            }
        }

        private void ResetCanBounce()
        {
            if (rect.anchoredPosition.x < (screenResolution.x / 2f - CircleRadius / 2f) * PlayFieldSizePercentage &&
                rect.anchoredPosition.x > (-screenResolution.x / 2f + CircleRadius / 2f) * PlayFieldSizePercentage &&
                rect.anchoredPosition.y < (screenResolution.y / 2f - CircleRadius / 2f) * PlayFieldSizePercentage &&
                rect.anchoredPosition.y > (-screenResolution.y / 2f + CircleRadius / 2f) * PlayFieldSizePercentage)
            {
                canBounce = true;
            }
        }

        private void Bounce(float angleMod)
        {
            startingDirection = angleMod;
            Instantiate(bounceEffect, transform.position, Quaternion.identity, transform.parent);
            Bouncing?.Invoke();
            canBounce = false;
        }

        public void ChangeSpeed(bool increaseSpeed)
        {
            float newSpeed;
            if (increaseSpeed)
            {
                newSpeed = currentMoveSpeed + SpeedChangeAmount;
                if (newSpeed < MaxMoveSpeed || Math.Abs(newSpeed - MaxMoveSpeed) < 1f)
                {
                    currentMoveSpeed = newSpeed;
                }
            }
            else
            {
                newSpeed = currentMoveSpeed - SpeedChangeAmount;
                if (newSpeed > MinMoveSpeed || Math.Abs(newSpeed - MinMoveSpeed) < 1f)
                {
                    currentMoveSpeed = newSpeed;
                }
            }

            IsMinSpeedReached = Mathf.Approximately(MinMoveSpeed, currentMoveSpeed);
            IsMaxSpeedReached = Mathf.Approximately(MaxMoveSpeed, currentMoveSpeed);
        }
    }
}