using System;
using UnityEngine;

namespace EAFU
{
    // EAFU: Create Custom Apis using this BaseApi
    [Serializable]
    public class EAFUApi
    {
        [SerializeField] protected ApiEndpoints endpoints;

        public void Get<T>(Action<T> callback) =>
            ApiService.Get(endpoints.Get, callback);

        public void Post<T>(object requestBody, Action<T> callback) =>
            ApiService.Post(endpoints.Post, requestBody, callback);

        public void Put<T>(object requestBody, Action<T> callback) =>
            ApiService.Put(endpoints.Put, requestBody, callback);

        public void Delete(Action<object> callback) =>
            ApiService.Delete(endpoints.Delete, callback);
    }
}
