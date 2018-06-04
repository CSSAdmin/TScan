
namespace T2Installer
{
    public static class ConstantVariable
    {
        public static readonly string Terrascan64Registry = "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe";
        public static readonly string Terrascan32Registry = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\Terrascan.exe";
        public static readonly string SetupName = "TerraScan.WCFService.Setup";
        public static readonly string T2SetupName = "TerraScan T2 Treasurer";
        public static readonly string WCFExists = "WCF already installed. Kindly uninstall and proceed further...";
        public static readonly string T2Exists = "Treasurer already installed. kindly uninstall and proceed further...";
        public static readonly string T2AndWCFExists = "WCF and Treasurer already installed. Kindly uninstall and proceed further...";
        public static readonly string InstallationFailed = "Installation failed!";
        public static readonly string MismatchPhysicalOrVirtualDir = "Mismatching physical or virtual directories...";
        public static readonly string AddVirtualDir = "Please add virtual directory in the xml file...";
        public static readonly string WcfPhysicalDir = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + VersionNumber + "phywcf.exe";
        public static readonly string T2PhysicalDir = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + VersionNumber + "phyt2.exe";
        public static readonly string WCFExeName = "\\TerraScan.WCFSetup\\setup.exe";
        public static readonly string InstallationName = "T2 MSI Installation";
        public static readonly string DefaultWebSiteName = "Default Web Site";
        public static readonly string MessageBoxTitle = "TerraScan Setup";
        public static readonly string AbortMessage = "The installation is not yet complete. Are you sure you want to exit?";
        public static readonly string WcfMsiName = "\\TerraScan.WCFSetup\\TerraScan.WCFService.Setup.msi";
        public static readonly string WCF = "WCF";
        public static readonly string DomainName = "DomainName";
        public static readonly string LDAP = "LDAP";
        public static readonly string UserName = "UserName";
        public static readonly string Password = "Password";
        public static readonly string DbServerName = "DbServerName";
        public static readonly string DbName = "DbName";
        public static readonly string Handle = "Handle";
        public static readonly string Treasurer = "Treasurer";
        public static readonly string WCFServiceURL = "WCFServiceURL";
        public static readonly string InstalURL = "InstalURL";
        public static readonly string MSWCFServiceURL = "MSWCFServiceURL";
        public static readonly string Installer = "Installer";
        public static readonly string SetupXml = "\\setup.xml";
        public static readonly string SmartClientServiceSVC = "/T2Web/SmartClientService.svc";
        public static readonly string T2App = "/T2App";
        public static readonly string ServiceSVC = "/T2MS/Service.svc";
        public static readonly string TextMissing = "Text Missing";
        public static readonly string used = "used";
        public static readonly string UIPolicyName = "UI Exception Policy";
        public static readonly string ExceptionMessage = "Error occured. Administrator might be aware of this problem.\nPlease try after sometime.";
        public static readonly string VersionNumber = "2.177.24";
        public static readonly string LogSource = "TerraScan";
        public static readonly string TargetExists = "Virtual path already exists. please try a different path.";
    }
}
