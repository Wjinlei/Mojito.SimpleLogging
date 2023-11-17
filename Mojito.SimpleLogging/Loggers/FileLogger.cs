﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mojito.SimpleLogging.Loggers
{
    public class FileLogger : Logger
    {
        private readonly object _lock = new object();

        public override void Log(string message, LogLevel level)
        {
            var logPath = LogConfigHelper.GetLogPath();
            if (IsWriteable(level))
            {
                lock (_lock)
                {
                    CheckRollBackups();
                    CheckDirectory(logPath);
                    File.AppendAllText(logPath, message);
                }
            }
        }

        /// <summary>
        /// 检查是否需要滚动日志
        /// </summary>
        private static void CheckRollBackups()
        {
            var rollTimeInMinutes = LogConfigHelper.GetRollTimeInMinutes();
            var maxRollSizeInKB = LogConfigHelper.GetMaxRollSizeInKB();
            var file = new FileInfo(LogConfigHelper.GetLogPath());
            if (!file.Exists)
                return;

            var fileSizeInBytes = file.Length;
            var fileSizeInKB = fileSizeInBytes / 1024;
            var timeDifference = DateTime.Now - file.CreationTime;

            if ((rollTimeInMinutes > 0 && timeDifference.TotalMinutes >= rollTimeInMinutes) ||
                (maxRollSizeInKB > 0 && fileSizeInKB >= maxRollSizeInKB))
            {
                RollBackups();
            }
        }

        /// <summary>
        /// 滚动日志
        /// </summary>
        private static void RollBackups()
        {
            var now = DateTime.Now;
            var strNow = $"{now:yyyy-MM-dd_HH-mm-ss}";
            var logPath = LogConfigHelper.GetLogPath();
            var maxRollBackups = LogConfigHelper.GetMaxRollBackups();
            try
            {
                if (maxRollBackups > 0)
                    DeleteOldRollBackups(maxRollBackups);

                File.Move(logPath, $"{logPath}.{strNow}");
                File.Create(logPath).Close();
                File.SetCreationTime(logPath, now);
            }
            catch (Exception ex)
            {
                var errorMessage = $"{strNow} RollError {ex.Message} {ex.StackTrace}{Environment.NewLine}";
                File.AppendAllText(logPath, errorMessage);
            }
        }

        /// <summary>
        /// 删除旧的滚动日志，保持最大滚动日志数量
        /// </summary>
        /// <param name="maxRollBackups">最大日志数量</param>
        private static void DeleteOldRollBackups(int maxRollBackups)
        {
            var logPath = LogConfigHelper.GetLogPath();

            var fileName = Path.GetFileName(logPath);
            if (string.IsNullOrWhiteSpace(fileName))
                throw new NullReferenceException($"GetFileName Error: logPath is {logPath}");

            var fileDir = Path.GetDirectoryName(logPath);
            if (string.IsNullOrWhiteSpace(fileDir))
                fileDir = Environment.CurrentDirectory;

            // 过滤日志备份文件
            var regexPattern = $@"^{Regex.Escape(fileName)}\..*";
            var files = Directory.GetFiles(fileDir)
                .Where(f => Regex.IsMatch(Path.GetFileName(f), regexPattern))
                .ToArray();

            if (files == null)
                return;

            if (files.Length >= maxRollBackups)
            {
                // 按创建时间排序
                Array.Sort(files, (a, b) => File.GetCreationTime(a).CompareTo(File.GetCreationTime(b)));

                for (int i = 0; i <= files.Length - maxRollBackups; i++)
                    File.Delete(files[i]);
            }
        }

        private static void CheckDirectory(string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrWhiteSpace(dir))
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
        }
    }
}