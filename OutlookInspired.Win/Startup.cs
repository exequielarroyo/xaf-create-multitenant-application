﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.ApplicationBuilder;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.ExpressApp.Design;
using OutlookInspired.Win.Extensions;
namespace OutlookInspired.Win;
public class ApplicationBuilder : IDesignTimeApplicationFactory {
    public static WinApplication BuildApplication(){
        var builder = WinApplication.CreateBuilder();
        builder.UseApplication<OutlookInspiredWindowsFormsApplication>();
        builder.AddModules();
        builder.AddObjectSpaceProviders();
        builder.AddSecurity();
        builder.AddBuildStep(application => application.DatabaseUpdateMode = DatabaseUpdateMode.Never);
        return builder.Build();
    }
    
    XafApplication IDesignTimeApplicationFactory.Create() {
        MiddleTierClientSecurityBase.DesignModeUserType = typeof(Module.BusinessObjects.ApplicationUser);
        MiddleTierClientSecurityBase.DesignModeRoleType = typeof(PermissionPolicyRole);
        return BuildApplication();
    }
}