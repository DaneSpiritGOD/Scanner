﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Archiving.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Archiving.Core.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 找不到对应的Monitor.
        /// </summary>
        internal static string CannotFindMonitor {
            get {
                return ResourceManager.GetString("CannotFindMonitor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 读取 {File} 发生错误, 可能是当时正在写入文件。信息: 总尝试自旋 {Time} ms..
        /// </summary>
        internal static string ConflictIOExceptionString {
            get {
                return ResourceManager.GetString("ConflictIOExceptionString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 已经开始监控！.
        /// </summary>
        internal static string HaveBeganMonitor {
            get {
                return ResourceManager.GetString("HaveBeganMonitor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MonitorCluster发生异常.
        /// </summary>
        internal static string MonitorClusterExceptionString {
            get {
                return ResourceManager.GetString("MonitorClusterExceptionString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MonitorCluster内部发生未知异常.
        /// </summary>
        internal static string MonitorRouterInnerUnknownExceptionString {
            get {
                return ResourceManager.GetString("MonitorRouterInnerUnknownExceptionString", resourceCulture);
            }
        }
    }
}
