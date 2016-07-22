using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public enum LogLevel {
    LL_NORMAL,   //普通打印信息
    LL_WARNING,      //警告信息
    LL_ERROR,        //错误信息
    LL_BADERROR,     //严重错误信息
    LL_EXCEPTION,    //异常
}

public class Logger {
    /// <summary>
    /// 是否打印到控制台
    /// </summary>
    public static bool isDegLog = true;
    /// <summary>
    /// 错误信息List
    /// </summary>
    private static List<string> logInfoList = new List<string>();

    public static List<string> LogInfoList {
        get {
            return logInfoList;
        }

        private set {
            logInfoList = value;
        }
    }

    /// <summary>
    /// 把打印信息写入文件
    /// </summary>
    /// <param name="kFilePath"></param>
    public static void CloseLog(string kFilePath = "Log_Athoes.txt") {
        StreamWriter pkSw;
        FileStream pkFs;
        if (Application.platform == RuntimePlatform.Android) {
            pkFs = new FileStream(Application.persistentDataPath + kFilePath, FileMode.Create);
        } else {
            pkFs = new FileStream("LOG/" + System.DateTime.Today.ToString("yyyy-MM-dd") + kFilePath, FileMode.Append);
        }

        pkSw = new StreamWriter(pkFs, Encoding.Unicode);
        for (int i = 0; i < LogInfoList.Count; i++) {
            pkSw.Write(LogInfoList[i]);
            pkSw.WriteLine("\n");
        }
        pkSw.Close();
        pkSw = null;
        pkFs.Close();
        pkFs = null;
    }

    public static void Log(string msg, params object[] args) {
        Print(LogLevel.LL_NORMAL, msg, args);
    }

    public static void LogBadError(string msg, params object[] args) {
        Print(LogLevel.LL_BADERROR, msg, args);
    }

    public static void LogError(string msg, params object[] args) {
        Print(LogLevel.LL_ERROR, msg, args);
    }

    public static void LogException(System.Exception ex) {
        Print(LogLevel.LL_EXCEPTION, ex.Message);
    }

    public static void LogWarning(string msg, params object[] args) {
        Print(LogLevel.LL_WARNING, msg, args);
    }

    private static void Print(LogLevel eLevel, string msg, params object[] args) {
        if (args.Length > 0) {
            msg = string.Format(msg, args);
        }
        switch (eLevel) {
            case LogLevel.LL_NORMAL:
                msg = "Normal:" + msg;
                if (isDegLog) {
                    Debug.Log(msg);
                }
                break;
            case LogLevel.LL_WARNING:
                msg = "Warning:" + msg;
                if (isDegLog) {
                    Debug.LogWarning(msg);
                }
                break;
            case LogLevel.LL_ERROR:
                msg = "Error:" + msg;
                if (isDegLog) {
                    Debug.LogError(msg);
                }
                break;
            case LogLevel.LL_BADERROR:
                msg = "BadError:" + msg;
                if (isDegLog) {
                    Debug.LogError(msg);
                }
                break;
            case LogLevel.LL_EXCEPTION:
                msg = "Exception:" + msg;
                if (isDegLog) {
                    Debug.LogError(msg);
                }
                break;
        }
        string time = System.DateTime.Now.ToString();
        msg = time + msg;
        LogInfoList.Add(msg);
    }
}