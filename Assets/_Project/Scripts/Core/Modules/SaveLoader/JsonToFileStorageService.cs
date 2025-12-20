using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class JsonToFileStorageService : IStorageService
{
    private bool _isInProgressNow;
    public void Load<T>(string key, Action<T> callback, Func<T> createDefault = null)
    {
        string path = BuildPath(key);

        if (!File.Exists(path))
        {
            // first run
            if (createDefault == null)
            {
                throw new Exception("Ты долбоёб");
            }

            T def = createDefault();
            Save(key, def);
            callback?.Invoke(def);
            return;
        }

        using (var reader = new StreamReader(path))
        {
            var json = reader.ReadToEnd();
            var data = JsonConvert.DeserializeObject<T>(json);
            callback?.Invoke(data);
        }
    }

    public void Save(string key, object data, Action<bool> callback = null)
    {
        if (!_isInProgressNow)
        {
            SaveAsync(key, data, callback);
        }
        else 
        {
            callback?.Invoke(false);
        }
    }

    public async void SaveAsync(string key, object data, Action<bool> callback)
    {
        string path = BuildPath(key);
        string json = JsonConvert.SerializeObject(data);

        using (var fileStream = new StreamWriter(path))
        {
            _isInProgressNow = true;
            await Task.Delay(500);
            await fileStream.WriteAsync(json);
        }
        _isInProgressNow = false;
        callback?.Invoke(true); 
    }

    private string BuildPath(string key)
    {
        return Path.Combine(Application.persistentDataPath, key);
    }
}