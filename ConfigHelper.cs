﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace OracleBankData
{
    public static class ConfigHelper<T>
    {
        private static T _instance;
        private static string _jsonContent;
        private static readonly object LockObj = new object();
        private static string ConfigFile;

        public static T Instance => GetConfigInfo();

        public static T GetConfigInfo()
        {
            lock (LockObj)
            {
                if (_instance != null) return _instance;

                _jsonContent = File.ReadAllText(ConfigFile);
                _instance = JsonConvert.DeserializeObject<T>(_jsonContent);
            }

            return _instance;
        }

        public static T GetConfigInfo(string configFile)
        {
            if (_instance != null) return _instance;

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", configFile);
            if (!File.Exists(path))
                throw new FileNotFoundException("Config file not exist.");
            ConfigFile = path;

            return GetConfigInfo();
        }

        public static bool UpdateConfig(T info)
        {
            T configInfo;
            configInfo = info;

            _jsonContent = JsonConvert.SerializeObject(configInfo, Formatting.Indented);
            File.WriteAllText(ConfigFile, _jsonContent);

            return true;
        }
    }
}
