using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TSettings.Overrides;

namespace TSettings
{
    public class Settings
    {
        //The instace of the class
        public static Settings Default
        {
            get { return _default ?? (_default = new Settings()); }
        }

        private static Settings _default;
        private readonly string _filename;
        private SerializableDictionary<string, object> _settingsDictionary = new SerializableDictionary<string, object>(); 

        public Settings(string filename = "settings.bin")
        {
            _filename = filename;
            _default = this;

            if (File.Exists(_filename))
            {
                Load();
            }
        }

        public void Save()
        {
            if (!string.IsNullOrWhiteSpace(Path.GetDirectoryName(_filename)) && !Directory.Exists(Path.GetDirectoryName(_filename)))
                Directory.CreateDirectory(Path.GetDirectoryName(_filename));

            using (var fileStream = new FileStream(_filename, FileMode.Create))
            {
                IFormatter bf = new BinaryFormatter();
                bf.Serialize(fileStream, _settingsDictionary);
            }
        }

        private void Load()
        {
            try
            {
                using (var fileStream = new FileStream(_filename, FileMode.Open))
                {
                    IFormatter bf = new BinaryFormatter();
                    _settingsDictionary = (SerializableDictionary<string, object>) bf.Deserialize(fileStream);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("[Settings Error] {0}", e.ToString());
            }

        }


        public object this[string key]
        {
            get { return Get<object>(key); }
            set { Set(key, value);}
        }

        public T Get<T>(string key, T @default)
        {
            if (!Exist(key)) return @default;
            if (_settingsDictionary[key] != null)
            {
                return (T)_settingsDictionary[key];
            }

            return @default;
        }

        public T Get<T>(string key) where T : new()
        {
            if (!Exist(key)) return new T();

            return (T)_settingsDictionary[key];
        }

        public void Set<T>(string key, T @value)
        {
            if (Exist(key) && _settingsDictionary[key] != null)
            {
                var test = _settingsDictionary[key].GetType();

                if (@value.GetType() == test)
                {
                    _settingsDictionary[key] = @value;
                }

                return;
            }

            _settingsDictionary.Add(key, @value);
        }

        public Type GetValueType(string key)
        {
            return Exist(key) ? _settingsDictionary[key].GetType() : null;
        }

        public bool Delete(string key)
        {
            if (!_settingsDictionary.ContainsKey(key)) return false;

            _settingsDictionary.Remove(key);

            return true;
        }

        public bool Exist(string key)
        {
            return _settingsDictionary.ContainsKey(key);
        }
    }
}
