using System;
using System.Collections;
using System.Collections.Generic;
using Utility.JSON;
using UnityEngine;

namespace Utility
{
    public static class JSONHelper
    {
        public static T toObject<T>(string json)
        {
            JSONObject jsonObj = JSONObject.Parse(json);
            T obj = JsonUtility.FromJson<T>(jsonObj.ToString());
            return obj;
        }

        public static List<T> jsonArrayToList<T>(string key, string json){
            List<T> jsonList = new List<T>();
            JSONObject jsonObj = JSONObject.Parse(json);

            if (!jsonObj.GetValue(key).Type.Equals("array")) return null;
            for (int i = 0; i < jsonObj.GetValue(key).Array.Length; i++) {
                jsonList.Add(JsonUtility.FromJson<T>(jsonObj.GetValue(key).Array[i].ToString()));
            }

            return jsonList;
        }

        public static List<T> fromJsonToList<T>(string type, string json)
        {
            string jso = "{\"" + type + "\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(jso);

            List<T> myList = new List<T>();

            for (int i = 0; i < wrapper.Items.Length; i++)
            {
                Debug.Log(wrapper.Items[i]);
                myList.Add(wrapper.Items[i]);
            }

            return myList;
        }

        public static string jsonFy(Dictionary<string, object> dic){
            string newJson = "{";

            foreach (var pair in dic)
            {
                newJson += "\"" + pair.Key + "\":\"" + pair.Value + "\",";
            }

            newJson = newJson.Substring(0, newJson.Length - 1);
            newJson += "}";

            return newJson;
        }  

        public static T errorHandler<T>(string errorCode){
            if(errorCode.Contains("error")){
                JSONObject jsonObj = JSONObject.Parse(errorCode);
                T obj = JsonUtility.FromJson<T>(jsonObj.ToString());
                return obj;
            }
            else if(errorCode.Contains("message")){
                JSONObject jsonObj = JSONObject.Parse(errorCode);
                T obj = JsonUtility.FromJson<T>(jsonObj.ToString());
                return obj;
            }
            else{
                JSONObject jsonObj = JSONObject.Parse(errorCode);
                T obj = JsonUtility.FromJson<T>(jsonObj.ToString());
                return obj;
            }
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}

