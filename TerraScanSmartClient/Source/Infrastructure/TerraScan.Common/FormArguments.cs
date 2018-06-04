namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    #region Operation SmartPart Related Arguments

    public struct OperationSmartPartFields
    {
        public string NewButtonText;
        public bool NewButtonVisible;


        public string SaveButtonText;
        public bool SaveButtonVisible;

        public string CancelButtonText;
        public bool CancelButtonVisible;

        public string DeleteButtonText;
        public bool DeleteButtonVisible;
    }



    public struct PermissionFields
    {
        public bool openPermission;

        public bool newPermission;

        public bool editPermission;

        public bool deletePermission;
    }

    #endregion

    #region Common Functionality Arguments

    public struct FormInfo
    {
        public string formFile;
        public string visibleName;
        public int openPermission;
        public int addPermission;
        public int editPermission;
        public int deletePermission;
        public object[] optionalParameters;
        public int form;
    }

    #endregion

    #region Slice Related Arguments

    public struct SliceValidationFields
    {
        public string ErrorMessage;
        public int FormNo;
        public bool RequiredFieldMissing;
        public bool DisableNewMethod;
    }

    public struct SliceFormCloseAlert
    {
        public int FormNo;
        public bool FlagFormClose;
    }

    public struct SliceResize
    {
        public int MasterFormNo;
        public string SliceFormName;
        public int SliceFormHeight;
    }

    public struct SliceReloadActiveRecord
    {
        public int MasterFormNo;
        public int SelectedKeyId;

    }

    public struct SliceReloadParaMeterizedActiveRecord
    {
        public int MasterFormNo;
        public int SelectedKeyId;
        public List<string> ParameterList;
    }

    public struct SliceReloadParaMeterActiveRecord
    {
        public int MasterFormNo;
        public int SelectedKeyId;
        public string ParameterList;
    }

    public struct F35002SubFormSaveEventArgs
    {
        public int valueSliceId;
        public decimal value;
        public byte type;
        public decimal amount;
    }

    public struct EnablePanelEventArgs
    {
        public bool IsSlice;
        public bool IsVisible;
    }

    public struct SliceNullRecordModeEventArgs
    {
        public int MasterFormNo;
        public bool AllowNullRecordMode;
        public bool WithoutKeyId;
    }

    #region QueryEngineRelated

    #region LoadLayout

    public struct LoadLayoutDetails
    {
        public int LayoutID;

        public string LayoutXml;

        public string LayoutName;

        public int MasterFormNo;

    }

    #endregion LoadLayout

    #region LoadSnapShot

    public struct LoadSnapShotDetails
    {
        public int SnapShotId;

        public int MasterFormNO;

        public int QueryViewId;

        public int SnapShotCount;
    }

    #endregion LoadSnapShot

    #region LoadSystemSnapShot

    public struct LoadSystemSnapShotDetails
    {
        public int MasterFormNO;

        public int RecordsetType;

        public bool IsSystemSnapShotLoaded;

        public string KeyIdColumnName;

        public string SnapShotXML;
    }

    #endregion LoadSystemSnapShot

    #region SetSystemSnapShotIdnCount

    public struct SetSystemSnapShotIdnCount
    {
        public int SystemSnapShotId;

        public int SystemSnapShotCount;
    }

    #endregion SetSystemSnapShotIdnCount

    #endregion QueryEngineRelated

    #endregion Slice Related Arguments

    #region RDL structs

    public struct ComboSQLText
    {
        public string ComboName;

        public string SqlText;
    }

    public struct DataSetCollection
    {
        public string dataSetName;
        public string commandType;
        public string commandText;
    }


    #endregion RDL structs


    #region Parcellockimplimentation structs

    public struct Parcellockimplimentation
    {
        public int MasterFormNo;
        public Color SaveButtonBackColor;
        public Color SaveButtonForeColor;
        public string SaveButtonText;
        public bool DeleteButtonEnable;
        public bool CancelButtonEnable;
        public string SaveButtonTooltipText;
        public bool SaveButtonEnable;
        ////public Type saveButtonType;
    }

    #endregion

}