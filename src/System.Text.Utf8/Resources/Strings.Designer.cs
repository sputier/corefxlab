﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace System.Text.Utf8.Resources {
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
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("System.Text.Utf8.Resources.Strings", typeof(Strings).Assembly);
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
        ///   Looks up a localized string similar to The provided callback created a UTF-8 string that is not well-formed..
        /// </summary>
        internal static string Argument_CreateCallbackReturnedIllFormedUtf8String {
            get {
                return ResourceManager.GetString("Argument_CreateCallbackReturnedIllFormedUtf8String", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided argument is not a valid Unicode scalar value. Unicode scalar values must be within the range [ U+0000..U+D7FF ], inclusive; or [ U+E000..U+10FFFF ], inclusive..
        /// </summary>
        internal static string Argument_NotValidUnicodeScalar {
            get {
                return ResourceManager.GetString("Argument_NotValidUnicodeScalar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided buffer is not large enough to hold the output data..
        /// </summary>
        internal static string Argument_OutputBufferTooSmall {
            get {
                return ResourceManager.GetString("Argument_OutputBufferTooSmall", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This operation would create a Utf8String that is not well-formed..
        /// </summary>
        internal static string InvalidOperation_SubstringWouldCreateIllFormedUtf8String {
            get {
                return ResourceManager.GetString("InvalidOperation_SubstringWouldCreateIllFormedUtf8String", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The comparison type &apos;{0}&apos; is not valid for this API..
        /// </summary>
        internal static string NotSupported_BadComparisonType {
            get {
                return ResourceManager.GetString("NotSupported_BadComparisonType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value must be non-negative..
        /// </summary>
        internal static string ValueMustBeNonNegative {
            get {
                return ResourceManager.GetString("ValueMustBeNonNegative", resourceCulture);
            }
        }
    }
}
