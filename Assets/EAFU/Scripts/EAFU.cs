using UnityEngine;
using UnityEngine.Events;

namespace EAFU
{
    // EAFU: Easily can be converted into:
    // Singleton Instance
    // Or Removed All Together, This class reveals AzureFunctionService.cs parameters to the Unity Inspector
    public class EAFU : MonoBehaviour
    {
        [SerializeField] private UnityEvent<object> onError;
        [SerializeField] private UnityEvent isLoading;
        [SerializeField] private UnityEvent isCompleted;

        public UnityEvent<object> OnError
        {
            get => onError;
            set => onError = value;
        }

        public UnityEvent IsLoading
        {
            get => isLoading;
            set => isLoading = value;
        }

        public UnityEvent IsCompleted
        {
            get => isCompleted;
            set => isCompleted = value;
        }

        public void Awake()
        {
            // EAFU: Set BaseURL and Callback Events for Basic EAFU Functions
            ApiService.onError += onError.Invoke;
            ApiService.isLoading += isLoading.Invoke;
            ApiService.isCompleted += isCompleted.Invoke;
        }
    }
}
