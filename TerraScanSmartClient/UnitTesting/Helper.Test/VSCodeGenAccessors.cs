﻿// ------------------------------------------------------------------------------
//<autogenerated>
//        This code was generated by Microsoft Visual Studio Team System 2005.
//
//        Changes to this file may cause incorrect behavior and will be lost if
//        the code is regenerated.
//</autogenerated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Helper.Test
{
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class BaseAccessor {
    
    protected Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject m_privateObject;
    
    protected BaseAccessor(object target, Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) {
        m_privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(target, type);
    }
    
    protected BaseAccessor(Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) : 
            this(null, type) {
    }
    
    internal virtual object Target {
        get {
            return m_privateObject.Target;
        }
    }
    
    public override string ToString() {
        return this.Target.ToString();
    }
    
    public override bool Equals(object obj) {
        if (typeof(BaseAccessor).IsInstanceOfType(obj)) {
            obj = ((BaseAccessor)(obj)).Target;
        }
        return this.Target.Equals(obj);
    }
    
    public override int GetHashCode() {
        return this.Target.GetHashCode();
    }
}


[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_BizCompHelperAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("TerraScan.Helper", "TerraScan.Helper.BizCompHelper");
    
    internal TerraScan_Helper_BizCompHelperAccessor(object target) : 
            base(target, m_privateType) {
    }
    
    internal static object CreatePrivate() {
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject("TerraScan.Helper", "TerraScan.Helper.BizCompHelper", new object[0]);
        return priv_obj.Target;
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_Properties_SettingsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType("TerraScan.Helper", "TerraScan.Helper.Properties.Settings");
    
    internal TerraScan_Helper_Properties_SettingsAccessor(object target) : 
            base(target, m_privateType) {
    }
    
    internal static global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor defaultInstance {
        get {
            object _ret_val = m_privateType.GetStaticField("defaultInstance");
            global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor _ret = null;
            if ((_ret_val != null)) {
                _ret = new global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor(_ret_val);
            }
            global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor ret = _ret;
            return ret;
        }
        set {
            m_privateType.SetStaticField("defaultInstance", value);
        }
    }
    
    internal static global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor Default {
        get {
            object _ret_val = m_privateType.GetStaticProperty("Default");
            global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor _ret = null;
            if ((_ret_val != null)) {
                _ret = new global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor(_ret_val);
            }
            global::Helper.Test.TerraScan_Helper_Properties_SettingsAccessor ret = _ret;
            return ret;
        }
    }
    
    internal string TerraScan_Helper_TerraScan_WebService_TerraScanService {
        get {
            string ret = ((string)(m_privateObject.GetProperty("TerraScan_Helper_TerraScan_WebService_TerraScanService")));
            return ret;
        }
    }
    
    internal static global::System.Configuration.ApplicationSettingsBase CreatePrivate() {
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject("TerraScan.Helper", "TerraScan.Helper.Properties.Settings", new object[0]);
        return ((global::System.Configuration.ApplicationSettingsBase)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetAttachmentCountCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetAttachmentCountCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetAttachmentCountCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetAttachmentCountCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetAttachmentCountCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetAttachmentCountCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetAttachmentCountCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetCommentsCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetCommentsCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetCommentsCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetCommentsCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetCommentsCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetCommentsCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetCommentsCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetCommentsCountCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetCommentsCountCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetCommentsCountCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetCommentsCountCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetCommentsCountCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetCommentsCountCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetCommentsCountCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetInterestAmountCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetInterestAmountCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetInterestAmountCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetInterestAmountCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetInterestAmountCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetInterestAmountCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetInterestAmountCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetMinTaxDueCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetMinTaxDueCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetMinTaxDueCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetMinTaxDueCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetMinTaxDueCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetMinTaxDueCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetMinTaxDueCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetRealEstateStatementCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetRealEstateStatementCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetRealEstateStatementCountCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCountCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetRealEstateStatementCountCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCountCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCountCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCountCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementCountCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetRealEstateStatementIDsCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementIdsCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetRealEstateStatementIDsCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementIdsCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementIdsCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementIdsCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetRealEstateStatementIdsCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetReceiptDetailsCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetReceiptDetailsCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetReceiptDetailsCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetReceiptDetailsCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetReceiptDetailsCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetReceiptDetailsCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetReceiptDetailsCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_GetValidReceiptTestCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.GetValidReceiptTestCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_GetValidReceiptTestCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.GetValidReceiptTestCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.GetValidReceiptTestCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.GetValidReceiptTestCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.GetValidReceiptTestCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_ListHistoryGridCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.ListHistoryGridCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_ListHistoryGridCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.ListHistoryGridCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.ListHistoryGridCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.ListHistoryGridCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.ListHistoryGridCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_ListTenderTypeCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.ListTenderTypeCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_ListTenderTypeCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.ListTenderTypeCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.ListTenderTypeCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.ListTenderTypeCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.ListTenderTypeCompletedEventArgs)(priv_obj.Target));
    }
}
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class TerraScan_Helper_TerraScan_WebService_SaveReceiptCompletedEventArgsAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::TerraScan.Helper.TerraScan.WebService.SaveReceiptCompletedEventArgs));
    
    internal TerraScan_Helper_TerraScan_WebService_SaveReceiptCompletedEventArgsAccessor(global::TerraScan.Helper.TerraScan.WebService.SaveReceiptCompletedEventArgs target) : 
            base(target, m_privateType) {
    }
    
    internal object[] results {
        get {
            object[] ret = ((object[])(m_privateObject.GetField("results")));
            return ret;
        }
        set {
            m_privateObject.SetField("results", value);
        }
    }
    
    internal static global::TerraScan.Helper.TerraScan.WebService.SaveReceiptCompletedEventArgs CreatePrivate(object[] results, global::System.Exception exception, bool cancelled, object userState) {
        object[] args = new object[] {
                results,
                exception,
                cancelled,
                userState};
        Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject priv_obj = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(typeof(global::TerraScan.Helper.TerraScan.WebService.SaveReceiptCompletedEventArgs), new System.Type[] {
                    typeof(object).MakeArrayType(),
                    typeof(global::System.Exception),
                    typeof(bool),
                    typeof(object)}, args);
        return ((global::TerraScan.Helper.TerraScan.WebService.SaveReceiptCompletedEventArgs)(priv_obj.Target));
    }
}
}