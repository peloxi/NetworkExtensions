﻿using ColossalFramework;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace CSL_Traffic
{
    static class FileManager
    {
        public enum Folder
        {
            Textures,
            UI
        }

        static readonly string MOD_PATH = FindModPath(); 

        static readonly Dictionary<Folder, string> sm_relativeTextureFolderPaths = new Dictionary<Folder, string>()
        {
            {Folder.Textures,       "Textures/"},
            {Folder.UI,             "Textures/UI/"},
        };

        static Dictionary<string, byte[]> sm_cachedFiles = new Dictionary<string, byte[]>();

        static string FindModPath()
        {
            PluginManager.PluginInfo plugin = Singleton<PluginManager>.instance.GetPluginsInfo().FirstOrDefault(p => p.name == "Traffic++" || p.publishedFileID.AsUInt64 == UserMod.WORKSHOP_ID);
            if (plugin != null)
                return plugin.modPath;
            else
                Logger.LogInfo("Cannot find plugin path.");

            return null;
        }

        public static bool GetTextureBytes(string fileName, Folder folder, out byte[] bytes)
        {
            return GetTextureBytes(fileName, folder, false, out bytes);
        }

        public static bool GetTextureBytes(string fileName, Folder folder, bool skipCache, out byte[] bytes)
        {
            bytes = null;
            string filePath = GetFilePath(fileName, folder);
            if (filePath == null || !File.Exists(filePath))
            {
#if DEBUG
                Logger.LogInfo("Cannot find texture file at " + filePath);
#endif
                return false;
            }

            if (!skipCache && sm_cachedFiles.TryGetValue(filePath, out bytes))
                return true;

            try
            {
                bytes = File.ReadAllBytes(filePath);
            }
            catch (Exception e)
            {
                Logger.LogInfo("Unexpected " + e.GetType().Name + " reading texture file at " + filePath);
                return false;
            }

            sm_cachedFiles[filePath] = bytes;
            return true;
        }

        public static string GetFilePath(string fileName, Folder folder)
        {
            if (MOD_PATH == null)
                return null;
            
            string relativeFolderPath;
            if (!sm_relativeTextureFolderPaths.TryGetValue(folder, out relativeFolderPath))
                return null;

            string path = MOD_PATH;
            path = Path.Combine(path, relativeFolderPath);
            return Path.Combine(path, fileName);
        }

        public static void ClearCache()
        {
            sm_cachedFiles.Clear();
        }
    }
}
