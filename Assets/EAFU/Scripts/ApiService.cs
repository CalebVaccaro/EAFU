using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace EAFU
{
    public sealed class ApiService
    {
        // EAFU: Use AzureService.cs or Environment Variable to set baseURL
        public static Action<string> onError;
        public static Action isLoading;
        public static Action isCompleted;

        public static async void Get<T>(string url, Action<T> callback) => 
            await GetRequest(url, callback);

        public static async void Post<T>(string url, object body, Action<T> callback) => 
            await PostRequest(url, body, callback);

        public static async void Put<T>(string url, object body, Action<T> callback) => 
            await PutRequest(url, body, callback);

        public static async void Delete(string url, Action<object> callback) => 
            await DeleteRequest(url, callback);

        private static async Task GetRequest<T>(string url, Action<T> callback)
        {
            string fullUrl = RemoteConfigSettings.Instance.URL + url;
            using UnityWebRequest www = UnityWebRequest.Get(fullUrl);
            www.SetRequestHeader("x-functions-key", RemoteConfigSettings.Instance.Key);
            www.SendWebRequest();
            isLoading?.Invoke();

            while (!www.isDone) await Task.Yield();
            if (www.result != UnityWebRequest.Result.Success){
                onError?.Invoke(www.error);
                return;
            }

            try{
                T response = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
                callback?.Invoke(response);
            }
            catch (Exception e){
                onError?.Invoke(e.Message);
            }
            finally{
                isCompleted?.Invoke();
            }
        }

        private static async Task PostRequest<T>(string url, object body, Action<T> callback)
        {
            string fullUrl = RemoteConfigSettings.Instance.URL + url;
            string bodyJson = JsonConvert.SerializeObject(body);
            byte[] bodyRaw = new UTF8Encoding().GetBytes(bodyJson);
            var www = new UnityWebRequest(fullUrl, "POST");
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("x-functions-key", RemoteConfigSettings.Instance.Key);
            www.SendWebRequest();
            isLoading?.Invoke();

            while (!www.isDone) await Task.Yield();
            if (www.result != UnityWebRequest.Result.Success){
                onError?.Invoke(www.error);
                return;
            }

            try{
                T response = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
                callback?.Invoke(response);
            }
            catch (Exception e){
                onError?.Invoke(e.Message);
            }
            finally
            {
                isCompleted?.Invoke();
            }
        }

        private static async Task PutRequest<T>(string url, object body, Action<T> callback)
        {
            string fullUrl = RemoteConfigSettings.Instance.URL + url;
            string bodyJson = JsonConvert.SerializeObject(body);
            byte[] bodyRaw = new UTF8Encoding().GetBytes(bodyJson);
            var www = new UnityWebRequest(fullUrl, "PUT");
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("x-functions-key", RemoteConfigSettings.Instance.Key);
            www.SendWebRequest();
            isLoading?.Invoke();

            while (!www.isDone) await Task.Yield();
            if (www.result != UnityWebRequest.Result.Success){
                onError?.Invoke(www.error);
                return;
            }

            try{
                T response = JsonConvert.DeserializeObject<T>(www.downloadHandler.text);
                callback?.Invoke(response);
            }
            catch (Exception e){
                onError?.Invoke(e.Message);
            }
            finally
            {
                isCompleted?.Invoke();
            }
        }

        private static async Task DeleteRequest(string url, Action<object> callback)
        {
            string fullUrl = RemoteConfigSettings.Instance.URL + url;
            using UnityWebRequest www = UnityWebRequest.Delete(fullUrl);
            www.SetRequestHeader("x-functions-key", RemoteConfigSettings.Instance.Key);
            www.SendWebRequest();
            isLoading?.Invoke();

            while (!www.isDone) await Task.Yield();

            if (www.result != UnityWebRequest.Result.Success){
                onError?.Invoke(www.error);
            }
            else
            {
                callback?.Invoke(www.result);
                isCompleted?.Invoke();
            }
        }
    }
}