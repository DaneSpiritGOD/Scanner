using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Archiving.Core.Options
{
    public static class AppOptionsPostConfigure
    {
        public static void PostAction(AppOptions mo)
        {
            NamedNullException.Assert(mo, nameof(mo));

            var defaultMonitor = mo.Default?.Monitor;
            var defaultOperation = mo.Default?.Operation;

            NumberOutOfRangeException<int>.Assert(
                mo.FileReadSpinWaitTimeout,
                0, AppOptions.MaxFileReadSpinWaitTimeout,
                nameof(mo.FileReadSpinWaitTimeout));

            var groups = NamedNullException.Assert(mo.Groups, nameof(mo.Groups));

            foreach (var group in groups)
            {
                NamedNullException.Assert(group, nameof(group));
                StringNullOrEmptyException.Assert(group.Name, nameof(group.Name));

                group.Monitor = postMonitor(group.Monitor, in defaultMonitor);

                NamedNullException.Assert(group.Operations, nameof(group.Operations));
                NotTrueException.Assert(group.Operations.Count() > 0, nameof(group.Operations));

                foreach (var op in group.Operations)
                {
                    postOperation(op, defaultOperation);
                }
            }
        }

        private static MonitorOptions postMonitor(in MonitorOptions monitor, in MonitorOptions _default)
        {
            if (_default == null) return ensureMonitorFullConfigured(monitor);
            if (monitor == null) return ensureMonitorFullConfigured(_default);

            extractFromDefault(monitor, _default);
            return monitor;

            MonitorOptions ensureMonitorFullConfigured(MonitorOptions localMonitorOptions)
            {
                NamedNullException.Assert(
                    localMonitorOptions,
                    nameof(localMonitorOptions));

                StringNullOrEmptyException.Assert(
                    localMonitorOptions.Path,
                    nameof(localMonitorOptions.Path));

                StringNullOrEmptyException.Assert(
                    localMonitorOptions.FileNameKey,
                    nameof(localMonitorOptions.FileNameKey));

                NotTrueException.Assert(
                    localMonitorOptions.FileExtensions.Count() > 0,
                    nameof(localMonitorOptions.FileExtensions));

                return localMonitorOptions;
            }

            void extractFromDefault(MonitorOptions local, MonitorOptions localDefault)
            {
                local.Path = StringNullOrEmptyException.Assert(
                    extractStringValueFromDefault(
                         local.Path, localDefault.Path),
                    nameof(local.Path));

                local.FileNameKey = StringNullOrEmptyException.Assert(
                    extractStringValueFromDefault(
                         local.FileNameKey, localDefault.FileNameKey),
                    nameof(local.Path));

                local.FileExtensions = extractRefValueFromDefault(
                        local.FileExtensions,
                        localDefault.FileExtensions);
                NotTrueException.Assert(
                    local.FileExtensions.Count() > 0,
                    nameof(local.FileExtensions));
            }
        }

        private static void postOperation(OperationOptions operation, RawOperation _default)
        {
            NamedNullException.Assert(operation, nameof(operation));
            if (_default == null) ensureOperationConfiguredRightly(operation);

            extractFromDefault(operation, _default);

            void ensureOperationConfiguredRightly(OperationOptions localOperation)
            {
                if (localOperation.EnableNetTransfer)
                    StringNullOrEmptyException.Assert(localOperation.NetAddress, nameof(localOperation.NetAddress));

                if (localOperation.EnableFtpTransfer)
                    FtpOptions.EnsureValid(localOperation.Ftp);

                if (localOperation.EnableMove)
                    StringNullOrEmptyException.Assert(localOperation.MovePath, nameof(localOperation.MovePath));

                if (localOperation.EnableBackup)
                    StringNullOrEmptyException.Assert(localOperation.BackupPath, nameof(localOperation.BackupPath));
            }

            void extractFromDefault(OperationOptions local, RawOperation localDefault)
            {
                if (local.EnableNetTransfer && string.IsNullOrWhiteSpace(local.NetAddress))
                    local.NetAddress = StringNullOrEmptyException.Assert(localDefault.NetAddress, nameof(localDefault.NetAddress));

                if (local.EnableFtpTransfer)
                {
                    if (local.Ftp == default)
                    {
                        FtpOptions.EnsureValid(localDefault.Ftp);
                        local.Ftp = localDefault.Ftp;
                    }
                    else
                        FtpOptions.EnsureValid(local.Ftp);
                }

                if (local.EnableMove && string.IsNullOrWhiteSpace(local.MovePath))
                    local.MovePath = StringNullOrEmptyException.Assert(localDefault.MovePath, nameof(localDefault.MovePath));

                if (local.EnableBackup && string.IsNullOrWhiteSpace(local.BackupPath))
                    local.BackupPath = StringNullOrEmptyException.Assert(localDefault.BackupPath, nameof(localDefault.BackupPath));
            }
        }

        static T extractValueFromDefault<T>(T? real, T? _default) where T : struct
        {
            if (real.HasValue) return real.Value;
            if (_default.HasValue) return _default.Value;
            throw new RuntimeException("either default or instance not set .");
        }

        static string extractStringValueFromDefault(string real, string _default)
        {
            if (!string.IsNullOrEmpty(real)) return real;
            if (!string.IsNullOrEmpty(_default)) return _default;
            throw new RuntimeException("either default or instance not set .");
        }

        static T extractRefValueFromDefault<T>(T real, T _default) where T : class
        {
            if (real != default) return real;
            if (_default != default) return _default;
            throw new RuntimeException("either default or instance not set .");
        }
    }
}
